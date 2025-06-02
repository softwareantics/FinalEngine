// <copyright file="WinFormsEventsProcessor.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop;

using System.Runtime.InteropServices;
using FinalEngine.Platform.Desktop.Invocation;
using FinalEngine.Platform.Desktop.Invocation.Native;

/// <summary>
///   Provides a Windows Forms implementation of the <see cref="IEventsProcessor"/> interface for processing Windows messages.
/// </summary>
/// <remarks>
///   This class is designed to process Windows messages in a manner similar to a Windows Forms application, allowing for message filtering and processing. It uses native interop to interact with the Windows API for message retrieval and dispatching. The implementation is suitable for use in a game engine context where fast and reliable message processing is required for input handling and other events.
/// </remarks>
/// <seealso cref="IEventsProcessor"/>
internal sealed class WinFormsEventsProcessor : IEventsProcessor
{
    /// <summary>
    ///   The application adapter used to filter messages before they are processed.
    /// </summary>
    private readonly IApplicationAdapter applicationAdapter;

    /// <summary>
    ///   The native adapter used to interact with the Windows API for message processing.
    /// </summary>
    private readonly INativeAdapter nativeAdapter;

    /// <summary>
    ///   Initializes a new instance of the <see cref="WinFormsEventsProcessor"/> class.
    /// </summary>
    /// <param name="nativeAdapter">
    ///   The native adapter used to interact with the Windows API for message processing.
    /// </param>
    /// <param name="applicationAdapter">
    ///   The application adapter used to filter messages before they are processed.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   The specified <paramref name="nativeAdapter"/> parameter is null.
    /// </exception>
    public WinFormsEventsProcessor(INativeAdapter nativeAdapter, IApplicationAdapter applicationAdapter)
    {
        this.nativeAdapter = nativeAdapter ?? throw new ArgumentNullException(nameof(nativeAdapter));
        this.applicationAdapter = applicationAdapter ?? throw new ArgumentNullException(nameof(applicationAdapter));

        this.CanProcessEvents = true;
    }

    /// <inheritdoc/>
    public bool CanProcessEvents { get; private set; }

    /// <summary>
    ///   Processes the events that are currently in the message queue.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///   An error happened in the messaging loop while processing windows messages.
    /// </exception>
    /// <remarks>
    ///   This method retrieves messages from the message queue and dispatches them to the appropriate window procedure. It will continue to process messages until there are no more messages available or until the <see cref="CanProcessEvents"/> property is set to <c>false</c>. If an error occurs while retrieving or dispatching messages, an <see cref="InvalidOperationException"/> will be thrown with the last Win32 error code. This implementation attempts to mimic the behavior of a typical Windows Forms application message loop, allowing for message filtering and processing similar to what is done in a WinForms application whilst still being suitable for use in a game engine context where fast and reliable message processing is required for input handling and other events.
    /// </remarks>
    public void ProcessEvents()
    {
        if (!this.CanProcessEvents)
        {
            return;
        }

        while (this.nativeAdapter.PeekMessage(out var message, IntPtr.Zero, 0, 0, 0) != 0)
        {
            int result = this.nativeAdapter.GetMessage(out message, IntPtr.Zero, 0, 0);

            if (result == -1)
            {
                throw new InvalidOperationException($"An error happened in the messaging loop while processing windows messages. Error: {Marshal.GetLastWin32Error()}");
            }

            if (result == 0)
            {
                this.CanProcessEvents = false;
                return;
            }

            var appMessage = new Message()
            {
                HWnd = message.Handle,
                LParam = message.LParam,
                Msg = (int)message.MessageCode,
                WParam = message.WParam,
            };

            if (!this.applicationAdapter.FilterMessage(ref appMessage))
            {
                this.nativeAdapter.TranslateMessage(ref message);
                this.nativeAdapter.DispatchMessage(ref message);
            }
        }
    }
}
