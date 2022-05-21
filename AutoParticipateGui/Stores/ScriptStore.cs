

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using AutoParticipateGui.Annotations;
using AutoParticipateGui.Scripts;

namespace AutoParticipateGui.Stores
{
    public enum ScriptStatus { Idling, Working }

    public class ScriptStore: INotifyPropertyChanged, IDisposable
    {
        private ScriptStatus _status;
        private MainScript _mainScript;

        public ScriptStore()
        {
            Logs = new ObservableCollection<string>();

            _status = ScriptStatus.Idling;
            _mainScript = new MainScript();
        }

        public ObservableCollection<string> Logs { get; set; }
        
        public ScriptStatus Status
        {
            get => _status;
            set
            {
                if (value == _status) return;
                _status = value;
                OnPropertyChanged();
            }
        }

        public void Start()
        {
            _mainScript.Start();
        }

        public void Dispose()
        {
            _mainScript.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}