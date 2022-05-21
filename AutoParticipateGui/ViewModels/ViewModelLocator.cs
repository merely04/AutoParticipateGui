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
            SimpleIoc.Default.Register<GeneralViewModel>();
            SimpleIoc.Default.Register<ProxyViewModel>();
            SimpleIoc.Default.Register<WebDriverViewModel>();
            SimpleIoc.Default.Register<DelaysViewModel>();
        }

        public NavigationViewModel Navigation => ServiceLocator.Current.GetInstance<NavigationViewModel>();

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        
        public GeneralViewModel General => ServiceLocator.Current.GetInstance<GeneralViewModel>();
        
        public ProxyViewModel Proxy => ServiceLocator.Current.GetInstance<ProxyViewModel>();
        
        public WebDriverViewModel WebDriver => ServiceLocator.Current.GetInstance<WebDriverViewModel>();
        
        public DelaysViewModel Delays => ServiceLocator.Current.GetInstance<DelaysViewModel>();

        public static void Cleanup()
        {
            if (Application.Current.Resources["ScriptStore"] is ScriptStore scriptStore)
                scriptStore.Dispose();
        }
    }
}