// <copyright file="NativeMessage.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Native.Messaging;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/// <summary>
///   Enumerates the available native message codes.
/// </summary>
internal enum NativeMessageCode : uint
{
    /// <summary>
    ///   The message code for a window closed event.
    /// </summary>
    Close = 0x0010,
}

/// <summary>
///   Represents a native message structure used for interop with the Windows API.
/// </summary>
[ExcludeFromCodeCoverage]
[StructLayout(LayoutKind.Sequential)]
internal struct NativeMessage
{
    /// <summary>
    ///   Gets or sets the handle.
    /// </summary>
    /// <value>
    ///   The handle.
    /// </value>
    public IntPtr Handle { get; set; }

    /// <summary>
    ///   Gets or sets the message code.
    /// </summary>
    /// <value>
    ///   The message code.
    /// </value>
    public NativeMessageCode MessageCode { get; set; }

    /// <summary>
    ///   Gets or sets the W parameter.
    /// </summary>
    /// <value>
    ///   The W parameter.
    /// </value>
    public IntPtr WParam { get; set; }

    /// <summary>
    ///   Gets or sets the L parameter.
    /// </summary>
    /// <value>
    ///   The L parameter.
    /// </value>
    public IntPtr LParam { get; set; }
}
