// <copyright file="StopwatchInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Adapters;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal sealed class StopwatchAdapter : IStopwatchAdapter
{
    private readonly Stopwatch watch;

    public StopwatchAdapter()
    {
        this.watch = new Stopwatch();
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
