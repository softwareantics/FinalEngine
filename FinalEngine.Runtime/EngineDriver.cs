// <copyright file="EngineDriver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System;
using FinalEngine.Input;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using FinalEngine.Runtime.Services;
using FinalEngine.Utilities;
using Microsoft.Extensions.DependencyInjection;

internal sealed class EngineDriver : IEngineDriver
{
    private readonly IEventsProcessor eventsProcessor;

    private readonly IGameTime gameTime;

    private readonly IInputDriver inputDriver;

    private readonly IRenderContext renderContext;

    private GameContainerBase? game;

    private bool isDisposed;

    private IRenderPipeline? renderPipeline;

    public EngineDriver(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));
        ServiceLocator.SetServiceProvider(serviceProvider);

        this.eventsProcessor = serviceProvider.GetRequiredService<IEventsProcessor>();
        this.gameTime = serviceProvider.GetRequiredService<IGameTime>();
        this.inputDriver = serviceProvider.GetRequiredService<IInputDriver>();
        this.renderContext = serviceProvider.GetRequiredService<IRenderContext>();
        this.renderPipeline = serviceProvider.GetRequiredService<IRenderPipeline>();
        this.game = serviceProvider.GetRequiredService<GameContainerBase>();
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

    public void Start()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);

        this.renderPipeline!.Initialize();

        this.game!.Initialize();
        this.game.LoadContent();

        while (this.eventsProcessor.CanProcessEvents)
        {
            if (!this.gameTime.CanProcessNextFrame())
            {
                continue;
            }

            this.game.Update(GameTime.Delta);

            this.inputDriver.Update();

            this.game.Render(GameTime.Delta);

            this.renderContext.SwapBuffers();
            this.eventsProcessor.ProcessEvents();
        }

        this.game.UnloadContent();
        this.game.Dispose();
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.game != null)
            {
                this.game.Dispose();
                this.game = null;
            }

            if (this.renderPipeline != null)
            {
                this.renderPipeline.Dispose();
                this.renderPipeline = null;
            }
        }

        this.isDisposed = true;
    }
}
