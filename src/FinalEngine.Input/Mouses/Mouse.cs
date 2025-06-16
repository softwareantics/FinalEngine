// <copyright file="Mouse.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System;
using System.Collections.Generic;
using System.Drawing;

internal sealed class Mouse : IMouse, IDisposable
{
    private readonly List<MouseButton> buttonsDown;

    private readonly IMouseDevice? device;

    private List<MouseButton> buttonsDownLast;

    private bool isDisposed;

    private PointF location;

    public Mouse(IMouseDevice? device)
    {
        this.device = device;

        this.buttonsDown = [];
        this.buttonsDownLast = [];

        if (this.device == null)
        {
            return;
        }

        this.device.ButtonDown += this.Device_ButtonDown;
        this.device.ButtonUp += this.Device_ButtonUp;
        this.device.Move += this.Device_Move;
        this.device.Scroll += this.Device_Scroll;
    }

    ~Mouse()
    {
        this.Dispose(false);
    }

    public PointF Location
    {
        get
        {
            return this.location;
        }

        set
        {
            if (this.Location.Equals(value))
            {
                return;
            }

            this.location = value;
            this.device?.SetCursorLocation(value);
        }
    }

    public bool Visible
    {
        get
        {
            return this.device?.ShowCursor ?? false;
        }

        set
        {
            if (this.device == null)
            {
                return;
            }

            this.device.ShowCursor = value;
        }
    }

    public double WheelOffset { get; private set; }

    public void Clear()
    {
        this.buttonsDown.Clear();
        this.buttonsDownLast.Clear();
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public bool IsButtonDown(MouseButton button)
    {
        return this.buttonsDown.Contains(button);
    }

    public bool IsButtonPressed(MouseButton button)
    {
        return this.buttonsDown.Contains(button) && !this.buttonsDownLast.Contains(button);
    }

    public bool IsButtonReleased(MouseButton button)
    {
        return !this.buttonsDown.Contains(button) && this.buttonsDownLast.Contains(button);
    }

    public void Update()
    {
        this.buttonsDownLast = [.. this.buttonsDown];
    }

    private void Device_ButtonDown(object? sender, MouseButtonEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
        this.buttonsDown.Add(e.Button);
    }

    private void Device_ButtonUp(object? sender, MouseButtonEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);

        while (this.buttonsDown.Contains(e.Button))
        {
            this.buttonsDown.Remove(e.Button);
        }
    }

    private void Device_Move(object? sender, MouseMoveEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
        this.location = e.Location;
    }

    private void Device_Scroll(object? sender, MouseScrollEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
        this.WheelOffset = e.Offset;
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.device != null)
        {
            this.device.ButtonDown -= this.Device_ButtonDown;
            this.device.ButtonUp -= this.Device_ButtonUp;
            this.device.Move -= this.Device_Move;
            this.device.Scroll -= this.Device_Scroll;
        }

        this.buttonsDown.Clear();
        this.buttonsDownLast.Clear();

        this.isDisposed = true;
    }
}
