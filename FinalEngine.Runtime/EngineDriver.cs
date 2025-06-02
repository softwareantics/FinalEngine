// <copyright file="EngineDriver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using FinalEngine.Platform;

/// <summary>
///   Provides a standard implementation of an <see cref="IEngineDriver"/> that manages the engine/game life-cycle.
/// </summary>
/// <seealso cref="IEngineDriver"/>
internal sealed class EngineDriver : IEngineDriver
{
    /// <summary>
    ///   The events processor used to handle events in the message queue.
    /// </summary>
    private readonly IEventsProcessor eventsProcessor;

    /// <summary>
    ///   Indicates whether the <see cref="EngineDriver"/> has been disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    ///   Indicates whether the <see cref="EngineDriver"/> is currently running.
    /// </summary>
    private bool isRunning;

    /// <summary>
    ///   The game window.
    /// </summary>
    private IWindow? window;

    /// <summary>
    ///   Initializes a new instance of the <see cref="EngineDriver"/> class.
    /// </summary>
    /// <param name="window">
    ///   The window to be used by the engine driver.
    /// </param>
    /// <param name="eventsProcessor">
    ///   The events processor to handle events in the message queue.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="window"/> or <paramref name="eventsProcessor"/> parameter is null.
    /// </exception>
    public EngineDriver(
        IWindow window,
        IEventsProcessor eventsProcessor)
    {
        this.window = window ?? throw new ArgumentNullException(nameof(window));
        this.eventsProcessor = eventsProcessor ?? throw new ArgumentNullException(nameof(eventsProcessor));
    }

    /// <summary>
    ///   Finalizes an instance of the <see cref="EngineDriver"/> class.
    /// </summary>
    ~EngineDriver()
    {
        this.Dispose(false);
    }

    /// <summary>
    ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Runs the engine.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown when the <see cref="EngineDriver"/> has already been disposed.
    /// </exception>
    /// <remarks>
    ///   This method starts the engine and processes events in the message queue until there are no more events to process. It will continue to run until the <see cref="IEventsProcessor.CanProcessEvents"/> property returns <c>false</c>. The engine will not run if it has already been disposed or if it is already running.
    /// </remarks>
    public void Run()
    {
        ObjectDisposedException.ThrowIf(this.isRunning, nameof(EngineDriver));

        if (this.isRunning)
        {
            return;
        }

        this.isRunning = true;

        while (this.eventsProcessor.CanProcessEvents)
        {
            this.eventsProcessor.ProcessEvents();
        }
    }

    /// <summary>
    ///   Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.window != null)
        {
            this.window.Dispose();
            this.window = null;
        }

        this.isDisposed = true;
    }
}
