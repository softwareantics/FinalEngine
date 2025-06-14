// <copyright file="NativeMessage.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Native.Messaging;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

internal enum NativeMessageCode : uint
{
    Close = 0x0010,
}

[ExcludeFromCodeCoverage]
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Required for interop")]
internal struct NativeMessage
{
    public IntPtr Handle;

    public NativeMessageCode MessageCode;

    public IntPtr WParam;

    public IntPtr LParam;

    public uint Time;

    public Point Point;
}
