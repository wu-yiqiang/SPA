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
            DependencyProperty.Register("Label", typeof(string), typeof(DropdownLabel), new PropertyMetadata(""));

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
          DependencyProperty.Register(
              name: "ItemsSource",                     // 属性名（必须与 CLR 属性同名）
              propertyType: typeof(IEnumerable),       // 类型：IEnumerable 或 object
              ownerType: typeof(DropdownLabel)           // 所属类型
          );
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DropdownLabel()
        {
            InitializeComponent();
        }
    }
}
