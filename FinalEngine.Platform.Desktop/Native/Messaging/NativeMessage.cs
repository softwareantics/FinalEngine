// <copyright file="NativeMessage.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Native.Messaging;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/// <summary>
/// Enumerates the available native message codes.
/// </summary>
internal enum NativeMessageCode : uint
{
    /// <summary>
    /// The message code for a window closed event.
    /// </summary>
    Close = 0x0010,
}

/// <summary>
/// Provides a structure that represents a native message used for interop with the Windows API.
/// </summary>
[ExcludeFromCodeCoverage]
[StructLayout(LayoutKind.Sequential)]
internal struct NativeMessage
{
    /// <summary>
    /// Gets or sets an <see cref="IntPtr"/> that represents the handle associated with the native message.
    /// </summary>
    ///
    /// <value>
    /// Returns an <see cref="IntPtr"/> that represents the handle associated with the native message.
    /// </value>
    public IntPtr Handle { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="NativeMessageCode"/> that represents the message code.
    /// </summary>
    ///
    /// <value>
    /// Returns a <see cref="NativeMessageCode"/> that represents the message code.
    /// </value>
    public NativeMessageCode MessageCode { get; set; }

    /// <summary>
    /// Gets or sets an <see cref="IntPtr"/> that represents the W parameter of the native message.
    /// </summary>
    ///
    /// <value>
    /// Returns an <see cref="IntPtr"/> that represents the W parameter of the native message.
    /// </value>
    public IntPtr WParam { get; set; }

    /// <summary>
    /// Gets or sets an <see cref="IntPtr"/> that represents the L parameter of the native message.
    /// </summary>
    ///
    /// <value>
    /// Returns an <see cref="IntPtr"/> that represents the L parameter of the native message.
    /// </value>
    public IntPtr LParam { get; set; }
}
