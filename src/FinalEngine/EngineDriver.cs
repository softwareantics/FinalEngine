// <copyright file="EngineDriver.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine;

using System.Drawing;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;
using FinalEngine.Scenes;
using FinalEngine.Scenes.Components;
using FinalEngine.Scenes.Entities;
using FinalEngine.Scenes.Systems;

internal sealed class EngineDriver : IEngineDriver
{
    private readonly IEventsProcessor eventsProcessor;

    private readonly IRenderContext renderContext;

    private readonly IRenderDevice renderDevice;

    private readonly ISceneFactory sceneFactory;

    private bool isDisposed;

    private bool isRunning;

    private IKeyboard? keyboard;

    private IMouse? mouse;

    private IRenderPipeline? renderPipeline;

    private ISceneManager? sceneManager;

    private IWindow? window;

    public EngineDriver(
        IWindow window,
        IEventsProcessor eventsProcessor,
        IKeyboard keyboard,
        IMouse mouse,
        IRenderContext renderContext,
        IRenderPipeline renderPipeline,
        IRenderDevice renderDevice,
        ISceneManager sceneManager,
        IEnumerable<IResourceLoader> loaders,
        ISceneFactory sceneFactory)
    {
        this.window = window ?? throw new ArgumentNullException(nameof(window));
        this.eventsProcessor = eventsProcessor ?? throw new ArgumentNullException(nameof(eventsProcessor));
        this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
        this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
        this.renderContext = renderContext ?? throw new ArgumentNullException(nameof(renderContext));
        this.renderPipeline = renderPipeline ?? throw new ArgumentNullException(nameof(renderPipeline));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
        this.sceneFactory = sceneFactory ?? throw new ArgumentNullException(nameof(sceneFactory));

        foreach (var loader in loaders)
        {
            ResourceManager.Instance.RegisterLoader(loader);
        }
    }

    ~EngineDriver()
    {
        this.Dispose(false);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Drive()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(EngineDriver));

        if (this.isRunning)
        {
            return;
        }

        this.isRunning = true;

        this.renderPipeline!.Initialize();

        //// TODO: Setup a way to set a default starting scene!
        //// I'm thinking the best way to handle this might be a ISceneStartup.ConfigureScene(IScene) interface.
        //// TODO: Setup service configurators to register automatically through Assembly.Load for all FinalEngine.* assemblies.
        //// TODO: Setup a FinalEngine.Hosting project, register all configurators there and set up an entry point like a way to take a scene and set it as the default

        var scene = this.sceneFactory.CreateScene();

        scene.AddSystem<SpriteRenderEntitySystem>();

        var entity = new Entity();

        entity.AddComponent(new TransformComponent());
        entity.AddComponent(new SpriteComponent()
        {
            Texture = ResourceManager.Instance.LoadResource<ITexture2D>("test.png"),
        });

        scene.AddEntity(entity);

        SceneManager.Instance.LoadScene(scene);

        this.sceneManager!.CurrentScene?.Initialize();
        this.sceneManager.CurrentScene?.LoadContent();

        while (this.eventsProcessor.CanProcessEvents)
        {
            this.sceneManager!.CurrentScene?.Update(8.3f);

            this.keyboard!.Update();
            this.mouse!.Update();

            this.renderDevice.Clear(Color.Black);
            this.sceneManager!.CurrentScene?.Render(8.3f);

            this.renderContext.SwapBuffers();
            this.eventsProcessor.ProcessEvents();
        }

        this.sceneManager.CurrentScene?.UnloadContent();
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.sceneManager != null)
            {
                this.sceneManager.Dispose();
                this.sceneManager = null;
            }

            if (this.renderPipeline != null)
            {
                this.renderPipeline.Dispose();
                this.renderPipeline = null;
            }

            if (this.mouse != null)
            {
                (this.mouse as IDisposable)?.Dispose();
                this.mouse = null;
            }

            if (this.keyboard != null)
            {
                (this.keyboard as IDisposable)?.Dispose();
                this.keyboard = null;
            }

            if (this.window != null)
            {
                this.window.Dispose();
                this.window = null;
            }
        }

        this.isDisposed = true;
    }
}
