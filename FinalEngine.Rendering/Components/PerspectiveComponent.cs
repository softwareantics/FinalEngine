// <copyright file="PerspectiveComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Components;

using System.ComponentModel;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.Maths;

[Category("Rendering")]
public class PerspectiveComponent : IEntityComponent
{
    public float AspectRatio { get; set; } = 1280.0f / 720.0f;

    public float FarPlaneDistance { get; set; } = 10000.0f;

    public float FieldOfView { get; set; } = 70.0f;

    public float NearPlaneDistance { get; set; } = 0.1f;

    public Matrix4x4 CreateProjectionMatrix()
    {
        return Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(this.FieldOfView), this.AspectRatio, this.NearPlaneDistance, this.FarPlaneDistance);
    }
}
