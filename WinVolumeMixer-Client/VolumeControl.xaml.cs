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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinVolumeMixer.Client
{
    public partial class VolumeControl : UserControl
    {
        private Application application;
        private bool dragStarted;

        public VolumeControl(Application application)
        {
            InitializeComponent();

            this.application = application;
            txtApplication.Text = application.Name;
            txtApplication.ToolTip = application.Name;
            btnMute.IsChecked = application.Muted;

            slider.Value = application.Volume * 100;
        }

        private void UpdateVolume(float sliderValue)
        {
            application.Volume = (float) sliderValue / 100;
            application.Muted = false;
            btnMute.IsChecked = false;
            ApplicationManager.GetManager().UpdateApplication(application);
        }

        private void slider_DragStarted(object sender, RoutedEventArgs e)
        {
            dragStarted = true;
        }

        private void slider_DragCompleted(object sender, RoutedEventArgs e)
        {
            UpdateVolume((float)slider.Value);
            dragStarted = false;
        }

        private void btnMute_Click(object sender, RoutedEventArgs e)
        {
            application.Muted = btnMute.IsChecked.GetValueOrDefault();
            ApplicationManager.GetManager().UpdateApplication(application);
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!dragStarted)
            {
                UpdateVolume((float)slider.Value);
            }
        }
    }
}
