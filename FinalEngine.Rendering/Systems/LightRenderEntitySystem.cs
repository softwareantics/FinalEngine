// <copyright file="LightRenderEntitySystem.cs" company="Software Antics">
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
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.Renderers;

[EntitySystemProcess(EventName = "Render")]
public sealed class LightRenderEntitySystem : EntitySystemBase
{
    private readonly IRenderQueue<Light> renderQueue;

    public LightRenderEntitySystem(IRenderQueue<Light> renderQueue)
    {
        this.renderQueue = renderQueue ?? throw new ArgumentNullException(nameof(renderQueue));
    }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() &&
               entity.ContainsComponent<LightComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var light = entity.GetComponent<LightComponent>();

            this.renderQueue.Enqueue(new Light()
            {
                Attenuation = light.Attenuation,
                Color = light.Color,
                Direction = light.Direction,
                Intensity = light.Intensity,
                OuterRadius = light.OuterRadius,
                Position = transform.Position,
                Radius = light.Radius,
                Type = light.Type,
            });
        }
    }
}
