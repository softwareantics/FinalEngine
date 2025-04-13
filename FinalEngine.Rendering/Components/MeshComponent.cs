// <copyright file="MeshComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Components;

using FinalEngine.ECS;
using FinalEngine.Rendering.Geometry;

public sealed class MeshComponent : IEntityComponent
{
    private IMaterial? material;

    public IMaterial Material
    {
        get { return this.material ??= new Material(); }
        set { this.material = value; }
    }

    public IMesh? Mesh { get; set; }
}
