// <copyright file="INativeWindowAdapter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Adapters;

using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

internal interface INativeWindowAdapter : IDisposable
{
    event Action<KeyboardKeyEventArgs> KeyDown;

    event Action<KeyboardKeyEventArgs> KeyUp;

    event Action<MouseButtonEventArgs> MouseDown;

    event Action<MouseMoveEventArgs> MouseMove;

    event Action<MouseButtonEventArgs> MouseUp;

    event Action<MouseWheelEventArgs> MouseWheel;

    Vector2i ClientSize { get; set; }

    IGLFWGraphicsContext Context { get; }

    CursorState CursorState { get; set; }

    bool IsExiting { get; }

    bool IsVisible { get; set; }

    Vector2 MousePosition { get; set; }

    string Title { get; set; }

    void Close();

    bool ProcessEvents(double timeout);
}
