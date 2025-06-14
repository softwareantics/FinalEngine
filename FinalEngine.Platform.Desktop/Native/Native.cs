// <copyright file="Native.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Native;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using FinalEngine.Platform.Native.Messaging;

[ExcludeFromCodeCoverage]
internal static partial class Native
{
    [LibraryImport("user32.dll", EntryPoint = "DispatchMessageA")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int DispatchMessage(ref NativeMessage lpMsg);

    [LibraryImport("user32.dll", EntryPoint = "GetMessageA")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int GetMessage(
        out NativeMessage lpMsg,
        IntPtr hWnd,
        int wMsgFilterMin,
        int wMsgFilterMax);

    [LibraryImport("user32.dll", EntryPoint = "PeekMessageA")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int PeekMessage(
        out NativeMessage lpMsg,
        IntPtr hWnd,
        int wMsgFilterMin,
        int wMsgFilterMax,
        int wRemoveMsg);

    [LibraryImport("user32.dll", EntryPoint = "PostQuitMessage")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial void PostQuitMessage(int exitCode);

    [LibraryImport("user32.dll", EntryPoint = "TranslateMessage")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static partial int TranslateMessage(ref NativeMessage lpMsg);
}
