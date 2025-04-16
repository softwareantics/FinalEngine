// <copyright file="SceneManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Drawing;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Editor.Common.Blackboard;
using FinalEngine.Editor.Common.Components;
using FinalEngine.Editor.Common.Systems;
using FinalEngine.Input;
using FinalEngine.Physics.Components;
using FinalEngine.Physics.Systems;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Systems;

internal sealed class SceneManager : ISceneManager
{
    private readonly IInputDriver inputDriver;

    private readonly IRenderDevice renderDevice;

    private readonly IRenderPipeline renderPipeline;

    public SceneManager(IEntityWorld scene, IInputDriver inputDriver, IRenderDevice renderDevice, IRenderPipeline renderPipeline)
    {
        this.Scene = scene ?? throw new ArgumentNullException(nameof(scene));
        this.inputDriver = inputDriver ?? throw new ArgumentNullException(nameof(inputDriver));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.renderPipeline = renderPipeline ?? throw new ArgumentNullException(nameof(renderPipeline));
    }

    public IEntityWorld Scene { get; }

    public void Initialize()
    {
        this.renderPipeline.Initialize();

        this.Scene.AddResource(new ViewportBlackboardResource());

        this.Scene.AddSystem<ViewportUpdateEntitySystem>();
        this.Scene.AddSystem<CameraUpdateEntitySystem>();
        this.Scene.AddSystem<SpinUpdateEntitySystem>();

        this.Scene.AddSystem<MeshRenderEntitySystem>();
        this.Scene.AddSystem<LightRenderEntitySystem>();
        this.Scene.AddSystem<PerspectiveRenderEntitySystem>();
        this.Scene.AddSystem<SpriteRenderEntitySystem>();

        var entity = new Entity();

        entity.AddComponent<TransformComponent>();
        entity.AddComponent<VelocityComponent>();
        entity.AddComponent<PerspectiveComponent>();
        entity.AddComponent<CameraComponent>();
        entity.AddComponent<EditorComponent>();

        this.Scene.AddEntity(entity);
    }

    public void Render()
    {
        this.renderDevice.Clear(Color.Black);
        this.Scene.ProcessAll("Render");
    }

    public void SetViewport(Rectangle viewport)
    {
        this.Scene.GetResource<ViewportBlackboardResource>().Resource = viewport;
    }

    public void Update()
    {
        this.Scene.ProcessAll("Update");
        this.inputDriver.Update();
    }
}
