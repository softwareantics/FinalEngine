// <copyright file="EngineDriver.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting;

using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using Microsoft.Extensions.Logging;

internal sealed class EngineDriver : IEngineDriver
{
    private readonly IEventsProcessor eventsProcessor;

    private readonly IGameTime gameTime;

    private readonly ILogger<EngineDriver> logger;

    private readonly IRenderContext renderContext;

    private bool isDisposed;

    private bool isRunning;

    private IKeyboard? keyboard;

    private IMouse? mouse;

    private IRenderPipeline? renderPipeline;

    private IWindow? window;

    public EngineDriver(
        ILogger<EngineDriver> logger,
        IWindow window,
        IEventsProcessor eventsProcessor,
        IKeyboard keyboard,
        IMouse mouse,
        IRenderContext renderContext,
        IRenderPipeline renderPipeline,
        IGameTime gameTime)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        this.window = window ?? throw new ArgumentNullException(nameof(window));
        this.eventsProcessor = eventsProcessor ?? throw new ArgumentNullException(nameof(eventsProcessor));

        this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
        this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));

        this.renderContext = renderContext ?? throw new ArgumentNullException(nameof(renderContext));
        this.renderPipeline = renderPipeline ?? throw new ArgumentNullException(nameof(renderPipeline));

        this.gameTime = gameTime ?? throw new ArgumentNullException(nameof(gameTime));
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
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(EngineDriver));

        if (this.isRunning)
        {
            this.logger.LogWarning("The engine driver is already running. Ignoring the request to run it again.");
            return;
        }

        this.isRunning = true;

        this.logger.LogInformation("Starting the engine driver...");

        this.renderPipeline!.Initialize();

        while (this.eventsProcessor.CanProcessEvents)
        {
            if (!this.gameTime.CanProcessNextFrame())
            {
                continue;
            }

            this.keyboard!.Update();
            this.mouse!.Update();

            this.renderContext!.SwapBuffers();
            this.eventsProcessor.ProcessEvents();
        }

        this.logger.LogInformation("Engine driver has stopped running.");
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        this.logger.LogInformation("Disposing the engine driver...");

        if (disposing)
        {
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
