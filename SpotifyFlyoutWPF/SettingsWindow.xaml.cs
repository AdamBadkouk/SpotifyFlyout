// Copyright © 2024-2026 The SpotifyFlyout Authors
// SPDX-License-Identifier: GPL-3.0-or-later

using SpotifyFlyout.Classes;
using SpotifyFlyout.Classes.Settings;
using SpotifyFlyoutWPF.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Wpf.Ui.Controls;

namespace SpotifyFlyoutWPF;

public partial class SettingsWindow : FluentWindow
{
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    private static SettingsWindow? instance;
    private Type? _currentPageType;
    private ScrollViewer? _contentScrollViewer;

    public SettingsWindow()
    {
        if (instance != null)
        {
            if (instance.WindowState == WindowState.Minimized)
            {
                instance.WindowState = WindowState.Normal;
            }

            instance.Activate();
            instance.Focus();
            Close();
            return;
        }

        InitializeComponent();
        instance = this;

        Closed += (s, e) => instance = null;
        DataContext = SettingsManager.Current;
    }

    public static void ShowInstance(string? navigationPage = null)
    {
        try
        {
            Logger.Info("ShowInstance called");
            if (instance == null)
            {
                Logger.Info("Creating new SettingsWindow instance");
                new SettingsWindow().Show();
                instance?.Activate();
            }
            else
            {
                Logger.Info("Activating existing SettingsWindow instance");
                if (instance.WindowState == WindowState.Minimized)
                {
                    instance.WindowState = WindowState.Normal;
                }

                instance.Activate();
                instance.Focus();
            }

            if (navigationPage != null)
            {
                var pageType = System.Reflection.Assembly
                    .GetExecutingAssembly()
                    .GetType($"SpotifyFlyoutWPF.Pages.{navigationPage}");
                if (pageType != null)
                    NavigateToPage(pageType);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error in ShowInstance");
            System.Windows.MessageBox.Show($"Error opening settings: {ex.Message}", "SpotifyFlyout Error");
        }
    }

    public static void NavigateToPage(Type pageType)
    {
        instance?.RootNavigation.Navigate(pageType);
    }

    private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Ensure template is applied before any pane operations
        RootNavigation.ApplyTemplate();
        
        _currentPageType = typeof(SpotifyFlyoutPage);
        RootNavigation.Navigate(_currentPageType);

        // Set pane open after template is applied
        Dispatcher.BeginInvoke(new Action(() =>
        {
            try
            {
                RootNavigation.IsPaneOpen = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error setting IsPaneOpen");
            }
        }), System.Windows.Threading.DispatcherPriority.Loaded);

        RootNavigation.Navigated += (s, args) =>
        {
            _currentPageType = args.Page?.GetType();
            ResetScrollPosition();
        };

        SettingsManager.Current.PropertyChanged += (s, args) =>
        {
            if (args.PropertyName == nameof(SettingsManager.Current.AppTheme))
            {
                // Re-navigate to refresh the page after theme change
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        RootNavigation.Navigate(typeof(SpotifyFlyoutPage));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, "Error navigating after theme change");
                    }
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
        };
    }

    private void SettingsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        SettingsManager.SaveSettings();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        SettingsManager.SaveSettings();
        Close();
    }

    private void ResetScrollPosition()
    {
        Dispatcher.BeginInvoke(new Action(() =>
        {
            try
            {
                _contentScrollViewer ??= FindScrollableScrollViewer(RootNavigation);

                if (_contentScrollViewer != null)
                {
                    _contentScrollViewer.ScrollToVerticalOffset(0);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error resetting scroll position in SettingsWindow");
            }
        }), System.Windows.Threading.DispatcherPriority.Loaded);
    }

    // helper functions to traverse visual tree

    private static T? FindChildByName<T>(DependencyObject parent, string name) where T : FrameworkElement
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T typedChild && typedChild.Name == name)
            {
                return typedChild;
            }

            var result = FindChildByName<T>(child, name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    private static ScrollViewer? FindScrollableScrollViewer(DependencyObject parent)
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is ScrollViewer sv && sv.ScrollableHeight > 0)
            {
                return sv;
            }

            var result = FindScrollableScrollViewer(child);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    private static T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T typedChild)
            {
                return typedChild;
            }

            var result = FindVisualChild<T>(child);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    private void NavigationViewItem_Click(object sender, RoutedEventArgs e)
    {

    }
}
