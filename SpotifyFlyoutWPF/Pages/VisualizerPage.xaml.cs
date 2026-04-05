using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;
using SpotifyFlyout.Classes.Settings;

namespace SpotifyFlyoutWPF.Pages
{
    public partial class VisualizerPage : Page
    {
        public VisualizerPage()
        {
            DataContext = SettingsManager.Current;
            InitializeComponent();
        }

        private void OutputDeviceHyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }
}
