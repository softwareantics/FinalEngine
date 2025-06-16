// <copyright file="OpenTKMouseDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System;
using System.Drawing;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform.Adapters;
using OpenTK.Mathematics;
using TKCursorState = OpenTK.Windowing.Common.CursorState;
using TKMouseButtonEventArgs = OpenTK.Windowing.Common.MouseButtonEventArgs;
using TKMouseMoveEventArgs = OpenTK.Windowing.Common.MouseMoveEventArgs;
using TKMouseWheelEventArgs = OpenTK.Windowing.Common.MouseWheelEventArgs;

internal sealed class OpenTKMouseDevice : IMouseDevice
{
    private readonly INativeWindowAdapter nativeWindow;

    private bool isDisposed;

    public OpenTKMouseDevice(INativeWindowAdapter nativeWindow)
    {
        this.nativeWindow = nativeWindow ?? throw new ArgumentNullException(nameof(nativeWindow));

        this.nativeWindow.MouseUp += this.NativeWindow_MouseUp;
        this.nativeWindow.MouseDown += this.NativeWindow_MouseDown;
        this.nativeWindow.MouseMove += this.NativeWindow_MouseMove;
        this.nativeWindow.MouseWheel += this.NativeWindow_MouseWheel;
    }

    ~OpenTKMouseDevice()
    {
        this.Dispose(false);
    }

    public event EventHandler<MouseButtonEventArgs>? ButtonDown;

    public event EventHandler<MouseButtonEventArgs>? ButtonUp;

    public event EventHandler<MouseMoveEventArgs>? Move;

    public event EventHandler<MouseScrollEventArgs>? Scroll;

    public bool ShowCursor
    {
        get { return this.nativeWindow.CursorState != TKCursorState.Hidden; }
        set { this.nativeWindow.CursorState = value ? TKCursorState.Normal : TKCursorState.Hidden; }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void SetCursorLocation(PointF location)
    {
        this.nativeWindow.MousePosition = new Vector2(location.X, location.Y);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            this.nativeWindow.MouseUp -= this.NativeWindow_MouseUp;
            this.nativeWindow.MouseDown -= this.NativeWindow_MouseDown;
            this.nativeWindow.MouseMove -= this.NativeWindow_MouseMove;
            this.nativeWindow.MouseWheel -= this.NativeWindow_MouseWheel;
        }

        this.isDisposed = true;
    }

    private void NativeWindow_MouseDown(TKMouseButtonEventArgs args)
    {
        this.ButtonDown?.Invoke(this, new MouseButtonEventArgs()
        {
            Button = (MouseButton)args.Button,
        });
    }

    private void NativeWindow_MouseMove(TKMouseMoveEventArgs args)
    {
        this.Move?.Invoke(this, new MouseMoveEventArgs()
        {
            Location = new PointF(args.X, args.Y),
        });
    }

    private void NativeWindow_MouseUp(TKMouseButtonEventArgs args)
    {
        this.ButtonUp?.Invoke(this, new MouseButtonEventArgs()
        {
            Button = (MouseButton)args.Button,
        });
    }

    private void NativeWindow_MouseWheel(TKMouseWheelEventArgs args)
    {
        this.Scroll?.Invoke(this, new MouseScrollEventArgs()
        {
            Offset = args.OffsetY,
        });
    }
}
