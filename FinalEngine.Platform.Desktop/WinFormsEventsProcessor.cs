// <copyright file="WinFormsEventsProcessor.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop;

using System.Runtime.InteropServices;
using FinalEngine.Platform.Desktop.Invocation.Native;
using FinalEngine.Platform.Desktop.Native.Messaging;

internal sealed class WinFormsEventsProcessor : IEventsProcessor
{
    private readonly IPInvokeAdapter nativeAdapter;

    private bool canProcessEvents;

    public WinFormsEventsProcessor(IPInvokeAdapter nativeAdapter)
    {
        this.nativeAdapter = nativeAdapter ?? throw new ArgumentNullException(nameof(nativeAdapter));
        this.canProcessEvents = true;
    }

    public void ProcessEvents()
    {
        if (!this.canProcessEvents)
        {
            return;
        }

        while (this.nativeAdapter.PeekMessage(out var message, IntPtr.Zero, 0, 0, 0) != 0)
        {
            if (this.nativeAdapter.GetMessage(out message, IntPtr.Zero, 0, 0) == -1)
            {
                throw new InvalidOperationException($"An error happened in messaging loop while processing windows messages. Error: {Marshal.GetLastWin32Error()}");
            }

            if (message.MessageCode == NativeMessageCode.NCDestroy)
            {
                this.canProcessEvents = false;
            }

            var appMessage = new Message()
            {
                HWnd = message.Handle,
                LParam = message.LParam,
                Msg = (int)message.MessageCode,
                WParam = message.WParam,
            };

            if (!Application.FilterMessage(ref appMessage))
            {
                this.nativeAdapter.TranslateMessage(ref message);
                this.nativeAdapter.DispatchMessage(ref message);
            }
        }
    }
}
