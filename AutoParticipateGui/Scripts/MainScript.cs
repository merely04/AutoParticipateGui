using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AutoParticipateGui.Controllers;
using AutoParticipateGui.Services;
using AutoParticipateGui.Stores;

namespace AutoParticipateGui.Scripts
{
    public class MainScript : IDisposable
    {
        private bool _stop;
        private Thread _scriptThread;

        protected virtual ScriptStore ScriptStore
        {
            get
            {
                if (Application.Current.Resources["ScriptStore"] is ScriptStore scriptStore)
                    return scriptStore;

                throw new Exception("ScriptStore not found in Application Resources");
            }
        }

        public void Start()
        {
            _scriptThread = new Thread(RunScript);
            _scriptThread.Start();
        }

        private void RunScript()
        {
            _stop = false;
            ScriptStore.Status = ScriptStatus.Working;

            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        ScriptStore.Logs.Clear();
                    }
                    catch {}
                });

                CheckDependencies();
                UpdateScript();
                InstallRequirements();
                FillSettings();

                var scriptPath = FileService.ScriptPath;
                var mainPy = Path.Combine(scriptPath, "main.py");
                var arguments = $"-u \"{mainPy}\"";

                var process = ChildProcessService.CreatePythonProcess(scriptPath, arguments);
                process.ErrorDataReceived += ProcessOnDataReceived;
                process.OutputDataReceived += ProcessOnDataReceived;

                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                while (process.HasExited == false || _stop == false)
                {
                    Thread.Sleep(1000);
                    if (process.HasExited || _stop) break;
                }

                process.Kill();
                WriteLog("Script process killed");
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
            finally
            {
                ScriptStore.Status = ScriptStatus.Idling;
            }
        }
        
        protected virtual void CheckDependencies()
        {
            var errors = new List<string>();
            
            var isChromeInstalled = SettingsController.IsChromeBrowserInstalled;
            var isPythonInstalled = SettingsController.IsPythonInstalled;
            
            WriteLog("Проверка зависимостей среды...");
            
            WriteLog($"Chrome: {isChromeInstalled}");
            WriteLog($"Python: {isPythonInstalled}");

            if (isPythonInstalled == false)
            {
                WriteLog("Для работы необходим Python версии 3.7.6");

                var tempPath = FileService.TempPath;
                var arguments = new List<string>
                {
                    "curl \"https://www.python.org/ftp/python/3.7.6/python-3.7.6-amd64.exe\" --output python.exe",
                    "python.exe /passive InstallAllUsers=0 PrependPath=1 Include_test=0 SimpleInstall=1",
                    "exit"
                    // "DEL /F /A python.exe"
                };
                
                WriteLog("Загрузка файла python-3.7.6-amd64.exe");

                try
                {
                    var process = ChildProcessService.CreateCmdProcess(tempPath);
                    process.ErrorDataReceived += ProcessOnDataReceived;
                    process.OutputDataReceived += ProcessOnDataReceived;
                        
                    process.Start();
                    process.BeginErrorReadLine();
                    process.BeginOutputReadLine();
                    
                    arguments.ForEach(arg =>
                    {
                        process.StandardInput.WriteLine(arg);
                    });

                    process.WaitForExit();
                    
                    errors.Add("Завершите установку python-3.7.6");
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }
            
            if (isChromeInstalled == false)
            {
                errors.Add("Для работы необходим браузер Chrome");
                Process.Start("https://www.google.com/intl/ru_ru/chrome/");
            }
            
            if (errors.Count > 0)
            {
                throw new Exception(string.Join("\n", errors));
            }
        }

        protected virtual void UpdateScript()
        {
            var scriptPath = FileService.ScriptPath;
            var updatePath = FileService.UpdatePath;
            var fileName = Path.Combine(updatePath, "latest.zip");

            WriteLog("Загрузка последней версии файлов...");
            ApiService.DownloadUpdate(fileName);
            
            WriteLog("Распаковка файлов...");
            FileService.UnzipFile(fileName, scriptPath);
        }

        protected virtual void InstallRequirements()
        {
            var scriptPath = FileService.ScriptPath;
            var requirementsFile = Path.Combine(scriptPath, "requirements.txt");
            var arguments = new List<string>
            {
                "-m pip install --upgrade pip",
                "-m pip install -r requirements.txt",
                "-m pip install --upgrade h2"
            };

            if (File.Exists(requirementsFile) == false) return;

            WriteLog("Установка зависимостей #2");
            arguments.ForEach(arg =>
            {
                try
                {
                    var process = ChildProcessService.CreatePythonProcess(scriptPath, arg);
                    process.ErrorDataReceived += ProcessOnDataReceived;
                    process.OutputDataReceived += ProcessOnDataReceived;

                    process.Start();
                    process.BeginErrorReadLine();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                }
                catch {}
            });
            
            WriteLog("Зависимости установлены");
        }

        protected virtual void FillSettings()
        {
            var scriptPath = FileService.ScriptPath;
            var settingsFile = Path.Combine(scriptPath, "settings.json");
            var json = DataController.Settings.ToJson();

            using (var fs = new FileStream(settingsFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.Write(json);
                sw.Flush();
            }
        }

        protected virtual string GetCommandLine(Process process)
        {
            using (var searcher = new ManagementObjectSearcher($"SELECT CommandLine FROM Win32_Process WHERE ProcessId = ${process.Id}"))
            using (var objects = searcher.Get())
            {
                return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
            }
        }

        protected virtual void ProcessOnDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                var data = e.Data;
                if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data)) return;
                
                WriteLog(data);
            }
            catch {}
        }

        private void WriteLog(string message)
        {
            Debug.WriteLine(message);

            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    ScriptStore.Logs.Add(message);
                }
                catch {}
            });
        }

        public void Dispose()
        {
            var thread = new Thread(() =>
            {
                try
                {
                    if (_scriptThread == null || !_scriptThread.IsAlive)
                        return;

                    _stop = true;

                    var driverProcesses = Process.GetProcessesByName("chromedriver").ToList();
                    if (driverProcesses.Count > 0)
                    {
                        driverProcesses.ForEach(p =>
                        {
                            try
                            {
                                p.Kill();
                                Debug.WriteLine($"[{p.Id}] chromedriver killed");
                            }
                            catch {}
                        });
                    }

                    var chromeProcesses = Process.GetProcessesByName("chrome")
                        .Where(p => GetCommandLine(p).Contains("--headless"))
                        .ToList();
                    if (chromeProcesses.Count > 0)
                    {
                        chromeProcesses.ForEach(p =>
                        {
                            try
                            {
                                p.Kill();
                                Debug.WriteLine($"[{p.Id}] chrome killed");
                            }
                            catch {}
                        });
                    }
                }
                catch {}
            });

            thread.Start();

            Task.Run(() =>
            {
                thread.Join();
            });
        }
    }
}