// <copyright file="INativeAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Invocation.Native;

using FinalEngine.Platform.Desktop.Native;
using FinalEngine.Platform.Desktop.Native.Messaging;

/// <summary>
/// Defines an interface that represents an adapter for the functions defined in <see cref="Native"/>.
/// </summary>
internal interface INativeAdapter
{
    /// <inheritdoc cref="Native.DispatchMessage(ref NativeMessage)"/>
    int DispatchMessage(ref NativeMessage lpMsg);

    /// <inheritdoc cref="Native.GetMessage(out NativeMessage, nint, int, int)"/>
    int GetMessage(
       out NativeMessage lpMsg,
       IntPtr hWnd,
       int wMsgFilterMin,
       int wMsgFilterMax);

    /// <inheritdoc cref="Native.PeekMessage(out NativeMessage, nint, int, int, int)"/>
    int PeekMessage(
        out NativeMessage lpMsg,
        IntPtr hWnd,
        int wMsgFilterMin,
        int wMsgFilterMax,
        int wRemoveMsg);

    /// <inheritdoc cref="Native.PostQuitMessage(int)"/>
    void PostQuitMessage(int exitCode);

    /// <inheritdoc cref="Native.TranslateMessage(ref NativeMessage)"/>
    int TranslateMessage(ref NativeMessage lpMsg);
}
