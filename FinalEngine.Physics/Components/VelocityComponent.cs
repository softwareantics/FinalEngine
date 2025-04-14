// <copyright file="VelocityComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Physics.Components;

using System.ComponentModel;
using FinalEngine.ECS;

[Category("Physics")]
public sealed class VelocityComponent : IEntityComponent
{
    public VelocityComponent()
    {
        this.Speed = 1.0f;
    }

    public float Speed { get; set; }
}
