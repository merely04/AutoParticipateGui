using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using AutoParticipateGui.Types;
using GalaSoft.MvvmLight;

namespace AutoParticipateGui.ViewModels
{
    public class WebDriverViewModel : ViewModelBase
    {
        public List<ClickType> ClickTypes => Enum.GetValues(typeof(ClickType)).Cast<ClickType>().ToList();
    }
}