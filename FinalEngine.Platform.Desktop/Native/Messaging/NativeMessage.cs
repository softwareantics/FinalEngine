// <copyright file="NativeMessage.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Native.Messaging;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/// <summary>
/// Enumerates the available Windows message codes.
/// </summary>
internal enum NativeMessageCode : uint
{
    /// <summary>
    /// The WM_CLOSE message is sent as a signal that a window or an application should terminate.
    /// </summary>
    Close = 0x0010,
}

/// <summary>
/// Represents a Windows message structure (MSG) used by the Windows API to store message information retrieved from a thread's message queue.
/// </summary>
/// <remarks>
/// Corresponds to the native MSG structure defined in the Windows API.
/// </remarks>
[ExcludeFromCodeCoverage]
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Required for interop")]
internal struct NativeMessage
{
    /// <summary>
    /// Handle to the window to which the message is directed.
    /// </summary>
    public IntPtr Handle;

    /// <summary>
    /// Specifies the message identifier.
    /// </summary>
    public NativeMessageCode MessageCode;

    /// <summary>
    /// Additional message information. The contents depend on the value of the <see cref="MessageCode"/> member.
    /// </summary>
    public IntPtr WParam;

    /// <summary>
    /// Additional message information. The contents depend on the value of the <see cref="MessageCode"/> member.
    /// </summary>
    public IntPtr LParam;

    /// <summary>
    /// The time at which the message was posted. This value is retrieved from the system tick count.
    /// </summary>
    public uint Time;

    /// <summary>
    /// The cursor position, in screen coordinates, when the message was posted.
    /// </summary>
    public Point Point;
}
