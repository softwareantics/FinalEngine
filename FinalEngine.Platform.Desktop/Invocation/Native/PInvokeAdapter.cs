// <copyright file="PInvokeAdapter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Invocation.Native;

using FinalEngine.Platform.Desktop.Native;
using FinalEngine.Platform.Desktop.Native.Messaging;

internal sealed class PInvokeAdapter : IPInvokeAdapter
{
    public int DispatchMessage(ref NativeMessage lpMsg)
    {
        return PInvoke.DispatchMessage(ref lpMsg);
    }

    public int GetMessage(out NativeMessage lpMsg, nint hWnd, int wMsgFilterMin, int wMsgFilterMax)
    {
        return PInvoke.GetMessage(out lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax);
    }

    public int PeekMessage(out NativeMessage lpMsg, nint hWnd, int wMsgFilterMin, int wMsgFilterMax, int wRemoveMsg)
    {
        return PInvoke.PeekMessage(out lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax, wRemoveMsg);
    }

    public int TranslateMessage(ref NativeMessage lpMsg)
    {
        return PInvoke.TranslateMessage(ref lpMsg);
    }
}
