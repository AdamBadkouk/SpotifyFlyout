// Copyright © 2024-2026 The SpotifyFlyout Authors
// SPDX-License-Identifier: GPL-3.0-or-later

using System.Windows.Media;

namespace SpotifyFlyout.Classes.Utils;

/// <summary>
/// Simplified media player data - hardcoded for Spotify only.
/// </summary>
public static class MediaPlayerData
{
    /// <summary>
    /// Returns hardcoded Spotify info. Icon extraction removed since we only support Spotify.
    /// </summary>
    public static (string, ImageSource?) getMediaPlayerData(string mediaPlayerId)
    {
        // Always return "Spotify" - we don't need dynamic detection anymore
        return ("Spotify", null);
    }
}