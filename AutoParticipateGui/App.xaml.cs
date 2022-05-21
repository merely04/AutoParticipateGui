using System;
using System.Windows;
using System.Windows.Threading;

namespace AutoParticipateGui
{
    public partial class App
    {
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            
            MessageBox.Show(
                "An unhandled exception just occurred: " + e.Exception.Message, 
                AppDomain.CurrentDomain.FriendlyName.Replace(".exe", ""), 
                MessageBoxButton.OK, 
                MessageBoxImage.Warning
            );
        }
    }
}