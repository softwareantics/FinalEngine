// <copyright file="Point.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Native.Messaging;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

[ExcludeFromCodeCoverage]
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Required for interop")]
internal struct Point
{
    public int X;

    public int Y;
}
