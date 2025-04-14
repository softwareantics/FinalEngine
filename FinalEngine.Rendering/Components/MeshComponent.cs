// <copyright file="MeshComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Components;

using System.ComponentModel;
using FinalEngine.ECS;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Resources;

[Category("Rendering")]
public sealed class MeshComponent : IEntityComponent
{
    private static readonly Model Model = ResourceManager.Instance.LoadResource<Model>("Resources\\Models\\Cube\\cube.obj");

    private IMaterial? material;

    public IMaterial Material
    {
        get { return this.material ??= new Material(); }
        set { this.material = value; }
    }

    public IMesh? Mesh { get; set; } = Model.RenderModel!.Mesh;
}
