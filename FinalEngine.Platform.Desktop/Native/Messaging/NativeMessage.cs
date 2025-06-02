// <copyright file="NativeMessage.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Native.Messaging;

using System.Runtime.InteropServices;

public enum NativeMessageCode : uint
{
    NCDestroy = 130,
}

[StructLayout(LayoutKind.Sequential)]
internal struct NativeMessage
{
    internal IntPtr Handle { get; set; }

    internal NativeMessageCode MessageCode { get; set; }

    internal IntPtr WParam { get; set; }

    internal IntPtr LParam { get; set; }
}
