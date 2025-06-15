// <copyright file="EngineDriver.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting;

using System.Diagnostics.CodeAnalysis;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using Microsoft.Extensions.Logging;

internal sealed class EngineDriver : IEngineDriver
{
    private readonly IEventsProcessor eventsProcessor;

    private readonly ILogger<EngineDriver> logger;

    private bool isDisposed;

    private bool isRunning;

    private IRenderContext? renderContext;

    private IWindow? window;

    public EngineDriver(
        ILogger<EngineDriver> logger,
        IWindow window,
        IEventsProcessor eventsProcessor,
        IRenderContext.RenderContextFactory renderContextFactory)
    {
        ArgumentNullException.ThrowIfNull(renderContextFactory);

        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.window = window ?? throw new ArgumentNullException(nameof(window));
        this.eventsProcessor = eventsProcessor ?? throw new ArgumentNullException(nameof(eventsProcessor));
        this.renderContext = renderContextFactory(this.window.Handle, this.window.ClientSize);
    }

    [ExcludeFromCodeCoverage]
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
        ObjectDisposedException.ThrowIf(this.isDisposed, nameof(EngineDriver));

        if (this.isRunning)
        {
            this.logger.LogWarning("The engine driver is already running.");
            return;
        }

        this.logger.LogInformation("Starting the engine driver...");

        this.RunGameLoop();
    }

    public void Stop()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, nameof(EngineDriver));
        this.logger.LogInformation("Stopping the engine driver...");
        this.isRunning = false;
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        this.logger.LogTrace("Disposing the engine driver...");

        if (disposing)
        {
            if (this.renderContext != null)
            {
                this.renderContext.Dispose();
                this.renderContext = null;
            }

            if (this.window != null)
            {
                this.window.Dispose();
                this.window = null;
            }
        }

        this.isDisposed = true;
    }

    private void RunGameLoop()
    {
        this.isRunning = true;

        this.logger.LogInformation("Entering the game loop...");

        this.renderContext!.MakeCurrent();

        while (this.eventsProcessor.CanProcessEvents)
        {
            this.renderContext.SwapBuffers();
            this.eventsProcessor.ProcessEvents();
        }

        this.logger.LogInformation("Exited the game loop.");
    }
}
