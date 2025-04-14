// <copyright file="Model.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Geometry;

using System;
using System.Collections.Generic;
using FinalEngine.Resources;

public sealed class Model : IResource
{
    private readonly List<Model> children;

    public Model(string name)
    {
        this.Name = name;
        this.children = [];
    }

    public IEnumerable<Model> Children
    {
        get { return this.children; }
    }

    public string Name { get; }

    public RenderModel? RenderModel { get; internal set; }

    public void AddChild(Model model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));
        this.children.Add(model);
    }
}
