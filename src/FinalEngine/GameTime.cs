// <copyright file="GameTime.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine;

using System;
using FinalEngine.Adapters;

internal sealed class GameTime : IGameTime
{
    private const double OneSecondAsMilliSeconds = 1000.0d;

    private readonly double waitTime;

    private readonly IStopwatchAdapter watch;

    private double lastTime;

    public GameTime(IStopwatchAdapter watch)
    {
        this.watch = watch ?? throw new ArgumentNullException(nameof(watch));
        this.waitTime = OneSecondAsMilliSeconds / 120.0d;
    }

    public float Delta { get; private set; }

    public float FrameRate { get; private set; }

    bool IGameTime.CanProcessNextFrame()
    {
        if (!this.watch.IsRunning)
        {
            this.watch.Restart();
        }

        double currentTime = this.watch.Elapsed.TotalMilliseconds;

        if (currentTime >= this.lastTime + this.waitTime)
        {
            this.Delta = (float)((float)(currentTime - this.lastTime) / OneSecondAsMilliSeconds);
            this.FrameRate = (float)((float)Math.Round(OneSecondAsMilliSeconds / this.Delta) * OneSecondAsMilliSeconds);

            this.lastTime = currentTime;

            return true;
        }

        return false;
    }
}
