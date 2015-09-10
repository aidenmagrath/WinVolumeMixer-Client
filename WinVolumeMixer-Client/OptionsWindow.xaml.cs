using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WinVolumeMixer.Client
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();

            txtHost.Text = AppSettings.Default.Host;
            txtPort.Text = AppSettings.Default.Port;
            checkAutoConnect.IsChecked = AppSettings.Default.AutoConnect;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            AppSettings.Default.Host = txtHost.Text;
            AppSettings.Default.Port = txtPort.Text;
            AppSettings.Default.AutoConnect = checkAutoConnect.IsChecked.GetValueOrDefault();

            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
