// <copyright file="OpenTKKeyboardDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System;
using FinalEngine.Input.Keyboards;
using FinalEngine.Platform.Adapters;
using OpenTK.Windowing.Common;

internal sealed class OpenTKKeyboardDevice : IKeyboardDevice
{
    private readonly INativeWindowAdapter nativeWindow;

    private bool isDisposed;

    public OpenTKKeyboardDevice(INativeWindowAdapter nativeWindow)
    {
        this.nativeWindow = nativeWindow ?? throw new ArgumentNullException(nameof(nativeWindow));

        this.nativeWindow.KeyUp += this.NativeWindow_KeyUp;
        this.nativeWindow.KeyDown += this.NativeWindow_KeyDown;
    }

    ~OpenTKKeyboardDevice()
    {
        this.Dispose(false);
    }

    public event EventHandler<KeyEventArgs>? KeyDown;

    public event EventHandler<KeyEventArgs>? KeyUp;

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            this.nativeWindow.KeyUp -= this.NativeWindow_KeyUp;
            this.nativeWindow.KeyDown -= this.NativeWindow_KeyDown;
        }

        this.isDisposed = true;
    }

    private void NativeWindow_KeyDown(KeyboardKeyEventArgs args)
    {
        this.KeyDown?.Invoke(this, new KeyEventArgs()
        {
            Key = (Key)args.Key,
            Modifiers = (KeyModifiers)args.Modifiers,
        });
    }

    private void NativeWindow_KeyUp(KeyboardKeyEventArgs args)
    {
        this.KeyUp?.Invoke(this, new KeyEventArgs()
        {
            Key = (Key)args.Key,
            Modifiers = (KeyModifiers)args.Modifiers,
        });
    }
}
