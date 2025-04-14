// <copyright file="InputDriver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input;

using System;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;

internal sealed class InputDriver : IInputDriver
{
    private readonly IKeyboard keyboard;

    private readonly IMouse mouse;

    public InputDriver(IKeyboard keyboard, IMouse mouse)
    {
        this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
        this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
    }

    public void Update()
    {
        this.keyboard.Update();
        this.mouse.Update();
    }
}
