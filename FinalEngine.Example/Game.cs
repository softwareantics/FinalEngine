// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Example;

using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Cameras;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.Systems;
using FinalEngine.Runtime;
using Microsoft.Extensions.DependencyInjection;

public sealed class Game : GameContainerBase
{
    private FlyCamera camera;

    private Model model;

    private IRenderingEngine renderingEngine;

    public override void Initialize()
    {
        this.World.AddSystem<MeshRenderEntitySystem>();
        this.World.AddSystem<LightRenderEntitySystem>();

        this.camera = new FlyCamera(this.Window.ClientSize.Width, this.Window.ClientSize.Height);

        this.model = this.ResourceManager.LoadResource<Model>("Resources\\Models\\Sponza\\sponza.obj");

        this.Populate(this.model);

        this.renderingEngine = this.Provider.GetRequiredService<IRenderingEngine>();

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

    public override void Render(float delta)
    {
        this.renderingEngine.Render(this.camera);
        base.Render(delta);
    }

    public override void Update(float delta)
    {
        this.Window.Title = $"{GameTime.FrameRate}";
        this.camera.Update(this.RenderDevice.Pipeline, this.Keyboard, this.Mouse);

        base.Update(delta);
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
