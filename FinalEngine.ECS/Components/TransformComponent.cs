// <copyright file="TransformComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components;

using System.ComponentModel;
using System.Numerics;
using FinalEngine.ECS;

[Category("Core")]
public class TransformComponent : IEntityComponent
{
    public TransformComponent()
    {
        this.Position = Vector3.Zero;
        this.Rotation = Quaternion.Identity;
        this.Scale = Vector3.One;
    }

    public Vector3 Backward
    {
        get { return Vector3.Normalize(Vector3.Transform(-Vector3.UnitZ, this.Rotation)); }
    }

    public Vector3 Down
    {
        get { return Vector3.Normalize(Vector3.Transform(-Vector3.UnitY, this.Rotation)); }
    }

    public Vector3 Forward
    {
        get { return Vector3.Normalize(Vector3.Transform(Vector3.UnitZ, this.Rotation)); }
    }

    public Vector3 Left
    {
        get { return Vector3.Normalize(Vector3.Transform(-Vector3.UnitX, this.Rotation)); }
    }

    public Vector3 Position { get; set; }

    public Vector3 Right
    {
        get { return Vector3.Normalize(Vector3.Transform(Vector3.UnitX, this.Rotation)); }
    }

    public Quaternion Rotation { get; set; }

    public Vector3 Scale { get; set; }

    public Vector3 Up
    {
        get { return Vector3.Normalize(Vector3.Transform(Vector3.UnitY, this.Rotation)); }
    }

    public Matrix4x4 CreateTransformationMatrix()
    {
        return Matrix4x4.CreateScale(this.Scale) *
               Matrix4x4.CreateFromQuaternion(this.Rotation) *
               Matrix4x4.CreateTranslation(this.Position);
    }

    public Matrix4x4 CreateViewMatrix(Vector3 cameraUp)
    {
        return Matrix4x4.CreateLookAt(this.Position, this.Position + this.Forward, cameraUp);
    }

    public void Rotate(Vector3 axis, float radians)
    {
        this.Rotation = Quaternion.CreateFromAxisAngle(axis, radians) * this.Rotation;
        this.Rotation = Quaternion.Normalize(this.Rotation);
    }

    public void Translate(Vector3 direction, float amount)
    {
        this.Position += direction * amount;
    }
}
