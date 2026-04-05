using SpotifyFlyout.Classes.Settings;
using System;
using System.IO;
using Windows.Storage;

namespace SpotifyFlyoutWPF.Classes.Utils
{
    internal class FileSystemHelper
    {
        public static string GetLogsPath()
        {
            return SettingsManager.Current.IsStoreVersion
                ? Path.Combine(ApplicationData.Current.LocalCacheFolder.Path, "Roaming", "SpotifyFlyout")
                : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SpotifyFlyout");
        }
    }
}
