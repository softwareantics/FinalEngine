// <copyright file="WPFKeyboardDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Framework.Input;

using System;
using FinalEngine.Input.Keyboards;
using OpenTK.Wpf;
using WKey = System.Windows.Input.Key;
using WKeyEventArgs = System.Windows.Input.KeyEventArgs;

internal sealed class WPFKeyboardDevice : IKeyboardDevice
{
    public event EventHandler<KeyEventArgs>? KeyDown;

    public event EventHandler<KeyEventArgs>? KeyUp;

    public void Initialize(GLWpfControl control)
    {
        ArgumentNullException.ThrowIfNull(control, nameof(control));

        control.PreviewKeyUp += this.Control_PreviewKeyUp;
        control.PreviewKeyDown += this.Control_PreviewKeyDown;
    }

    private static Key ConvertKey(WKey key)
    {
        return key switch
        {
            WKey.Space => Key.Space,
            WKey.OemQuotes => Key.Apostrophe,
            WKey.OemComma => Key.Comma,
            WKey.OemMinus => Key.Minus,
            WKey.OemPeriod => Key.Period,
            WKey.OemBackslash => Key.Slash,
            WKey.D0 => Key.D0,
            WKey.D1 => Key.D1,
            WKey.D2 => Key.D2,
            WKey.D3 => Key.D3,
            WKey.D4 => Key.D4,
            WKey.D5 => Key.D5,
            WKey.D6 => Key.D6,
            WKey.D7 => Key.D7,
            WKey.D8 => Key.D8,
            WKey.D9 => Key.D9,
            WKey.OemSemicolon => Key.Semicolon,
            WKey.A => Key.A,
            WKey.B => Key.B,
            WKey.C => Key.C,
            WKey.D => Key.D,
            WKey.E => Key.E,
            WKey.F => Key.F,
            WKey.G => Key.G,
            WKey.H => Key.H,
            WKey.I => Key.I,
            WKey.J => Key.J,
            WKey.K => Key.K,
            WKey.L => Key.L,
            WKey.M => Key.M,
            WKey.N => Key.N,
            WKey.O => Key.O,
            WKey.P => Key.P,
            WKey.Q => Key.Q,
            WKey.R => Key.R,
            WKey.S => Key.S,
            WKey.T => Key.T,
            WKey.U => Key.U,
            WKey.V => Key.V,
            WKey.W => Key.W,
            WKey.X => Key.X,
            WKey.Y => Key.Y,
            WKey.Z => Key.Z,
            WKey.OemOpenBrackets => Key.LeftBracket,
            WKey.OemCloseBrackets => Key.RightBracket,
            WKey.OemTilde => Key.GraveAccent,
            WKey.Escape => Key.Escape,
            WKey.Enter => Key.Enter,
            WKey.Tab => Key.Tab,
            WKey.Back => Key.Backspace,
            WKey.Insert => Key.Insert,
            WKey.Delete => Key.Delete,
            WKey.Right => Key.Right,
            WKey.Left => Key.Left,
            WKey.Down => Key.Down,
            WKey.Up => Key.Up,
            WKey.PageUp => Key.PageUp,
            WKey.PageDown => Key.PageDown,
            WKey.Home => Key.Home,
            WKey.End => Key.End,
            WKey.CapsLock => Key.CapsLock,
            WKey.Scroll => Key.ScrollLock,
            WKey.NumLock => Key.NumLock,
            WKey.PrintScreen => Key.PrintScreen,
            WKey.Pause => Key.Pause,
            WKey.F1 => Key.F1,
            WKey.F2 => Key.F2,
            WKey.F3 => Key.F3,
            WKey.F4 => Key.F4,
            WKey.F5 => Key.F5,
            WKey.F6 => Key.F6,
            WKey.F7 => Key.F7,
            WKey.F8 => Key.F8,
            WKey.F9 => Key.F9,
            WKey.F10 => Key.F10,
            WKey.F11 => Key.F11,
            WKey.F12 => Key.F12,
            WKey.F13 => Key.F13,
            WKey.F14 => Key.F14,
            WKey.F15 => Key.F15,
            WKey.F16 => Key.F16,
            WKey.F17 => Key.F17,
            WKey.F18 => Key.F18,
            WKey.F19 => Key.F19,
            WKey.F20 => Key.F20,
            WKey.F21 => Key.F21,
            WKey.F22 => Key.F22,
            WKey.F23 => Key.F23,
            WKey.F24 => Key.F24,
            WKey.NumPad0 => Key.KeyPad0,
            WKey.NumPad1 => Key.KeyPad1,
            WKey.NumPad2 => Key.KeyPad2,
            WKey.NumPad3 => Key.KeyPad3,
            WKey.NumPad4 => Key.KeyPad4,
            WKey.NumPad5 => Key.KeyPad5,
            WKey.NumPad6 => Key.KeyPad6,
            WKey.NumPad7 => Key.KeyPad7,
            WKey.NumPad8 => Key.KeyPad8,
            WKey.NumPad9 => Key.KeyPad9,
            WKey.Decimal => Key.KeyPadDecimal,
            WKey.Divide => Key.KeyPadDivide,
            WKey.Multiply => Key.KeyPadMultiply,
            WKey.Subtract => Key.KeyPadSubtract,
            WKey.Add => Key.KeyPadAdd,
            WKey.LeftShift => Key.LeftShift,
            WKey.LeftCtrl => Key.LeftControl,
            WKey.LeftAlt => Key.LeftAlt,
            WKey.LWin => Key.LeftSuper,
            WKey.RightShift => Key.RightShift,
            WKey.RightCtrl => Key.RightControl,
            WKey.RightAlt => Key.RightAlt,
            WKey.RWin => Key.RightSuper,
            _ => Key.Unknown,
        };
    }

    private void Control_PreviewKeyDown(object sender, WKeyEventArgs e)
    {
        this.KeyDown?.Invoke(this, new KeyEventArgs()
        {
            Key = ConvertKey(e.Key),
            Modifiers = KeyModifiers.None,
        });
    }

    private void Control_PreviewKeyUp(object sender, WKeyEventArgs e)
    {
        this.KeyUp?.Invoke(this, new KeyEventArgs()
        {
            Key = ConvertKey(e.Key),
            Modifiers = KeyModifiers.None,
        });
    }
}
