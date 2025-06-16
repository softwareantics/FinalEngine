// <copyright file="IMouse.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System.Drawing;

public interface IMouse
{
    PointF Location { get; set; }

    bool Visible { get; set; }

    double WheelOffset { get; }

    void Clear();

    bool IsButtonDown(MouseButton button);

    bool IsButtonPressed(MouseButton button);

    bool IsButtonReleased(MouseButton button);

    internal void Update();
}
