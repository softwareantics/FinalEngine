// <copyright file="Keyboard.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

using System;
using System.Collections.Generic;

internal sealed class Keyboard : IKeyboard, IDisposable
{
    private readonly IKeyboardDevice? device;

    private readonly List<Key> keysDown;

    private bool isDisposed;

    private List<Key> keysDownLast;

    public Keyboard(IKeyboardDevice? device)
    {
        this.device = device;

        this.keysDown = [];
        this.keysDownLast = [];

        if (this.device == null)
        {
            return;
        }

        this.device.KeyDown += this.Device_KeyDown;
        this.device.KeyUp += this.Device_KeyUp;
    }

    ~Keyboard()
    {
        this.Dispose(false);
    }

    public bool IsAltDown
    {
        get { return this.keysDown.Contains(Key.LeftAlt) || this.keysDown.Contains(Key.RightAlt); }
    }

    public bool IsCapsLocked { get; private set; }

    public bool IsControlDown
    {
        get { return this.keysDown.Contains(Key.LeftControl) || this.keysDown.Contains(Key.RightControl); }
    }

    public bool IsNumLocked { get; private set; }

    public bool IsShiftDown
    {
        get { return this.keysDown.Contains(Key.LeftShift) || this.keysDown.Contains(Key.RightShift); }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public bool IsKeyDown(Key key)
    {
        return this.keysDown.Contains(key);
    }

    public bool IsKeyPressed(Key key)
    {
        return this.keysDown.Contains(key) && !this.keysDownLast.Contains(key);
    }

    public bool IsKeyReleased(Key key)
    {
        return !this.keysDown.Contains(key) && this.keysDownLast.Contains(key);
    }

    public void Update()
    {
        this.keysDownLast = [.. this.keysDown];
    }

    private void Device_KeyDown(object? sender, KeyEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);

        this.IsCapsLocked = e.CapsLock;
        this.IsNumLocked = e.NumLock;

        this.keysDown.Add(e.Key);
    }

    private void Device_KeyUp(object? sender, KeyEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);

        while (this.keysDown.Contains(e.Key))
        {
            this.keysDown.Remove(e.Key);
        }
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.device != null)
        {
            this.device.KeyDown -= this.Device_KeyDown;
            this.device.KeyUp -= this.Device_KeyUp;
        }

        this.keysDown.Clear();
        this.keysDownLast.Clear();

        this.isDisposed = true;
    }
}
