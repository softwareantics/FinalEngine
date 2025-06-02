// <copyright file="IWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System.Drawing;

/// <summary>
///   Enumerates the available window states for a window.
/// </summary>
public enum WindowState
{
    /// <summary>
    ///   The window is in its normal state, neither minimized nor maximized.
    /// </summary>
    Normal,

    /// <summary>
    ///   The window is minimized, typically represented by an icon in the task-bar.
    /// </summary>
    Minimized,

    /// <summary>
    ///   The window is maximized, filling the entire screen except for the task-bar.
    /// </summary>
    Maximized,

    /// <summary>
    ///   The window is in full-screen mode, occupying the entire screen and hiding the task-bar and other windows.
    /// </summary>
    Fullscreen,
}

/// <summary>
///   Enumerates the available border styles for a window.
/// </summary>
public enum WindowStyle
{
    /// <summary>
    ///   The window has a fixed size and cannot be resized by the user.
    /// </summary>
    /// <remarks>
    ///   This style typically includes a title bar and system buttons, but the user cannot change the window's dimensions.
    /// </remarks>
    Fixed,

    /// <summary>
    ///   The window can be resized by the user, allowing for dynamic changes to its dimensions.
    /// </summary>
    /// <remarks>
    ///   This style usually includes a title bar and system buttons, and the user can drag the edges or corners to resize the window.
    /// </remarks>
    Resizable,

    /// <summary>
    ///   The window has no borders or title bar, allowing for a clean, unobtrusive appearance.
    /// </summary>
    /// <remarks>
    ///   This style is often used for games that require a custom border interface or when the window's appearance should not distract from the content. Please note that this style should not limit the user's ability to move or resize the window using standard methods, regardless of the platform and/or implementation.
    /// </remarks>
    Borderless,
}

/// <summary>
///   Defines an interface that represents a window that can be displayed on the screen.
/// </summary>
/// <remarks>
///   This interface provides properties and methods to manage the window's appearance, size, state, and visibility. The window can be closed, and it supports various styles and states that can be set or retrieved. Implementations of this interface should ensure that the window behaves consistently across different platforms and environments. The interface also inherits from <see cref="IDisposable"/>, allowing for proper resource management and cleanup when the window is no longer needed.
/// </remarks>
/// <seealso cref="IDisposable"/>
public interface IWindow : IDisposable
{
    /// <summary>
    ///   Gets or sets the size of the client (the area inside the window excluding borders and title bar).
    /// </summary>
    /// <value>
    ///   The size of the client (the area inside the window excluding the borders and title bar).
    /// </value>
    Size ClientSize { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether this <see cref="IWindow"/> can be resized manually by the user.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="IWindow"/> is user re-sizable; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    ///   The value of this property determines whether the user can change the size of the window by dragging its edges or corners. If set to <c>false</c>, the window will maintain a fixed size and cannot be resized by the user.
    /// </remarks>
    bool IsUserReSizable { get; set; }

    /// <summary>
    ///   Gets or sets a value indicating whether this <see cref="IWindow"/> is visible.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="IWindow"/> is visible; otherwise, <c>false</c>.
    /// </value>
    bool IsVisible { get; set; }

    /// <summary>
    ///   Gets or sets the state of the window (e.g., normal, minimized, maximized, full-screen).
    /// </summary>
    /// <value>
    ///   The state of the window (e.g., normal, minimized, maximized, full-screen).
    /// </value>
    /// <remarks>
    ///   When set to <see cref="WindowState.Fullscreen"/>, the window should occupy the entire screen, hiding the task-bar and other windows. When set to <see cref="WindowState.Maximized"/>, the window should fill the screen except for the task-bar. The <see cref="WindowState.Normal"/> state indicates that the window is in its default size and position, while <see cref="WindowState.Minimized"/> indicates that the window is minimized to an icon in the task-bar. When the user changes the window from <see cref="WindowState.Fullscreen"/> to <see cref="WindowState.Normal"/> the desired behavior is not to resize the client area of the display. This functionality is not outside the scope of <see cref="IWindow"/>.
    /// </remarks>
    WindowState State { get; set; }

    /// <summary>
    ///   Gets or sets the style of the window (e.g., fixed, re-sizable, border-less).
    /// </summary>
    /// <value>
    ///   The style of the window (e.g., fixed, re-sizable, border-less).
    /// </value>
    /// <remarks>
    ///   When set to <see cref="WindowStyle.Fixed"/>, the window cannot be resized by the user, and should include a title bar and system buttons. When set to <see cref="WindowStyle.Resizable"/>, the user can change the size of the window by dragging its edges or corners. The <see cref="WindowStyle.Borderless"/> style indicates that the window has no borders or title bar, allowing for a clean, unobtrusive appearance. This style is often used for applications that require a custom border interface or when the window's appearance should not distract from the content.
    /// </remarks>
    WindowStyle Style { get; set; }

    /// <summary>
    ///   Gets or sets the title of the <see cref="IWindow"/>.
    /// </summary>
    /// <value>
    ///   The title of the <see cref="IWindow"/>.
    /// </value>
    /// <remarks>
    ///   The title is typically displayed in the title bar of the window. If developing for other platforms such as mobile this would be the name of the application as it appears in the application switcher or task manager.
    /// </remarks>
    string Title { get; set; }

    /// <summary>
    ///   Closes this <see cref="IWindow"/> and releases any resources associated with it.
    /// </summary>
    void Close();
}
