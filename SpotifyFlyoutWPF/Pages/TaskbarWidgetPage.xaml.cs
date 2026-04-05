using System.Windows;
using System.Windows.Controls;
using SpotifyFlyout.Classes.Settings;

namespace SpotifyFlyoutWPF.Pages
{
    public partial class TaskbarWidgetPage : Page
    {
        public TaskbarWidgetPage()
        {
            DataContext = SettingsManager.Current;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TaskbarWidgetSelectedMonitorComboBox.Items.Clear();
            for (int i = 0; i < System.Windows.Forms.Screen.AllScreens.Length; i++)
            {
                TaskbarWidgetSelectedMonitorComboBox.Items.Add($"Monitor {i + 1}");
            }
            TaskbarWidgetSelectedMonitorComboBox.SelectedIndex = SettingsManager.Current.TaskbarWidgetSelectedMonitor;
        }
    }
}
