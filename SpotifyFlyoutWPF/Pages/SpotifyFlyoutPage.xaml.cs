// Copyright © 2024-2026 The SpotifyFlyout Authors
// SPDX-License-Identifier: GPL-3.0-or-later

using SpotifyFlyout.Classes.Settings;
using System.Windows.Controls;

namespace SpotifyFlyoutWPF.Pages;

public partial class SpotifyFlyoutPage : Page
{
    public SpotifyFlyoutPage()
    {
        InitializeComponent();
        DataContext = SettingsManager.Current;
    }
}