// <copyright file="SpriteRenderEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Systems;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;
using FinalEngine.ECS.Components;
using FinalEngine.Maths;
using FinalEngine.Rendering.Batching;
using FinalEngine.Rendering.Components;

[EntitySystemProcess(EventName = "Render")]
public sealed class SpriteRenderEntitySystem : EntitySystemBase
{
    private readonly ISpriteDrawer drawer;

    public SpriteRenderEntitySystem(ISpriteDrawer drawer)
    {
        this.drawer = drawer ?? throw new ArgumentNullException(nameof(drawer));
    }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() &&
               entity.ContainsComponent<SpriteComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        this.drawer.Begin();

        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var sprite = entity.GetComponent<SpriteComponent>();

            if (sprite.Texture == null)
            {
                continue;
            }

            var translation = new Vector2(transform.Position.X, transform.Position.Y);
            var scale = new Vector2(transform.Scale.X, transform.Scale.Y);

            var rotation = Quaternion.Normalize(transform.Rotation);
            float angle = MathHelper.RadiansToDegrees(2 * (float)Math.Acos(rotation.W));

            this.drawer.Draw(
                texture: sprite.Texture,
                color: sprite.Color,
                origin: sprite.Origin,
                position: translation,
                rotation: angle,
                scale: scale);
        }

        this.drawer.End();
    }
}
