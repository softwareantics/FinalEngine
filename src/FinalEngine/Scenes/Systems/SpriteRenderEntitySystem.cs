// <copyright file="SpriteRenderEntitySystem.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes.Systems;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Rendering.Batching;
using FinalEngine.Scenes.Attributes;
using FinalEngine.Scenes.Components;
using FinalEngine.Scenes.Entities;

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
        this.drawer.Start();

        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var sprite = entity.GetComponent<SpriteComponent>();

            if (sprite.Texture == null)
            {
                continue;
            }

            this.drawer.Draw(
                sprite.Texture,
                sprite.Color,
                sprite.Origin,
                transform.Position,
                transform.Rotation,
                transform.Scale);
        }

        this.drawer.Finish();
    }
}
