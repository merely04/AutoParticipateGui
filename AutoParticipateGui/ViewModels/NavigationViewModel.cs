using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using AutoParticipateGui.Annotations;
using AutoParticipateGui.Views.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AutoParticipateGui.ViewModels
{
    public class NavigationViewModel: ViewModelBase
    {
        private readonly List<Page> _pages;

        public NavigationViewModel()
        {
            _pages = new List<Page>()
            {
                new General(),
                new Proxy(),
                new Delays(),
                new WebDriver(),
                new Notifications()
            };

            _currentPage = _pages.First();
        }
        
        private Page _currentPage;

        public Page CurrentPage
        {
            get => _currentPage;
            set => Set(ref _currentPage, value);
        }

        private RelayCommand<Type> _navigateCommand;
        
        public RelayCommand<Type> NavigateCommand
        {
            get
            {
                return _navigateCommand ??
                       (_navigateCommand = new RelayCommand<Type>(obj =>
                       {
                           try
                           {
                               var page = _pages.FirstOrDefault(x => x.GetType() == obj);
                               if (page == null || Equals(page, CurrentPage)) return;
                               CurrentPage = page;
                           }
                           catch {}
                       }));
            }
        }
    }
}