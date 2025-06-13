// <copyright file="IEngineDriver.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting;

/// <summary>
/// Defines an interface that represents a driver for running an engine.
/// </summary>
///
/// <seealso cref="IDisposable"/>
public interface IEngineDriver : IDisposable
{
    /// <summary>
    /// Runs the engine.
    /// </summary>
    ///
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="IEngineDriver"/> has already been disposed.
    /// </exception>
    void Start();

    /// <summary>
    /// Stops the engine.
    /// </summary>
    ///
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="IEngineDriver"/> has already been disposed.
    /// </exception>
    void Stop();
}
