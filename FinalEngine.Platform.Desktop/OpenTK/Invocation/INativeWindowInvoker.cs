// <copyright file="INativeWindowInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK.Invocation;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using global::OpenTK.Mathematics;
using global::OpenTK.Windowing.Common;
using global::OpenTK.Windowing.Desktop;
using global::OpenTK.Windowing.GraphicsLibraryFramework;

[SuppressMessage("Design", "CA1003:Use generic event handler instances", Justification = "Required by Invocation")]
internal interface INativeWindowInvoker : IDisposable
{
    event Action<KeyboardKeyEventArgs> KeyDown;

    event Action<KeyboardKeyEventArgs> KeyUp;

    event Action<MouseButtonEventArgs> MouseDown;

    event Action<MouseMoveEventArgs> MouseMove;

    event Action<MouseButtonEventArgs> MouseUp;

    event Action<MouseWheelEventArgs> MouseWheel;

    Vector2i ClientSize { get; }

    IGLFWGraphicsContext Context { get; }

    bool IsDisposed { get; }

    bool IsExiting { get; }

    bool IsFocused { get; }

    bool IsVisible { get; set; }

    IReadOnlyList<JoystickState> JoystickStates { get; }

    Vector2 MousePosition { get; set; }

    IMouseStateInvoker MouseState { get; }

    Vector2i Size { get; set; }

    string Title { get; set; }

    void Close();

    void ProcessEvents();
}
