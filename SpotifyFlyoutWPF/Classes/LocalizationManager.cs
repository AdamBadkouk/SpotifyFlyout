// Copyright © 2024-2026 The SpotifyFlyout Authors
// SPDX-License-Identifier: GPL-3.0-or-later

using SpotifyFlyout.Classes.Settings;
using System.Windows;

namespace SpotifyFlyout.Classes;

public static class LocalizationManager
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    // current language code - always English
    public static string LanguageCode { get; set; } = "en";

    public static void ApplyLocalization()
    {
        Logger.Debug("Applying English localization");

        // Set left-to-right flow direction
        SettingsManager.Current.FlowDirection = FlowDirection.LeftToRight;

        // Set default font family
        SettingsManager.Current.FontFamily = "Segoe UI Variable, Microsoft YaHei UI, Yu Gothic UI, Malgun Gothic";
    }
}