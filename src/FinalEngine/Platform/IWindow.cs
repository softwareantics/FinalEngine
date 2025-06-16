// <copyright file="IWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System;
using System.Drawing;

public enum WindowState
{
    Normal,

    Minimized,

    Maximized,

    Fullscreen,
}

public enum WindowStyle
{
    Fixed,

    Resizable,

    Borderless,
}

public interface IWindow : IDisposable
{
    Size ClientSize { get; set; }

    bool IsVisible { get; set; }

    WindowState State { get; set; }

    WindowStyle Style { get; set; }

    string Title { get; set; }

    void Close();
}
