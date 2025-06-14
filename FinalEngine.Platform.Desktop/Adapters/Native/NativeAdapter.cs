// <copyright file="NativeAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Adapters.Native;

using System.Diagnostics.CodeAnalysis;
using FinalEngine.Platform.Native;
using FinalEngine.Platform.Native.Messaging;

[ExcludeFromCodeCoverage]
internal sealed class NativeAdapter : INativeAdapter
{
    public int DispatchMessage(ref NativeMessage lpMsg)
    {
        return Native.DispatchMessage(ref lpMsg);
    }

    public int GetMessage(out NativeMessage lpMsg, nint hWnd, int wMsgFilterMin, int wMsgFilterMax)
    {
        return Native.GetMessage(out lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax);
    }

    public int PeekMessage(out NativeMessage lpMsg, nint hWnd, int wMsgFilterMin, int wMsgFilterMax, int wRemoveMsg)
    {
        return Native.PeekMessage(out lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax, wRemoveMsg);
    }

    public void PostQuitMessage(int exitCode)
    {
        Native.PostQuitMessage(exitCode);
    }

    public int TranslateMessage(ref NativeMessage lpMsg)
    {
        return Native.TranslateMessage(ref lpMsg);
    }
}
