// Copyright © 2024-2026 The SpotifyFlyout Authors
// SPDX-License-Identifier: GPL-3.0-or-later

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace SpotifyFlyoutWPF.Pages;

public partial class AboutPage : Page
{
    private const string GitHubUrl = "https://github.com/AdamBadkouk/SpotifyFlyout";
    private const string ReportBugUrl = "https://github.com/AdamBadkouk/SpotifyFlyout/issues";

    public AboutPage()
    {
        InitializeComponent();
    }

    private void GitHubButton_Click(object sender, RoutedEventArgs e)
    {
        OpenUrl(GitHubUrl);
    }

    private void ReportBugButton_Click(object sender, RoutedEventArgs e)
    {
        OpenUrl(ReportBugUrl);
    }

    private static void OpenUrl(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch
        {
            // Silently fail if unable to open URL
        }
    }
}
