using iNKORE.UI.WPF.Modern;
using Microsoft.Win32;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows;
using Microsoft.Win32;


namespace spa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application

    {
        private const string Key = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string Value = "AppsUseLightTheme";

        public static bool IsLight => (Registry.GetValue(Registry.CurrentUser.Name + "\\" + Key, Value, 1) as int?) == 1;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        ThemeManager.Current.ApplicationTheme = IsLight ? ApplicationTheme.Light : ApplicationTheme.Dark;
    }
    }

}
