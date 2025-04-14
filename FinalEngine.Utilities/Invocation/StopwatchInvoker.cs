// <copyright file="StopwatchInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities.Invocation;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage(Justification = "Invocation")]
internal sealed class StopwatchInvoker : IStopwatchInvoker
{
    private readonly Stopwatch watch;

    public StopwatchInvoker()
        : this(new Stopwatch())
    {
    }

    public StopwatchInvoker(Stopwatch watch)
    {
        this.watch = watch ?? throw new ArgumentNullException(nameof(watch));
    }

    public TimeSpan Elapsed
    {
        get { return this.watch.Elapsed; }
    }

    public bool IsRunning
    {
        get { return this.watch.IsRunning; }
    }

    public void Restart()
    {
        this.watch.Restart();
    }
}
