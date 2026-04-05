## SpotifyFlyout

A modern, lightweight Spotify controller for Windows 11 with Fluent 2 Design.

---

## About

SpotifyFlyout is a media controller that blends seamlessly with Windows 11. It provides a clean, native-like experience for controlling your Spotify playback directly from your taskbar with smooth animations and Mica blur effects.

> **Note:** This project is a fork of the open-source [FluentFlyout](https://github.com/unchihugo/FluentFlyout).

## Features

- **Spotify Exclusive** — Built solely for Spotify.
- **Modern Flyout** — Displays album art, track title, artist, and playback controls.
- **Native Windows 11 Design** — Uses Fluent 2 components and Windows Mica blur effects.
- **System Integration** — Matches your system color theme (Light and Dark mode support).
- **Full Playback Control** — Includes Play/Pause, Skip, Previous, Repeat, Repeat One, and Shuffle.
- **Portable & Lightweight** — Runs quietly in the system tray with no heavy background services.

## Requirements

- Windows 10/11
- Spotify Desktop App
- .NET Desktop Runtime (v10)

## How to Install

### Using the Release (Recommended)
1. Go to the [Releases](https://github.com/AdamBadkouk/SpotifyFlyout/releases) page.
2. Download the latest `SpotifyFlyout.zip` file.
3. Extract the folder and double-click `SpotifyFlyout.exe` to run it! (No installation required).

> **Note about Windows SmartScreen:** Windows might show a warning "Windows protected your PC" the first time you run it. Simply click **More info** -> **Run anyway**. 

> **Tip:** You can access the Settings and About pages by right-clicking the SpotifyFlyout app icon in your system tray.

### Build from Source

First, clone the repository to your local machine:
```
git clone https://github.com/AdamBadkouk/SpotifyFlyout.git
cd SpotifyFlyout
```

**Option 1: Using Visual Studio**
1. Open `SpotifyFlyout.sln` in Visual Studio.
3. Press `F5` or click **Start** to build and run.

**Option 2: Using the .NET CLI**
1. To build the project, run: `dotnet build`
2. To run the application, run: `dotnet run --project SpotifyFlyoutWPF`

## Dependencies

- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)
- [Dubya.WindowsMediaController](https://github.com/DubyaDude/WindowsMediaController)
- [MicaWPF](https://github.com/Simnico99/MicaWPF)
- [Microsoft.Toolkit.Uwp.Notifications](https://github.com/CommunityToolkit/WindowsCommunityToolkit)
- [NAudio](https://github.com/naudio/NAudio)
- [NLog](https://github.com/NLog/NLog)
- [WPF-UI](https://github.com/lepoco/wpfui)
- [WPF-UI.Tray](https://github.com/lepoco/wpfui)

## License

This project is licensed under the **GNU General Public License v3.0 (GPL-3.0)**, adopting the same terms as the original FluentFlyout project. See the [LICENSE](LICENSE) file for details.
