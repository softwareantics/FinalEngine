// <copyright file="ICamera.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Cameras;

using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Components;

public interface ICamera
{
    Rectangle Bounds { get; }

    Matrix4x4 Projection { get; }

    TransformComponent Transform { get; }

    Matrix4x4 View { get; }
}
