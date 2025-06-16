// <copyright file="TransformComponent.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes.Components;

using System.ComponentModel;
using System.Numerics;
using FinalEngine.Scenes;

[Category("Core")]
public sealed class TransformComponent : IEntityComponent
{
    public TransformComponent()
    {
        this.Position = Vector2.Zero;
        this.Rotation = 0;
        this.Scale = Vector2.One;
    }

    public Vector2 Position { get; set; }

    public float Rotation { get; set; }

    public Vector2 Scale { get; set; }
}
