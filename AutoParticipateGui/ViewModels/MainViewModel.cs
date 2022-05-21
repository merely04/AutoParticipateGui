using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using AutoParticipateGui.Services;
using AutoParticipateGui.Stores;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AutoParticipateGui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Title = "AutoParticipate GUI";
        }
        
        public string Title { get; set; }

        protected virtual ScriptStore ScriptStore
        {
            get
            {
                if (Application.Current.Resources["ScriptStore"] is ScriptStore scriptStore) 
                    return scriptStore;

                throw new Exception("ScriptStore not found in Application Resources");
            }
        }
        
        private RelayCommand _startStopCommand;

        public RelayCommand StartStopCommand
        {
            get
            {
                return _startStopCommand
                       ?? (_startStopCommand = new RelayCommand(() => {
                           try
                           {
                               
                               
                               if (ScriptStore.Status == ScriptStatus.Idling)
                               {
                                   CheckDependencies();
                                   
                                   ScriptStore.Start();   
                               }
                               else
                               {
                                   ScriptStore.Dispose();
                               }
                           }
                           catch (Exception ex)
                           {
                               MessageBox.Show(ex.Message,
                                   "Зависимости",
                                   MessageBoxButton.OK,
                                   MessageBoxImage.Warning);
                           }
                       }));
            }
        }

        protected virtual void CheckDependencies()
        {
            var isChromeInstalled = RegistryService.IsChromeBrowserInstalled;
            var pythonExecutablePath = RegistryService.PythonExecutablePath;
            var isPythonInstalled = string.IsNullOrEmpty(pythonExecutablePath) == false && 
                                    string.IsNullOrWhiteSpace(pythonExecutablePath)  == false;
            
            Debug.WriteLine(pythonExecutablePath);

            if (isChromeInstalled == false || isPythonInstalled == false)
            {
                var errors = new List<string>();

                if (isChromeInstalled == false)
                {
                    errors.Add("Для работы необходим бразуер Chrome");
                    Process.Start("https://www.google.com/intl/ru_ru/chrome/");
                }

                if (isPythonInstalled == false)
                {
                    errors.Add("Для работы необходим Python версии 3.7.6");
                    Process.Start("https://www.python.org/ftp/python/3.7.6/python-3.7.6-amd64.exe");
                }

                throw new Exception(string.Join("\n", errors));
            }
        }

        private RelayCommand<string> _openLinkCommand;

        public RelayCommand<string> OpenLinkCommand
        {
            get
            {
                return _openLinkCommand
                       ?? (_openLinkCommand = new RelayCommand<string>(obj => {
                           try
                           {
                               Process.Start(obj);
                           }
                           catch {}
                       }));
            }
        }
        
        private RelayCommand<Window> _dragMoveCommand;

        public RelayCommand<Window> DragMoveCommand
        {
            get
            {
                return _dragMoveCommand
                       ?? (_dragMoveCommand = new RelayCommand<Window>(obj => {
                           try
                           {
                               obj.DragMove();
                           }
                           catch {}
                       }));
            }
        }
        
        private RelayCommand<Window> _minimizeCommand;

        public RelayCommand<Window> MinimizeCommand
        {
            get
            {
                return _minimizeCommand
                       ?? (_minimizeCommand = new RelayCommand<Window>(obj => {
                           try
                           {
                               obj.WindowState = WindowState.Minimized;
                           }
                           catch {}
                       }));
            }
        }
        
        private RelayCommand<Window> _closeCommand;

        public RelayCommand<Window> CloseCommand
        {
            get
            {
                return _closeCommand
                       ?? (_closeCommand = new RelayCommand<Window>(obj => {
                           try
                           {
                               obj.Close();
                           }
                           catch {}
                       }));
            }
        }
    }
}