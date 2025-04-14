// <copyright file="GameContainerBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System;
using System.Drawing;
using FinalEngine.ECS;
using FinalEngine.Input.Controllers;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Batching;
using FinalEngine.Resources;
using FinalEngine.Runtime.Services;
using Microsoft.Extensions.DependencyInjection;

public abstract class GameContainerBase : IDisposable
{
    protected GameContainerBase()
    {
        this.Window = ServiceLocator.Provider.GetRequiredService<IWindow>();
        this.Keyboard = ServiceLocator.Provider.GetRequiredService<IKeyboard>();
        this.Mouse = ServiceLocator.Provider.GetRequiredService<IMouse>();
        this.Controller = ServiceLocator.Provider.GetRequiredService<IGameController>();
        this.RenderDevice = ServiceLocator.Provider.GetRequiredService<IRenderDevice>();
        this.ResourceManager = ServiceLocator.Provider.GetRequiredService<IResourceManager>();
        this.Drawer = ServiceLocator.Provider.GetRequiredService<ISpriteDrawer>();
        this.World = ServiceLocator.Provider.GetRequiredService<IEntityWorld>();

        var fetcher = ServiceLocator.Provider.GetRequiredService<IResourceLoaderFetcher>();
        var loaders = fetcher.GetResourceLoaders();

        foreach (var loader in loaders)
        {
            this.ResourceManager.RegisterLoader(loader.GetResourceType(), loader);
        }
    }

    ~GameContainerBase()
    {
        this.Dispose(false);
    }

    protected IGameController Controller { get; private set; }

    protected ISpriteDrawer Drawer { get; private set; }

    protected bool IsDisposed { get; private set; }

    protected IKeyboard Keyboard { get; private set; }

    protected IMouse Mouse { get; private set; }

    protected IRenderDevice RenderDevice { get; }

    protected IResourceManager ResourceManager { get; private set; }

    protected IWindow Window { get; private set; }

    protected IEntityWorld World { get; private set; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Exit()
    {
        this.Window.Close();
    }

    public virtual void Initialize()
    {
    }

    public virtual void LoadContent()
    {
    }

    public virtual void Render(float delta)
    {
        this.RenderDevice.Clear(Color.CornflowerBlue);
        this.World.ProcessAll(GameLoopType.Render);
    }

    public virtual void UnloadContent()
    {
    }

    public virtual void Update(float delta)
    {
        this.World.ProcessAll(GameLoopType.Update);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.Drawer != null)
            {
                ((IDisposable)this.Drawer).Dispose();
                this.Drawer = null!;
            }

            if (this.ResourceManager != null)
            {
                this.ResourceManager.Dispose();
                this.ResourceManager = null!;
            }

            if (this.Mouse != null)
            {
                ((IDisposable)this.Mouse).Dispose();
                this.Mouse = null!;
            }

            if (this.Keyboard != null)
            {
                ((IDisposable)this.Keyboard).Dispose();
                this.Keyboard = null!;
            }

            if (this.Window != null)
            {
                this.Window.Dispose();
                this.Window = null!;
            }
        }

        this.IsDisposed = true;
    }
}
