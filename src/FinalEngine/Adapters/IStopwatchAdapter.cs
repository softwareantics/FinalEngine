// <copyright file="IStopwatchAdapter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Adapters;

using System;

internal interface IStopwatchAdapter
{
    TimeSpan Elapsed { get; }

    bool IsRunning { get; }

    void Restart();
}
