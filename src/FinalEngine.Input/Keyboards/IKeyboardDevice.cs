// <copyright file="IKeyboardDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

using System;

public interface IKeyboardDevice : IDisposable
{
    event EventHandler<KeyEventArgs>? KeyDown;

    event EventHandler<KeyEventArgs>? KeyUp;
}
