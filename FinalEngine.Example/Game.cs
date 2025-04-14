// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Example;

using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Physics.Components;
using FinalEngine.Physics.Systems;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Geometry;
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
        this.World.AddSystem<SpinUpdateEntitySystem>();

        var camera = new Entity();

        camera.AddComponent<TransformComponent>();
        camera.AddComponent<PerspectiveComponent>();
        camera.AddComponent(new CameraComponent()
        {
            Viewport = this.Window.ClientBounds,
        });

        camera.AddComponent(new VelocityComponent()
        {
            Speed = 4,
        });

        this.World.AddEntity(camera);

        var cube = new Entity();
        cube.AddComponent(new TransformComponent()
        {
            Position = new Vector3(0, 200, 0),
            Scale = new Vector3(80, 80, 80),
        });
        cube.AddComponent<MeshComponent>();
        cube.AddComponent<VelocityComponent>();
        cube.AddComponent<SpinComponent>();

        this.World.AddEntity(cube);

        this.Create(this.ResourceManager.LoadResource<Rendering.Geometry.Model>("Resources\\Models\\Sponza\\sponza.obj"));

        var light = new Entity();

        light.AddComponent(new TransformComponent()
        {
            Position = new Vector3(2, 40, 0),
        });

        light.AddComponent(new LightComponent()
        {
            Intensity = 80,
        });

        this.World.AddEntity(light);

        base.Initialize();
    }

    private void Create(Model model, bool rotate = false)
    {
        var renderModel = model.RenderModel;

        var entity = new Entity();

        entity.AddComponent(new MeshComponent()
        {
            Mesh = renderModel.Mesh,
            Material = renderModel.Material,
        });

        entity.AddComponent(renderModel.Transform);

        this.World.AddEntity(entity);

        foreach (var child in model.Children)
        {
            this.Create(child);
        }
    }
}
