// <copyright file="WinFormsEventsProcessor.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System.Runtime.InteropServices;
using FinalEngine.Platform.Adapters.Applications;
using FinalEngine.Platform.Adapters.Native;
using Microsoft.Extensions.Logging;

internal sealed class WinFormsEventsProcessor : IEventsProcessor
{
    private readonly IApplicationAdapter application;

    private readonly ILogger<WinFormsEventsProcessor> logger;

    private readonly INativeAdapter native;

    public WinFormsEventsProcessor(
        ILogger<WinFormsEventsProcessor> logger,
        INativeAdapter native,
        IApplicationAdapter application)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.native = native ?? throw new ArgumentNullException(nameof(native));
        this.application = application ?? throw new ArgumentNullException(nameof(application));

        this.CanProcessEvents = true;

        this.logger.LogInformation("Events processor initialized and ready to process events.");
    }

    public bool CanProcessEvents { get; private set; }

    public void ProcessEvents()
    {
        if (!this.CanProcessEvents)
        {
            this.logger.LogWarning("Events processor is not in a state to process events. Exiting the processing loop.");
            return;
        }

        while (this.native.PeekMessage(out _, nint.Zero, 0, 0, 0) != 0)
        {
            int result = this.native.GetMessage(out var message, nint.Zero, 0, 0);

            if (result == -1)
            {
                this.logger.LogError("An error occurred while retrieving a message from the Windows message queue. Error: {Error}", Marshal.GetLastWin32Error());
                throw new InvalidOperationException($"An error happened in the messaging loop while processing windows messages. Error: {Marshal.GetLastWin32Error()}");
            }

            if (result == 0)
            {
                this.CanProcessEvents = false;
                this.logger.LogDebug("No more messages to process in the queue. Exiting the processing loop.");
                return;
            }

            var appMessage = new Message()
            {
                HWnd = message.Handle,
                LParam = message.LParam,
                Msg = (int)message.MessageCode,
                WParam = message.WParam,
            };

            if (!this.application.FilterMessage(ref appMessage))
            {
                this.native.TranslateMessage(ref message);
                this.native.DispatchMessage(ref message);
            }
        }
    }
}
