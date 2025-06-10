// <copyright file="EngineDriver.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using FinalEngine.Platform;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

//// TODO: Finish unit tests - added GameContainerBase and registration of services in the constructor.

/// <summary>
/// Provides a standard implementation of an <see cref="IEngineDriver"/> that manages the engine/game life-cycle.
/// </summary>
///
/// <seealso cref="IEngineDriver"/>
internal sealed class EngineDriver : IEngineDriver
{
    /// <summary>
    /// Specifies an <see cref="IEventsProcessor"/> that represents the events processor used to handle events in the message queue.
    /// </summary>
    private readonly IEventsProcessor eventsProcessor;

    /// <summary>
    /// Specifies an <see cref="ILogger{TCategoryName}"/> that is used for logging purposes.
    /// </summary>
    private readonly ILogger<EngineDriver> logger;

    /// <summary>
    /// Specifies a <see cref="GameContainerBase"/> that represents the game container used to manage the game state and content.
    /// </summary>
    private GameContainerBase? gameContainer;

    /// <summary>
    /// Indicates whether the <see cref="EngineDriver"/> has been disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// Indicates whether the <see cref="EngineDriver"/> is currently running.
    /// </summary>
    private bool isRunning;

    /// <summary>
    /// Specifies an <see cref="IWindow"/> that represents the window to be used by the engine driver.
    /// </summary>
    private IWindow? window;

    /// <summary>
    /// Initializes a new instance of the <see cref="EngineDriver"/> class.
    /// </summary>
    ///
    /// <param name="provider">
    /// The <see cref="IServiceProvider"/> that provides the required services for the engine driver.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified <paramref name="provider"/> parameter is <c>null</c>.
    /// </exception>
    public EngineDriver(IServiceProvider provider)
    {
        ServiceLocator.SetServiceProvider(provider);

        this.logger = provider.GetRequiredService<ILogger<EngineDriver>>();
        this.window = provider.GetRequiredService<IWindow>();
        this.eventsProcessor = provider.GetRequiredService<IEventsProcessor>();
        this.gameContainer = provider.GetRequiredService<GameContainerBase>();
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="EngineDriver"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    ~EngineDriver()
    {
        this.Dispose(false);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="EngineDriver"/> has already been disposed.
    /// </exception>
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

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="EngineDriver"/> has already been disposed.
    /// </exception>
    public void Stop()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, nameof(EngineDriver));
        this.logger.LogInformation("Stopping the engine driver...");
        this.isRunning = false;
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        this.logger.LogTrace("Disposing the engine driver...");

        if (disposing)
        {
            if (this.gameContainer != null)
            {
                this.gameContainer.Dispose();
                this.gameContainer = null;
            }

            if (this.window != null)
            {
                this.window.Dispose();
                this.window = null;
            }
        }

        this.isDisposed = true;
    }

    /// <summary>
    /// Runs the main game loop.
    /// </summary>
    private void RunGameLoop()
    {
        this.isRunning = true;

        this.gameContainer!.Initialize();
        this.gameContainer.LoadContent();

        this.logger.LogInformation("Entering the game loop...");

        while (this.eventsProcessor.CanProcessEvents)
        {
            this.gameContainer.Update();
            this.gameContainer.Draw();

            this.eventsProcessor.ProcessEvents();
        }

        this.logger.LogInformation("Exited the game loop.");

        this.gameContainer.UnloadContent();
    }
}
