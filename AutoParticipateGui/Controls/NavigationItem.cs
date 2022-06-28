using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace AutoParticipateGui.Controls
{
    public class NavigationItem : ButtonBase
    {
        static NavigationItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationItem), new FrameworkPropertyMetadata(typeof(NavigationItem)));
        }

        public static readonly DependencyProperty ActiveProperty = DependencyProperty.Register("Active", typeof(bool), typeof(NavigationItem), new PropertyMetadata(null));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(NavigationItem), new PropertyMetadata(null));
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(Type), typeof(NavigationItem), new PropertyMetadata(null));

        public bool Active
        {
            get => (bool)GetValue(ActiveProperty);
            set => SetValue(ActiveProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Type Type
        {
            get => (Type)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }
    }
}