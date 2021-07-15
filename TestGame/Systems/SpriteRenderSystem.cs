// <copyright file="SpriteRenderSystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame.Systems
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.ECS;
    using FinalEngine.Rendering;
    using TestGame.Components;

    public class SpriteRenderSystem : EntitySystemBase
    {
        private readonly ISpriteDrawer drawer;

        public SpriteRenderSystem(ISpriteDrawer drawer)
        {
            this.drawer = drawer ?? throw new ArgumentNullException(nameof(drawer), $"The specified {nameof(drawer)} parameter cannot be null.");
        }

        public override GameLoopType LoopType { get; } = GameLoopType.Render;

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<SpriteComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            this.drawer.Begin();

            foreach (dynamic entity in entities)
            {
                TransformComponent transform = entity.Transform;
                SpriteComponent sprite = entity.Sprite;

                this.drawer.Draw(
                    sprite.Texture,
                    sprite.Color,
                    sprite.Origin,
                    transform.Position,
                    transform.Rotation,
                    transform.Scale);
            }

            this.drawer.End();
        }
    }
}