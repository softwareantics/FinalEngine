// <copyright file="IKeyboard.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

public interface IKeyboard
{
    bool IsAltDown { get; }

    bool IsCapsLocked { get; }

    bool IsControlDown { get; }

    bool IsNumLocked { get; }

    bool IsShiftDown { get; }

    bool IsKeyDown(Key key);

    bool IsKeyPressed(Key key);

    bool IsKeyReleased(Key key);

    internal void Update();
}
