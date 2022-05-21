using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using AutoParticipateGui.ViewModels;

namespace AutoParticipateGui
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += OnClosing;
        }

        private void Frame_OnContentRendered(object sender, EventArgs e)
        {
            if (sender is Frame frame &&
                frame.Content is Page currentPage && 
                Application.Current.Resources["Locator"] is ViewModelLocator locator)
            {
                locator.Navigation.NavigateCommand.Execute(currentPage.GetType());
            }
        }

        protected virtual void OnClosing(object sender, CancelEventArgs e)
        {
            ViewModelLocator.Cleanup();
        }
    }
}