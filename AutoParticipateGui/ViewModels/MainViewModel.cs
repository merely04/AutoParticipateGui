using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using AutoParticipateGui.Controllers;
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
                return _startStopCommand ??= new RelayCommand(() => {
                    try
                    {
                        if (ScriptStore.Status == ScriptStatus.Idling)
                        {
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
                });
            }
        }

        private RelayCommand<string> _openLinkCommand;

        public RelayCommand<string> OpenLinkCommand
        {
            get
            {
                return _openLinkCommand ??= new RelayCommand<string>(obj => {
                    try
                    {
                        Process.Start(obj);
                    }
                    catch {}
                });
            }
        }
        
        private RelayCommand<Window> _dragMoveCommand;

        public RelayCommand<Window> DragMoveCommand
        {
            get
            {
                return _dragMoveCommand ??= new RelayCommand<Window>(obj => {
                    try
                    {
                        obj.DragMove();
                    }
                    catch {}
                });
            }
        }
        
        private RelayCommand<Window> _minimizeCommand;

        public RelayCommand<Window> MinimizeCommand
        {
            get
            {
                return _minimizeCommand ??= new RelayCommand<Window>(obj => {
                    try
                    {
                        obj.WindowState = WindowState.Minimized;
                    }
                    catch {}
                });
            }
        }
        
        private RelayCommand<Window> _closeCommand;

        public RelayCommand<Window> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<Window>(obj => {
                    try
                    {
                        obj.Close();
                    }
                    catch {}
                });
            }
        }
    }
}