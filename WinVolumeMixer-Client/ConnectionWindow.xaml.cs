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
    public partial class ConnectionWindow : Window
    {
        public ConnectionWindow()
        {
            InitializeComponent();
            txtHost.Text = AppSettings.Default.Host;
            txtPort.Text = AppSettings.Default.Port;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            AppSettings.Default.Host = txtHost.Text;
            AppSettings.Default.Port = txtPort.Text;
            ConnectionManager.GetManager().Connect(txtHost.Text, txtPort.Text);
        }
    }
}
