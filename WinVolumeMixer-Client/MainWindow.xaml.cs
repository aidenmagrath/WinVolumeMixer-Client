using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinVolumeMixer.Client
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppSettings.Default.AutoConnect)
            {
                ConnectionManager.GetManager().Connect(AppSettings.Default.Host, AppSettings.Default.Port);
            }
            else
            {
                ConnectionWindow conWin = new ConnectionWindow();
                conWin.Owner = GetWindow(this);
                conWin.ShowDialog();
            }
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void menuRefresh_Click(object sender, RoutedEventArgs e)
        {
            ApplicationManager.GetManager().RefreshApplications();
        }

        private void menuConnect_Click(object sender, RoutedEventArgs e)
        {
            ConnectionWindow connectionWindow = new ConnectionWindow();
            connectionWindow.Owner = GetWindow(this);
            connectionWindow.ShowDialog();
        }

        private void menuOptions_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.Owner = GetWindow(this);
            optionsWindow.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ApplicationManager.GetManager().RefreshApplications();
        }

        private void menuDisconnect_Click(object sender, RoutedEventArgs e)
        {
            ConnectionManager.GetManager().Disconnect();
        }
    }
}
