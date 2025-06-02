// <copyright file="IEngineDriver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using FinalEngine.Platform;

/// <summary>
///   Defines an interface that represents a driver for running an engine.
/// </summary>
/// <seealso cref="IDisposable"/>
public interface IEngineDriver : IDisposable
{
    /// <summary>
    ///   Runs the engine.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown when the <see cref="EngineDriver"/> has already been disposed.
    /// </exception>
    /// <remarks>
    ///   The implementation should start the engine and processes events in the message queue until there are no more events to process. It should continue to run until the <see cref="IEventsProcessor.CanProcessEvents"/> property returns <c>false</c>. The engine will not run if it has already been disposed or if it is already running.
    /// </remarks>
    void Run();
}
