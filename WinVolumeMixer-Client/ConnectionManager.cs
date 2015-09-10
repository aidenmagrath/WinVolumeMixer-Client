using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WinVolumeMixer.Client
{
    class ConnectionManager
    {
        private static ConnectionManager instance = new ConnectionManager();
        public static String ipAddress = "localhost";
        public static String port = "9000";
        public static bool isConnected = false;
        

        public static ConnectionManager GetManager()
        {
            return instance;
        }

        public async Task Connect(string ipAddress, string port)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://" + ipAddress + ":" + port + "/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            ApplicationManager.GetManager().Init(client);

            try
            {
                HttpResponseMessage response = await client.GetAsync("api/application/");
                response.EnsureSuccessStatusCode();
                isConnected = true;
            }
            catch (Exception)
            {
                MessageBox.Show(System.Windows.Application.Current.MainWindow, "Error: Could not connect to the server at " + ipAddress + ":" + port, "Error");
                ConnectionWindow connectionWindow = new ConnectionWindow();
                connectionWindow.Owner = System.Windows.Application.Current.MainWindow;
                connectionWindow.ShowDialog();
                return;
            }
            

            await ApplicationManager.GetManager().RefreshApplications();

            foreach (Window w in System.Windows.Application.Current.Windows)
            {
                if (w.GetType() == typeof(MainWindow))
                {
                    MainWindow window = w as MainWindow;
                    window.menuConnect.IsEnabled = false;
                    window.menuDisconnect.IsEnabled = true;
                    window.menuRefresh.IsEnabled = true;
                }
            }
        }//Checking why we get 2 error messages on fail connect. Check with clicking refresh first.

        public void Disconnect()
        {
            isConnected = false;
            MainWindow mainWindow = (MainWindow) System.Windows.Application.Current.MainWindow;
            mainWindow.menuConnect.IsEnabled = true;
            mainWindow.menuDisconnect.IsEnabled = false;
            mainWindow.menuRefresh.IsEnabled = false;

            ApplicationManager.GetManager().GetApplications().Clear();
            ApplicationManager.GetManager().UpdateUI();
        }
    }
}
