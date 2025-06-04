// <copyright file="Native.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Native;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using FinalEngine.Platform.Desktop.Native.Messaging;

/// <summary>
/// Provides managed wrappers for selected functions from the user32.dll library related to message handling in Windows.
/// </summary>
[ExcludeFromCodeCoverage]
internal static partial class Native
{
    /// <summary>
    /// Dispatches a message to a window procedure.
    /// </summary>
    ///
    /// <param name="lpMsg">
    /// Specifies a <see cref="NativeMessage"/> structure that represents the message to be dispatched.
    /// </param>
    ///
    /// <returns>
    /// Returns an <see cref="int"/> that represents the result of the message processing, typically the return value from the window procedure.
    /// </returns>
    [LibraryImport("user32.dll", EntryPoint = "DispatchMessageA")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int DispatchMessage(ref NativeMessage lpMsg);

    /// <summary>
    /// Retrieves a message from the calling thread's message queue.
    /// </summary>
    ///
    /// <param name="lpMsg">
    /// Specifies an output <see cref="NativeMessage"/> structure that receives message information.
    /// </param>
    ///
    /// <param name="hWnd">
    /// Specifies an <see cref="IntPtr"/> that represents the handle to the window whose messages are to be retrieved.
    /// </param>
    ///
    /// <param name="wMsgFilterMin">
    /// Specifies an <see cref="int"/> that represents the minimum value in the range of message codes to be retrieved.
    /// </param>
    ///
    /// <param name="wMsgFilterMax">
    /// Specifies an <see cref="int"/> that represents the maximum value in the range of message codes to be retrieved.
    /// </param>
    ///
    /// <returns>
    /// Returns a non-zero <see cref="int"/> if a message was retrieved; otherwise, returns zero.
    /// </returns>
    [LibraryImport("user32.dll", EntryPoint = "GetMessageA")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int GetMessage(
        out NativeMessage lpMsg,
        IntPtr hWnd,
        int wMsgFilterMin,
        int wMsgFilterMax);

    /// <summary>
    /// Retrieves a message from the calling thread's message queue without removing it.
    /// </summary>
    ///
    /// <param name="lpMsg">
    /// Specifies an output <see cref="NativeMessage"/> structure that receives message information.
    /// </param>
    ///
    /// <param name="hWnd">
    /// Specifies an <see cref="IntPtr"/> that represents the handle to the window whose messages are to be retrieved.
    /// </param>
    ///
    /// <param name="wMsgFilterMin">
    /// Specifies an <see cref="int"/> that represents the minimum value in the range of message codes to be retrieved.
    /// </param>
    ///
    /// <param name="wMsgFilterMax">
    /// Specifies an <see cref="int"/> that represents the maximum value in the range of message codes to be retrieved.
    /// </param>
    ///
    /// <param name="wRemoveMsg">
    /// Specifies an <see cref="int"/> that determines how messages are to be handled.
    /// </param>
    ///
    /// <returns>
    /// Returns a non-zero <see cref="int"/> if a message was retrieved; otherwise, returns zero.
    /// </returns>
    [LibraryImport("user32.dll", EntryPoint = "PeekMessageA")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int PeekMessage(
        out NativeMessage lpMsg,
        IntPtr hWnd,
        int wMsgFilterMin,
        int wMsgFilterMax,
        int wRemoveMsg);

    /// <summary>
    /// Indicates to the system that a thread has made a request to terminate by posting a quit message to the message queue.
    /// </summary>
    ///
    /// <param name="exitCode">
    /// Specifies an <see cref="int"/> that represents the application exit code to be used by the process and returned to the system.
    /// </param>
    [LibraryImport("user32.dll", EntryPoint = "PostQuitMessage")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial void PostQuitMessage(int exitCode);

    /// <summary>
    /// Translates virtual-key messages into character messages.
    /// </summary>
    ///
    /// <param name="lpMsg">
    /// Specifies a <see cref="NativeMessage"/> structure that represents the message to be translated.
    /// </param>
    ///
    /// <returns>
    /// Returns a non-zero <see cref="int"/> if the message was translated and posted to the message queue; otherwise, returns zero.
    /// </returns>
    [LibraryImport("user32.dll", EntryPoint = "TranslateMessage")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int TranslateMessage(ref NativeMessage lpMsg);
}
