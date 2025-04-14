// <copyright file="WPFMouseDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Framework.Input;

using System;
using System.Drawing;
using System.Windows;
using FinalEngine.Editor.Desktop.External;
using FinalEngine.Input.Mouses;
using OpenTK.Wpf;
using WMouseButton = System.Windows.Input.MouseButton;
using WMouseButtonEventArgs = System.Windows.Input.MouseButtonEventArgs;
using WMouseEventArgs = System.Windows.Input.MouseEventArgs;
using WMouseWheelEventArgs = System.Windows.Input.MouseWheelEventArgs;

internal sealed class WPFMouseDevice : IMouseDevice
{
    public event EventHandler<MouseButtonEventArgs>? ButtonDown;

    public event EventHandler<MouseButtonEventArgs>? ButtonUp;

    public event EventHandler<MouseMoveEventArgs>? Move;

    public event EventHandler<MouseScrollEventArgs>? Scroll;

    public void Initialize(GLWpfControl control)
    {
        ArgumentNullException.ThrowIfNull(control, nameof(control));

        control.PreviewMouseUp += this.Control_PreviewMouseUp;
        control.PreviewMouseDown += this.Control_PreviewMouseDown;
        control.PreviewMouseMove += this.Control_PreviewMouseMove;
        control.PreviewMouseWheel += this.Control_PreviewMouseWheel;
    }

    public void SetCursorLocation(PointF location)
    {
        Win32Native.SetCursorPos((int)location.X, (int)location.Y);
    }

    private static MouseButton ConvertButton(WMouseButton button)
    {
        return button switch
        {
            WMouseButton.Left => MouseButton.Left,
            WMouseButton.Right => MouseButton.Right,
            WMouseButton.Middle => MouseButton.Middle,
            WMouseButton.XButton1 => MouseButton.Button4,
            WMouseButton.XButton2 => MouseButton.Button5,
            _ => MouseButton.Unknown,
        };
    }

    private void Control_PreviewMouseDown(object sender, WMouseButtonEventArgs e)
    {
        this.ButtonDown?.Invoke(this, new MouseButtonEventArgs()
        {
            Button = ConvertButton(e.ChangedButton),
        });
    }

    private void Control_PreviewMouseMove(object sender, WMouseEventArgs e)
    {
        var position = e.GetPosition(Application.Current.MainWindow);

        this.Move?.Invoke(this, new MouseMoveEventArgs()
        {
            Location = new PointF((float)position.X, (float)position.Y),
        });
    }

    private void Control_PreviewMouseUp(object sender, WMouseButtonEventArgs e)
    {
        this.ButtonUp?.Invoke(this, new MouseButtonEventArgs()
        {
            Button = ConvertButton(e.ChangedButton),
        });
    }

    private void Control_PreviewMouseWheel(object sender, WMouseWheelEventArgs e)
    {
        this.Scroll?.Invoke(this, new MouseScrollEventArgs()
        {
            Offset = e.Delta,
        });
    }
}
