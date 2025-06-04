// <copyright file="NativeAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Invocation.Native;

using System.Diagnostics.CodeAnalysis;
using FinalEngine.Platform.Desktop.Native;
using FinalEngine.Platform.Desktop.Native.Messaging;

/// <summary>
/// Provides a standard implementation of an <see cref="INativeAdapter"/>.
/// </summary>
///
/// <seealso cref="INativeAdapter"/>
[ExcludeFromCodeCoverage]
internal sealed class NativeAdapter : INativeAdapter
{
    /// <inheritdoc/>
    public int DispatchMessage(ref NativeMessage lpMsg)
    {
        return Native.DispatchMessage(ref lpMsg);
    }

    /// <inheritdoc/>
    public int GetMessage(out NativeMessage lpMsg, nint hWnd, int wMsgFilterMin, int wMsgFilterMax)
    {
        return Native.GetMessage(out lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax);
    }

    /// <inheritdoc/>
    public int PeekMessage(out NativeMessage lpMsg, nint hWnd, int wMsgFilterMin, int wMsgFilterMax, int wRemoveMsg)
    {
        return Native.PeekMessage(out lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax, wRemoveMsg);
    }

    /// <inheritdoc/>
    public void PostQuitMessage(int exitCode)
    {
        Native.PostQuitMessage(exitCode);
    }

    /// <inheritdoc/>
    public int TranslateMessage(ref NativeMessage lpMsg)
    {
        return Native.TranslateMessage(ref lpMsg);
    }
}
