// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Example;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.Input.Controllers;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Systems;
using FinalEngine.Rendering.Textures;
using FinalEngine.Runtime;

public class GameControllerInputEntitySystem : EntitySystemBase
{
    private readonly IGameController controller;

    public GameControllerInputEntitySystem(IGameController controller)
    {
        this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            transform.Translate(movement, 1);
        }
    }
}

public sealed class Game : GameContainerBase
{
    public override void Initialize()
    {
        this.World.AddSystem<SpriteRenderEntitySystem>();
        this.World.AddSystem<GameControllerInputEntitySystem>();

        var entity = new Entity();

        entity.AddComponent<TransformComponent>();
        entity.AddComponent(new SpriteComponent()
        {
            Color = Color.White,
            Origin = Vector2.Zero,
            Texture = this.ResourceManager.LoadResource<ITexture2D>("Resources\\Textures\\default_diffuse.png"),
        });

        this.World.AddEntity(entity);

        base.Initialize();
    }
}
