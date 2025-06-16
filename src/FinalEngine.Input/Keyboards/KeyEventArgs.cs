// <copyright file="KeyEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

using System;

public sealed class KeyEventArgs : EventArgs
{
    public bool Alt
    {
        get { return (this.Modifiers & KeyModifiers.Alt) != 0; }
    }

    public bool CapsLock
    {
        get { return (this.Modifiers & KeyModifiers.CapsLock) != 0; }
    }

    public bool Control
    {
        get { return (this.Modifiers & KeyModifiers.Control) != 0; }
    }

    public Key Key { get; init; }

    public KeyModifiers Modifiers { get; init; }

    public bool NumLock
    {
        get { return (this.Modifiers & KeyModifiers.NumLock) != 0; }
    }

    public bool Shift
    {
        get { return (this.Modifiers & KeyModifiers.Shift) != 0; }
    }

    public bool Super
    {
        get { return (this.Modifiers & KeyModifiers.Super) != 0; }
    }
}
