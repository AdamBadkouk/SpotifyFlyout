using System.Windows.Controls;
using SpotifyFlyout.Classes.Settings;

namespace SpotifyFlyoutWPF.Pages
{
    public partial class NextUpPage : Page
    {
        public NextUpPage()
        {
            DataContext = SettingsManager.Current;
            InitializeComponent();
        }
    }
}
