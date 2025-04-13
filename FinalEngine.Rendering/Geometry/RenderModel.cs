// <copyright file="RenderModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Geometry;

using FinalEngine.Rendering.Components;

public sealed class RenderModel
{
    private IMaterial? material;

    private TransformComponent? transform;

    public IMaterial Material
    {
        get { return this.material ??= new Material(); }
        set { this.material = value; }
    }

    public IMesh? Mesh { get; set; }

    public TransformComponent Transform
    {
        get { return this.transform ??= new TransformComponent(); }
        set { this.transform = value; }
    }
}
