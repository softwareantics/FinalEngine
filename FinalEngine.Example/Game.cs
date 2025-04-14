// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Example;

using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.Systems;
using FinalEngine.Runtime;
using Microsoft.Extensions.DependencyInjection;

public sealed class Game : GameContainerBase
{
    private Model model;

    private IRenderingEngine renderingEngine;

    public override void Initialize()
    {
        this.World.AddSystem<MeshRenderEntitySystem>();
        this.World.AddSystem<LightRenderEntitySystem>();
        this.World.AddSystem<FlyCameraUpdateEntitySystem>();
        this.World.AddSystem<PerspectiveRenderEntitySystem>();

        this.model = this.ResourceManager.LoadResource<Model>("Resources\\Models\\Sponza\\sponza.obj");

        this.Populate(this.model);

        this.renderingEngine = this.Provider.GetRequiredService<IRenderingEngine>();

        var camera = new Entity();

        camera.AddComponent<TransformComponent>();

        camera.AddComponent(new VelocityComponent()
        {
            Speed = 0.5f,
        });

        camera.AddComponent(new PerspectiveComponent()
        {
            AspectRatio = this.Window.ClientSize.Width / this.Window.ClientSize.Height,
        });

        camera.AddComponent(new CameraComponent()
        {
            Viewport = this.Window.ClientBounds,
        });

        this.World.AddEntity(camera);

        var entity = new Entity();

        entity.AddComponent(new TransformComponent()
        {
            Position = new Vector3(-80, 4, -30),
        });

        entity.AddComponent(new LightComponent()
        {
            Type = LightType.Point,
        });

        this.World.AddEntity(entity);

        base.Initialize();
    }

    private void Populate(Model model)
    {
        if (model.RenderModel != null)
        {
            model.RenderModel.Transform.Scale = new Vector3(0.2f);

            var entity = new Entity();

            entity.AddComponent(model.RenderModel.Transform);
            entity.AddComponent(new MeshComponent()
            {
                Mesh = model.RenderModel.Mesh,
                Material = model.RenderModel.Material,
            });

            this.World.AddEntity(entity);
        }

        foreach (var child in model.Children)
        {
            this.Populate(child);
        }
    }
}
