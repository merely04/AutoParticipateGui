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
using AutoParticipateGui.Services;
using AutoParticipateGui.Stores;

namespace AutoParticipateGui.Scripts
{
    public class MainScript : IDisposable
    {
        private bool _stop;
        private Thread _scriptThread;

        public MainScript()
        {

        }

        protected virtual ScriptStore ScriptStore
        {
            get
            {
                if (Application.Current.Resources["ScriptStore"] is ScriptStore scriptStore)
                    return scriptStore;

                throw new Exception("ScriptStore not found in Application Resources");
            }
        }

        protected virtual SettingsStore SettingsStore
        {
            get
            {
                if (Application.Current.Resources["SettingsStore"] is SettingsStore settingsStore)
                    return settingsStore;

                throw new Exception("SettingsStore not found in Application Resources");
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

                UpdateScript();
                InstallRequirements();
                FillSettings();

                var scriptPath = FileService.ScriptPath;
                var mainPy = Path.Combine(scriptPath, "main.py");
                var arguments = $"-u \"{mainPy}\"";

                var process = ChildProcessService.CreateProcess(scriptPath, arguments);
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
                Debug.WriteLine("Script process killed");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                ScriptStore.Status = ScriptStatus.Idling;
            }
        }

        protected virtual void UpdateScript()
        {
            var scriptPath = FileService.ScriptPath;
            var updatePath = FileService.UpdatePath;
            var fileName = Path.Combine(updatePath, "latest.zip");

            ApiService.DownloadUpdate(fileName);
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

            Debug.WriteLine("Install requirements");
            arguments.ForEach(arg =>
            {
                try
                {
                    var process = ChildProcessService.CreateProcess(scriptPath, arg);
                    process.ErrorDataReceived += ProcessOnDataReceived;
                    process.OutputDataReceived += ProcessOnDataReceived;

                    process.Start();
                    process.BeginErrorReadLine();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                }
                catch {}
            });
            Debug.WriteLine("Requirements installed");
        }

        protected virtual void FillSettings()
        {
            var scriptPath = FileService.ScriptPath;
            var settingsFile = Path.Combine(scriptPath, "settings.json");
            var json = SettingsStore.ToJson();

            using (var fs = new FileStream(settingsFile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.Write(json);
                sw.Flush();
            }
        }

        protected virtual string GetCommandLine(Process process)
        {
            using (var searcher =
                new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            using (var objects = searcher.Get())
                return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
        }

        protected virtual void ProcessOnDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                var data = e.Data;
                if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data)) return;

                Debug.WriteLine(data);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        ScriptStore.Logs.Add(data);
                    }
                    catch {}
                });
            }
            catch {}
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