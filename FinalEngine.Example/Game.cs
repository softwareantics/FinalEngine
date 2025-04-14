// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Example;

using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Systems;
using FinalEngine.Runtime;

public sealed class Game : GameContainerBase
{
    public override void Initialize()
    {
        this.World.AddSystem<CameraUpdateEntitySystem>();
        this.World.AddSystem<MeshRenderEntitySystem>();
        this.World.AddSystem<LightRenderEntitySystem>();
        this.World.AddSystem<PerspectiveRenderEntitySystem>();
        this.World.AddSystem<SpriteRenderEntitySystem>();
        this.World.AddSystem<GameControllerInputEntitySystem>();

        var camera = new Entity();

        camera.AddComponent<TransformComponent>();
        camera.AddComponent<PerspectiveComponent>();
        camera.AddComponent(new CameraComponent()
        {
            Viewport = this.Window.ClientBounds,
        });

        camera.AddComponent(new VelocityComponent()
        {
            Speed = 0.1f,
        });

        this.World.AddEntity(camera);

        var cube = new Entity();

        cube.AddComponent<TransformComponent>();
        cube.AddComponent<MeshComponent>();
        this.World.AddEntity(cube);

        var light = new Entity();

        light.AddComponent(new TransformComponent()
        {
            Position = new Vector3(2, 2, 0),
        });

        light.AddComponent<LightComponent>();

        this.World.AddEntity(light);

        base.Initialize();
    }
}
