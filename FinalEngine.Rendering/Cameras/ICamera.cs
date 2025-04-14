// <copyright file="ICamera.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Cameras;

using System.Drawing;
using System.Numerics;

public struct Camera
{
    public Matrix4x4 Projection { get; set; }

    public Matrix4x4 Transform { get; set; }

    public Matrix4x4 View { get; set; }

    public Rectangle Viewport { get; set; }
}
