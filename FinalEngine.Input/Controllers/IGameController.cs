// <copyright file="IGameController.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Controllers;

public interface IGameController
{
    float GetAxis(int index, ControllerAxis axis);

    bool IsButtonDown(int index, ControllerButton button);

    bool IsButtonPressed(int index, ControllerButton button);

    bool IsButtonReleased(int index, ControllerButton button);
}
