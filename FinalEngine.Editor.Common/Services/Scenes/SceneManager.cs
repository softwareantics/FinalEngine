// <copyright file="SceneManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Drawing;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Blackboard;
using FinalEngine.Editor.Common.Services.Factories.Entities.Cameras;
using FinalEngine.Editor.Common.Systems;
using FinalEngine.Input;
using FinalEngine.Physics.Systems;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Systems;

internal sealed class SceneManager : ISceneManager
{
    private readonly IInputDriver inputDriver;

    private readonly IRenderDevice renderDevice;

    private readonly IRenderPipeline renderPipeline;

    public SceneManager(IEntityWorld scene, IInputDriver inputDriver, IRenderDevice renderDevice, IRenderPipeline renderPipeline)
    {
        this.ActiveScene = scene ?? throw new ArgumentNullException(nameof(scene));
        this.inputDriver = inputDriver ?? throw new ArgumentNullException(nameof(inputDriver));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.renderPipeline = renderPipeline ?? throw new ArgumentNullException(nameof(renderPipeline));
    }

    public IEntityWorld ActiveScene { get; }

    public void Initialize()
    {
        this.renderPipeline.Initialize();

        this.ActiveScene.AddResource(new ViewportBlackboardResource());

        this.ActiveScene.AddSystem<ViewportUpdateEntitySystem>();
        this.ActiveScene.AddSystem<CameraUpdateEntitySystem>();
        this.ActiveScene.AddSystem<SpinUpdateEntitySystem>();

        this.ActiveScene.AddSystem<MeshRenderEntitySystem>();
        this.ActiveScene.AddSystem<LightRenderEntitySystem>();
        this.ActiveScene.AddSystem<PerspectiveRenderEntitySystem>();
        this.ActiveScene.AddSystem<SpriteRenderEntitySystem>();

        this.ActiveScene.AddEntityFromFactory<EditorCameraEntityFactory>();
    }

    public void Render()
    {
        this.renderDevice.Clear(Color.Black);
        this.ActiveScene.ProcessAll("Render");
    }

    public void SetViewport(Rectangle viewport)
    {
        this.ActiveScene.GetResource<ViewportBlackboardResource>().Resource = viewport;
    }

    public void Update()
    {
        this.ActiveScene.ProcessAll("Update");
        this.inputDriver.Update();
    }
}
