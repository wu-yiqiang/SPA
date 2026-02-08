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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        public void Handle_Open(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("打开");

        }
        public void Handle_Close(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("关闭");
        }

    }

}