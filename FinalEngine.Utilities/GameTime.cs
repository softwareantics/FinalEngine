// <copyright file="GameTime.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities;

using System;
using FinalEngine.Utilities.Invocation;

public sealed class GameTime : IGameTime
{
    private const double OneSecondAsMilliSeconds = 1000.0d;

    private readonly double waitTime;

    private readonly IStopwatchInvoker watch;

    private double lastTime;

    public GameTime(double frameCap)
        : this(new StopwatchInvoker(), frameCap)
    {
    }

    internal GameTime(IStopwatchInvoker watch, double frameCap)
    {
        this.watch = watch ?? throw new ArgumentNullException(nameof(watch));

        if (frameCap <= 0.0d)
        {
            throw new DivideByZeroException($"The specified {nameof(frameCap)} parameter must be greater than zero.");
        }

        this.waitTime = OneSecondAsMilliSeconds / frameCap;
    }

    public static float Delta { get; private set; } = 8.3f;

    public static float FrameRate { get; private set; }

    bool IGameTime.CanProcessNextFrame()
    {
        if (!this.watch.IsRunning)
        {
            this.watch.Restart();
        }

        double currentTime = this.watch.Elapsed.TotalMilliseconds;

        if (currentTime >= this.lastTime + this.waitTime)
        {
            Delta = (float)(currentTime - this.lastTime) / (float)OneSecondAsMilliSeconds;
            FrameRate = (float)Math.Round(OneSecondAsMilliSeconds / Delta);

            this.lastTime = currentTime;

            return true;
        }

        return false;
    }
}
