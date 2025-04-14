// <copyright file="OpenTKGameController.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK;

using FinalEngine.Input.Controllers;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;

internal sealed class OpenTKGameController : IGameController
{
    private readonly INativeWindowInvoker window;

    public OpenTKGameController(INativeWindowInvoker window)
    {
        this.window = window ?? throw new System.ArgumentNullException(nameof(window));
    }

    public float GetAxis(int index, ControllerAxis axis)
    {
        if (!(this.window.JoystickStates.Count >= index))
        {
            return 0.0f;
        }

        return this.window.JoystickStates[index].GetAxis((int)axis);
    }

    public bool IsButtonDown(int index, ControllerButton button)
    {
        if (!(this.window.JoystickStates.Count >= index))
        {
            return false;
        }

        return this.window.JoystickStates[index].IsButtonDown((int)button);
    }

    public bool IsButtonPressed(int index, ControllerButton button)
    {
        if (!(this.window.JoystickStates.Count >= index))
        {
            return false;
        }

        return this.window.JoystickStates[index].IsButtonPressed((int)button);
    }

    public bool IsButtonReleased(int index, ControllerButton button)
    {
        if (!(this.window.JoystickStates.Count >= index))
        {
            return false;
        }

        return this.window.JoystickStates[index].IsButtonReleased((int)button);
    }
}
