// <copyright file="SpinComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Physics.Components;

using System.Numerics;
using FinalEngine.ECS;

public sealed class SpinComponent : IEntityComponent
{
    public Vector3 Axis { get; set; } = Vector3.UnitY;
}
