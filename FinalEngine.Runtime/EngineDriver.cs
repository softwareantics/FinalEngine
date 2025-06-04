// <copyright file="EngineDriver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Platform;

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
    /// <param name="window">
    /// The window to be used by the engine driver.
    /// </param>
    ///
    /// <param name="eventsProcessor">
    /// The events processor to handle events in the message queue.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown when one of the following parameters is null:
    /// <list type="bullet">
    ///     <item>
    ///         <paramref name="window"/>
    ///     </item>
    ///     <item>
    ///         <paramref name="eventsProcessor"/>
    ///     </item>
    /// </list>
    /// </exception>
    public EngineDriver(
        IWindow window,
        IEventsProcessor eventsProcessor)
    {
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
            return;
        }

        this.RunGameLoop();
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="EngineDriver"/> has already been disposed.
    /// </exception>
    public void Stop()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, nameof(EngineDriver));
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

        while (this.eventsProcessor.CanProcessEvents)
        {
            this.eventsProcessor.ProcessEvents();
        }
    }
}
