// <copyright file="MeshRenderEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Systems;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;
using FinalEngine.ECS.Components;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Renderers;

[EntitySystemProcess(EventName = "Render")]
public sealed class MeshRenderEntitySystem : EntitySystemBase
{
    private readonly IRenderQueue<RenderModel> renderQueue;

    public MeshRenderEntitySystem(IRenderQueue<RenderModel> renderQueue)
    {
        this.renderQueue = renderQueue ?? throw new ArgumentNullException(nameof(renderQueue));
    }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() &&
               entity.ContainsComponent<MeshComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var mesh = entity.GetComponent<MeshComponent>();

            this.renderQueue.Enqueue(new RenderModel()
            {
                Mesh = mesh.Mesh,
                Material = mesh.Material,
                Transform = transform,
            });
        }
    }
}
