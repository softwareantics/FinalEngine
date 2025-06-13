// <copyright file="EngineDriver.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting;

using System.Diagnostics.CodeAnalysis;
using FinalEngine.Platform;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a standard implementation of an <see cref="IEngineDriver"/> that manages the engine/game life-cycle.
/// </summary>
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
    /// <param name="logger">
    /// Specifies an <see cref="ILogger{TCategoryName}"/> that is used for logging purposes.
    /// </param>
    ///
    /// <param name="window">
    /// Specifies an <see cref="IWindow"/> that represents the window to be used by the engine driver.
    /// </param>
    ///
    /// <param name="eventsProcessor">
    /// Specifies an <see cref="IEventsProcessor"/> that represents the events processor used to handle events in the message queue.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown when one of the following parameters is null:
    /// <list type="bullet">
    ///     <item>
    ///         <paramref name="logger"/>
    ///     </item>
    ///     <item>
    ///         <paramref name="window"/>
    ///     </item>
    ///     <item>
    ///         <paramref name="eventsProcessor"/>
    ///     </item>
    /// </list>
    /// </exception>
    public EngineDriver(
        ILogger<EngineDriver> logger,
        IWindow window,
        IEventsProcessor eventsProcessor)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.window = window ?? throw new ArgumentNullException(nameof(window));
        this.eventsProcessor = eventsProcessor ?? throw new ArgumentNullException(nameof(eventsProcessor));
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

        if (disposing && this.window != null)
        {
            this.window.Dispose();
            this.window = null;
        }

        this.isDisposed = true;
    }

    /// <summary>
    /// Runs the main game loop.
    /// </summary>
    private void RunGameLoop()
    {
        this.isRunning = true;

        this.logger.LogInformation("Entering the game loop...");

        while (this.eventsProcessor.CanProcessEvents)
        {
            this.eventsProcessor.ProcessEvents();
        }

        this.logger.LogInformation("Exited the game loop.");
    }
}
