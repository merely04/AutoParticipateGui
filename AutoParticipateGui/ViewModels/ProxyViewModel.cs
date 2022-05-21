using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using AutoParticipateGui.Stores;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

namespace AutoParticipateGui.ViewModels
{
    public class ProxyViewModel : ViewModelBase
    {
        private RelayCommand _documentUploadCommand;

        public RelayCommand DocumentUploadCommand
        {
            get
            {
                return _documentUploadCommand
                       ?? (_documentUploadCommand = new RelayCommand(() => {
                           try
                           {
                               var openFileDialog = new OpenFileDialog
                               {
                                   Title = "Импорт файла с прокси",
                                   RestoreDirectory = false,
                                   Multiselect = false,
                                   Filter = "Txt files (*.txt)|*.txt|All files (*.*)|*.*",
                               };

                               if (openFileDialog.ShowDialog() == false) return;
                               if (Application.Current.Resources["SettingsStore"] is SettingsStore settingsStore)
                               {
                                   settingsStore.ProxyFileName = openFileDialog.FileName;
                               }
                           }
                           catch {}
                       }));
            }
        }
    }
}