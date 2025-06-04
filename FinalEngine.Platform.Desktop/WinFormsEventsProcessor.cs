// <copyright file="WinFormsEventsProcessor.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop;

using System.Runtime.InteropServices;
using FinalEngine.Platform;

using FinalEngine.Platform.Desktop.Invocation.Applications;
using FinalEngine.Platform.Desktop.Invocation.Native;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a Windows Forms-based implementation of the <see cref="IEventsProcessor"/> interface for processing Windows messages.
/// </summary>
///
/// <remarks>
/// This implementation mimics the message loop behavior of a Windows Forms application, enabling message filtering and dispatching through native interop. It is designed for game engine use cases where high-performance, reliable message handling is required for input and other system-level events.
/// </remarks>
///
/// <seealso cref="IEventsProcessor"/>
internal sealed class WinFormsEventsProcessor : IEventsProcessor
{
    /// <summary>
    /// Specifies the <see cref="IApplicationAdapter"/> instance used to filter messages before they are processed.
    /// </summary>
    private readonly IApplicationAdapter application;

    /// <summary>
    /// Specifies an <see cref="ILogger{TCategoryName}"/> that is used for logging purposes.
    /// </summary>
    private readonly ILogger<WinFormsEventsProcessor> logger;

    /// <summary>
    /// Specifies the <see cref="INativeAdapter"/> instance used to interact with the Windows API for message processing.
    /// </summary>
    private readonly INativeAdapter native;

    /// <summary>
    /// Initializes a new instance of the <see cref="WinFormsEventsProcessor"/> class.
    /// </summary>
    ///
    /// <param name="logger">
    /// Specifies an <see cref="ILogger{TCategoryName}"/> that is used for logging purposes.
    /// </param>
    ///
    /// <param name="native">
    /// Specifies an <see cref="INativeAdapter"/> that is used to perform Windows API calls for message retrieval and dispatching.
    /// </param>
    ///
    /// <param name="application">
    /// Specifies an <see cref="IApplicationAdapter"/> that is used to filter messages before processing.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown when one of the following parameters is null:
    /// <list type="bullet">
    ///     <item>
    ///         <paramref name="logger"/>
    ///     </item>
    ///     <item>
    ///         <paramref name="native"/>
    ///     </item>
    ///     <item>
    ///         <paramref name="application"/>
    ///     </item>
    /// </list>
    /// </exception>
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

    /// <inheritdoc/>
    public bool CanProcessEvents { get; private set; }

    /// <summary>
    /// Processes all pending Windows messages from the message queue.
    /// </summary>
    ///
    /// <remarks>
    /// This method continuously retrieves and dispatches Windows messages until the queue is empty or <see cref="CanProcessEvents"/> is set to <c>false</c>. If <c>PeekMessage</c> or <c>GetMessage</c> returns an error, an exception is thrown with the last Win32 error. Messages are optionally filtered through the configured <see cref="IApplicationAdapter"/> before being passed to the Windows message system via <c>TranslateMessage</c> and <c>DispatchMessage</c>.
    /// </remarks>
    ///
    /// <exception cref="InvalidOperationException">
    /// Thrown when a critical error occurs during message retrieval from the Windows message queue.
    /// </exception>
    public void ProcessEvents()
    {
        if (!this.CanProcessEvents)
        {
            this.logger.LogWarning("Events processor is not in a state to process events. Exiting the processing loop.");
            return;
        }

        while (this.native.PeekMessage(out var message, IntPtr.Zero, 0, 0, 0) != 0)
        {
            int result = this.native.GetMessage(out message, IntPtr.Zero, 0, 0);

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
