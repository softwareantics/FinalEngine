// <copyright file="PInvoke.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Native;

using FinalEngine.Platform.Desktop.Native.Messaging;

using System.Runtime.InteropServices;

/// <summary>
///   Provides managed wrappers for selected functions from the user32.dll library related to message handling in Windows.
/// </summary>
internal static partial class PInvoke
{
    /// <summary>
    ///   Dispatches a message to a window procedure.
    /// </summary>
    /// <param name="lpMsg">
    ///   A pointer to a structure that contains the message.
    /// </param>
    /// <returns>
    ///   The result of the message processing; typically, it is the result value of the window procedure.
    /// </returns>
    [LibraryImport("user32.dll", EntryPoint = "DispatchMessageA")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int DispatchMessage(ref NativeMessage lpMsg);

    /// <summary>
    ///   Retrieves a message from the calling thread's message queue.
    /// </summary>
    /// <param name="lpMsg">
    ///   A pointer to a structure that receives message information.
    /// </param>
    /// <param name="hWnd">
    ///   A handle to the window whose messages are to be retrieved.
    /// </param>
    /// <param name="wMsgFilterMin">
    ///   The value of the first message in the range of messages to be examined.
    /// </param>
    /// <param name="wMsgFilterMax">
    ///   The value of the last message in the range of messages to be examined.
    /// </param>
    /// <returns>
    ///   Non-zero if a message was retrieved, zero if no messages are available.
    /// </returns>
    [LibraryImport("user32.dll", EntryPoint = "GetMessageA")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int GetMessage(
        out NativeMessage lpMsg,
        IntPtr hWnd,
        int wMsgFilterMin,
        int wMsgFilterMax);

    /// <summary>
    ///   Retrieves a message from the calling thread's message queue without removing it.
    /// </summary>
    /// <param name="lpMsg">
    ///   A pointer to a structure that receives message information.
    /// </param>
    /// <param name="hWnd">
    ///   A handle to the window whose messages are to be retrieved.
    /// </param>
    /// <param name="wMsgFilterMin">
    ///   The value of the first message in the range of messages to be examined.
    /// </param>
    /// <param name="wMsgFilterMax">
    ///   The value of the last message in the range of messages to be examined.
    /// </param>
    /// <param name="wRemoveMsg">
    ///   Specifies how messages are to be handled.
    /// </param>
    /// <returns>
    ///   Non-zero if a message was retrieved, zero if no messages are available.
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
    ///   Indicates to the system that a thread has made a request to terminate (post a quit message to the message queue).
    /// </summary>
    /// <param name="exitCode">
    ///   The application exit code to be used by the process and provided to the system.
    /// </param>
    [LibraryImport("user32.dll", EntryPoint = "PostQuitMessage")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial void PostQuitMessage(int exitCode);

    /// <summary>
    ///   Translates virtual-key messages into character messages.
    /// </summary>
    /// <param name="lpMsg">
    ///   A pointer to a structure that contains the message.
    /// </param>
    /// <returns>
    ///   Non-zero if the message has been translated and posted to the message queue, zero otherwise.
    /// </returns>
    [LibraryImport("user32.dll", EntryPoint = "TranslateMessage")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int TranslateMessage(ref NativeMessage lpMsg);
}
