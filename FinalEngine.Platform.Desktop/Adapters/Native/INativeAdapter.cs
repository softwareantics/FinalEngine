// <copyright file="INativeAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Adapters.Native;

using FinalEngine.Platform.Native.Messaging;

internal interface INativeAdapter
{
    int DispatchMessage(ref NativeMessage lpMsg);

    int GetMessage(
       out NativeMessage lpMsg,
       IntPtr hWnd,
       int wMsgFilterMin,
       int wMsgFilterMax);

    int PeekMessage(
        out NativeMessage lpMsg,
        IntPtr hWnd,
        int wMsgFilterMin,
        int wMsgFilterMax,
        int wRemoveMsg);

    void PostQuitMessage(int exitCode);

    int TranslateMessage(ref NativeMessage lpMsg);
}
