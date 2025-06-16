// <copyright file="IMouseDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System;
using System.Drawing;

public interface IMouseDevice : IDisposable
{
    event EventHandler<MouseButtonEventArgs>? ButtonDown;

    event EventHandler<MouseButtonEventArgs>? ButtonUp;

    event EventHandler<MouseMoveEventArgs>? Move;

    event EventHandler<MouseScrollEventArgs>? Scroll;

    bool ShowCursor { get; set; }

    void SetCursorLocation(PointF location);
}
