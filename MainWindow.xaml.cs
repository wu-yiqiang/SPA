using spa.ViewModels;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace spa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();

            DataContext = _viewModel;

        }

        public void Handle_Open(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("打开");

        }
        public void Handle_Close(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("关闭");
        }
        private bool _isUserScrolling = false;

        private void TxtLog_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {

                if (!_isUserScrolling && _viewModel.AutoScroll == true)
                {
                    textBox.ScrollToEnd();
                }
            }
        }
        private void TxtLog_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var scrollViewer = textBox.Template.FindName("PART_ContentHost", textBox) as ScrollViewer;
                if (scrollViewer != null)
                {
                    _isUserScrolling = scrollViewer.VerticalOffset < scrollViewer.ScrollableHeight - 10;
                }
            }
        }

    }

}