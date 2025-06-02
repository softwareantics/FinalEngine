// <copyright file="EngineDriver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using FinalEngine.Platform;

internal sealed class EngineDriver : IEngineDriver
{
    private readonly IEventsProcessor eventsProcessor;

    private readonly IWindow window;

    private bool isRunning;

    public EngineDriver(
        IWindow window,
        IEventsProcessor eventsProcessor)
    {
        this.window = window ?? throw new ArgumentNullException(nameof(window));
        this.eventsProcessor = eventsProcessor ?? throw new ArgumentNullException(nameof(eventsProcessor));
    }

    public void Run()
    {
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
}
