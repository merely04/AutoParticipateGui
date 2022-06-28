using System.Windows;
using AutoParticipateGui.Stores;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace AutoParticipateGui.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<NavigationViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<WebDriverViewModel>();
        }

        public NavigationViewModel Navigation => ServiceLocator.Current.GetInstance<NavigationViewModel>();

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public WebDriverViewModel WebDriver => ServiceLocator.Current.GetInstance<WebDriverViewModel>();

        public static void Cleanup()
        {
            if (Application.Current.Resources["ScriptStore"] is ScriptStore scriptStore)
                scriptStore.Dispose();
        }
    }
}