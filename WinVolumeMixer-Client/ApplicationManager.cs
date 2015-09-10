using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WinVolumeMixer.Client
{
    class ApplicationManager
    {
        private static ApplicationManager instance = new ApplicationManager();

        private HttpClient client;
        private List<Application> applications = new List<Application>();

        public CancellationTokenSource refreshCancelToken = new CancellationTokenSource();
        public Task autoRefreshTask;

        public static ApplicationManager GetManager()
        {
            return instance;
        }

        public void Init(HttpClient client)
        {
            this.client = client;
        }

        public List<Application> GetApplications()
        {
            return this.applications;
        }

        public HttpClient GetClient()
        {
            return this.client;
        }

        public async Task RefreshApplications()
        {
            if (!ConnectionManager.isConnected)
            {
                return;
            }

            try
            {
                HttpResponseMessage response = await client.GetAsync("api/application/");
                response.EnsureSuccessStatusCode();

                applications = await response.Content.ReadAsAsync<List<Application>>();
            }
            catch (HttpRequestException)
            {
                ConnectionManager.GetManager().Disconnect();
                MessageBox.Show("Error: Client lost connection to server!", "Error");
                applications.Clear();
            }

            UpdateUI();
        }

        public void UpdateUI()
        {
            foreach (Window w in System.Windows.Application.Current.Windows)
            {
                if (w.GetType() == typeof(MainWindow))
                {
                    MainWindow window = w as MainWindow;
                    window.panel.Children.Clear();
                    foreach (Application app in applications)
                    {
                        VolumeControl control = new VolumeControl(app);
                        window.panel.Children.Add(control);
                    }
                }
            }
        }

        public async Task UpdateApplication(Application application)
        {
            if (!ConnectionManager.isConnected)
            {
                return;
            }

            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/application", application);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                ConnectionManager.GetManager().Disconnect();
                MessageBox.Show("Error: Client lost connection to server!", "Error");
                applications.Clear();
                UpdateUI();
            }
        }
    }
}
