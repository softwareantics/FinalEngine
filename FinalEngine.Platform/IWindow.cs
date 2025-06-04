// <copyright file="IWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System.Drawing;

/// <summary>
/// Enumerates the available states for a window.
/// </summary>
public enum WindowState
{
    /// <summary>
    /// The window is in its normal state and is neither minimized nor maximized.
    /// </summary>
    Normal,

    /// <summary>
    /// The window is minimized, typically to an icon in the taskbar.
    /// </summary>
    Minimized,

    /// <summary>
    /// The window is maximized, filling the screen except for the taskbar.
    /// </summary>
    Maximized,

    /// <summary>
    /// The window occupies the full screen and hides the taskbar and other windows.
    /// </summary>
    Fullscreen,
}

/// <summary>
/// Enumerates the available border styles for a window.
/// </summary>
public enum WindowStyle
{
    /// <summary>
    /// The window cannot be resized by the user.
    /// </summary>
    ///
    /// <remarks>
    /// This style usually includes a title bar and system buttons, but the dimensions are fixed.
    /// </remarks>
    Fixed,

    /// <summary>
    /// The window can be resized by the user.
    /// </summary>
    ///
    /// <remarks>
    /// This style allows resizing via the window’s edges or corners and typically includes system controls.
    /// </remarks>
    Resizable,

    /// <summary>
    /// The window has no borders or title bar.
    /// </summary>
    ///
    /// <remarks>
    /// Commonly used for immersive or custom UIs. This style should not restrict platform-expected window interactions like moving or resizing via system gestures or methods.
    /// </remarks>
    Borderless,
}

/// <summary>
/// Defines an interface that represents a window that can be displayed on the screen.
/// </summary>
///
/// <remarks>
/// This interface provides properties and methods to manage a window's appearance, size, state, and visibility. The window can be closed, and it supports various styles and states that can be set or retrieved. Implementations of this interface should ensure that the window behaves consistently across different platforms and environments. The interface also inherits from <see cref="IDisposable"/>, allowing for proper resource management and cleanup when the window is no longer needed.
/// </remarks>
public interface IWindow : IDisposable
{
    /// <summary>
    /// Gets or sets a <see cref="Size"/> that represents the size of the client area.
    /// </summary>
    ///
    /// <value>
    /// Specifies a <see cref="Size"/> that represents the dimensions of the client area inside the window, excluding borders and the title bar.
    /// </value>
    Size ClientSize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the window can be resized manually by the user.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if this <see cref="IWindow"/> is user re-sizable; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <remarks>
    /// If this value is <c>false</c>, the user will not be able to resize the window by dragging its edges or corners.
    /// </remarks>
    bool IsUserReSizable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the window is visible.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if this <see cref="IWindow"/> is visible; otherwise, <c>false</c>.
    /// </value>
    bool IsVisible { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="WindowState"/> that represents the current state of the window.
    /// </summary>
    ///
    /// <value>
    /// Specifies a <see cref="WindowState"/> that represents whether the window is normal, minimized, maximized, or full-screen.
    /// </value>
    ///
    /// <remarks>
    /// When set to <see cref="WindowState.Fullscreen"/>, the window should occupy the entire screen, hiding the taskbar and other windows. The <see cref="WindowState.Maximized"/> state should fill the screen except for the taskbar. <see cref="WindowState.Normal"/> represents the default size and position, and <see cref="WindowState.Minimized"/> minimizes the window to an icon in the task-bar. Switching from <see cref="WindowState.Fullscreen"/> to <see cref="WindowState.Normal"/> should resize the client area back to the original client size prior to setting the state to <see cref="WindowState.Fullscreen"/>. This behavior is expected within implementations of <see cref="IWindow"/>.
    /// </remarks>
    WindowState State { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="WindowStyle"/> that represents the style of the window.
    /// </summary>
    ///
    /// <value>
    /// Specifies a <see cref="WindowStyle"/> that represents the window's border style.
    /// </value>
    ///
    /// <remarks>
    /// The <see cref="WindowStyle.Fixed"/> style prevents user resizing and typically includes a title bar and system buttons. <see cref="WindowStyle.Resizable"/> allows dynamic resizing via the window edges or corners. <see cref="WindowStyle.Borderless"/> removes window decorations and is often used for immersive or custom interfaces.
    /// </remarks>
    WindowStyle Style { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="string"/> that represents the title of the window.
    /// </summary>
    ///
    /// <value>
    /// Specifies a <see cref="string"/> that represents the title text displayed in the window's title bar.
    /// </value>
    ///
    /// <remarks>
    /// On desktop systems, the title typically appears in the title bar. On mobile or alternate platforms, this may reflect the application name shown in switchers or task managers.
    /// </remarks>
    string Title { get; set; }

    /// <summary>
    /// Closes the window and releases any associated resources.
    /// </summary>
    void Close();
}
