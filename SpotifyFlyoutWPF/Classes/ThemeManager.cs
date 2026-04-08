// Copyright © 2024-2026 The SpotifyFlyout Authors
// SPDX-License-Identifier: GPL-3.0-or-later

using SpotifyFlyout.Classes.Settings;
using SpotifyFlyoutWPF;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Helpers;
using MicaWPF.Core.Services;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using Wpf.Ui.Appearance;
using Wpf.Ui.Tray.Controls;

namespace SpotifyFlyout.Classes;

/// <summary>
/// Manages the application theme settings and applies the selected theme.
/// </summary>
internal static class ThemeManager
{
    private static bool _isInitialized;
    private static bool? _lastKnownDarkTheme;

    /// <summary>
    /// Applies the theme saved in the application settings. Used at application startup.
    /// </summary>
    /// <inheritdoc cref="ApplyTheme"/>
    public static void ApplySavedTheme()
    {
        // Subscribe to system theme changes to update tray icon
        if (!_isInitialized)
        {
            SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
            _isInitialized = true;
        }

        ApplyTheme(SettingsManager.Current.AppTheme);
        UpdateTrayIcon();
        UpdateTaskbarWidget();
    }

    private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (e.Category == UserPreferenceCategory.General)
        {
            // Check if theme actually changed
            bool currentDark = IsSystemDarkTheme();
            if (_lastKnownDarkTheme != currentDark)
            {
                _lastKnownDarkTheme = currentDark;
                UpdateTrayIcon();
            }
        }
    }

    /// <summary>
    /// Reads Windows theme directly from registry.
    /// </summary>
    private static bool IsSystemDarkTheme()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            var value = key?.GetValue("AppsUseLightTheme");
            return value is int i && i == 0;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Applies the specified theme and saves it to the application settings.
    /// </summary>
    /// <inheritdoc cref="ApplyTheme"/>
    public static void ApplyAndSaveTheme(int theme)
    {
        ApplyTheme(theme);
        SettingsManager.Current.AppTheme = theme;
        SettingsManager.SaveSettings();

        if (SettingsManager.Current.MediaFlyoutAcrylicWindowEnabled) { WindowBlurHelper.EnableBlur(Application.Current.MainWindow); }
        UpdateTaskbarWidget();
    }

    /// <summary>
    /// Applies the specified theme. See also <see href="https://github.com/Simnico99/MicaWPF/wiki/Change-Theme-or-Accent-color"/>.
    /// </summary>
    /// <param name="theme">The theme to apply. 1 for Light, 2 for Dark, 0 or any other value for System Default.</param>
    private static void ApplyTheme(int theme)
    {
        switch (theme)
        {
            case 1:
                UnWatchThemeChanges();
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                MicaWPFServiceUtility.ThemeService.ChangeTheme(WindowsTheme.Light);
                break;
            case 2:
                UnWatchThemeChanges();
                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                MicaWPFServiceUtility.ThemeService.ChangeTheme(WindowsTheme.Dark);
                break;
            default:
                WatchThemeChanges();
                ApplicationThemeManager.ApplySystemTheme();
                MicaWPFServiceUtility.ThemeService.ChangeTheme(/*WindowsTheme.Auto*/);
                break;
        }

        // refresh accent color to its counterpart after theme changes
        MicaWPFServiceUtility.AccentColorService.RefreshAccentsColors();
    }

    /// <summary>
    /// Starts watching for system theme changes and applies them automatically. (just a wrapper for <see cref="SystemThemeWatcher.Watch"/>)
    /// </summary>
    /// <remarks>This function was not necessary because the theme was managed by MicaWPF.</remarks>
    private static void WatchThemeChanges()
    {
        SystemThemeWatcher.Watch(Application.Current.MainWindow/*, WindowBackdropType.Mica, true*/);
    }

    /// <summary>
    /// Stops watching for system theme changes. (just a wrapper for <see cref="SystemThemeWatcher.UnWatch"/>)
    /// </summary>
    /// <remarks>This function was not necessary because the theme was managed by MicaWPF.</remarks>
    private static void UnWatchThemeChanges()
    {
        // check if window is loaded
        if (Application.Current.MainWindow.IsLoaded == false) return;

        SystemThemeWatcher.UnWatch(Application.Current.MainWindow);
    }

    /// <summary>
    /// Changes the tray icon according to the specified app theme and setting.
    /// </summary>
    public static void UpdateTrayIcon()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            if (Application.Current.MainWindow.FindName("nIcon") is NotifyIcon nIcon)
            {
                if (SettingsManager.Current.NIconSymbol == true)
                {
                    // Always follow Windows system theme for tray icon
                    bool isDarkTheme = IsSystemDarkTheme();
                    _lastKnownDarkTheme = isDarkTheme;

                    var iconUri = new Uri(isDarkTheme
                        ? "pack://application:,,,/Resources/TrayIcons/SpotifyFlyoutWhite.png"
                        : "pack://application:,,,/Resources/TrayIcons/SpotifyFlyoutBlack.png");
                    nIcon.Icon = new BitmapImage(iconUri);
                }
                else
                {
                    var iconUi = new Uri("pack://application:,,,/Resources/SpotifyFlyout.ico");
                    nIcon.Icon = new BitmapImage(iconUi);
                }

                // Force tray icon to refresh by re-registering
                if (!SettingsManager.Current.NIconHide && nIcon.IsRegistered)
                {
                    nIcon.Unregister();
                    nIcon.Register();
                }
            }
        });
    }

    /// <summary>
    /// Updates the taskbar widget theme to match the current Windows theme.
    /// </summary>
    public static void UpdateTaskbarWidget()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            if (Application.Current.MainWindow is not MainWindow mainWindow)
                return;

            mainWindow.taskbarWindow?.Widget?.ApplyWindowsTheme();
        });
    }
}