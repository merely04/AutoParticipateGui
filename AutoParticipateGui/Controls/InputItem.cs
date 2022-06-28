using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AutoParticipateGui.Controls
{
    public class InputItem : TextBox
    {
        static InputItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputItem), new FrameworkPropertyMetadata(typeof(InputItem)));
        }
        
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(InputItem), new PropertyMetadata(null));
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(InputItem), new PropertyMetadata(null));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }
    }
}