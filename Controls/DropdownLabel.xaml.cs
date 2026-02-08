using System.Collections;
using System.Windows;
using System.Windows.Controls;


namespace spa.Controls
{
    /// <summary>
    /// DropdownLabel.xaml 的交互逻辑
    /// </summary>
    public partial class DropdownLabel : UserControl
    {
        public static readonly DependencyProperty LabelProperty =
           DependencyProperty.Register("Label", typeof(string), typeof(DropdownLabel), new PropertyMetadata("选项"));

        public static readonly DependencyProperty ItemsSourceProperty =
           DependencyProperty.Register("ItemsSource", typeof(object), typeof(DropdownLabel), new PropertyMetadata(null));
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(DropdownLabel), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedValue
        {
            get => GetValue(SelectedValueProperty);
            set => SetValue(SelectedValueProperty, value);
        }
        public DropdownLabel()
        {
            InitializeComponent();
        }
    }
}
