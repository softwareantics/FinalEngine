// <copyright file="Point.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Native.Messaging;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

/// <summary>
/// Represents a point in 2D space, defined by its x and y coordinates.
/// </summary>
/// <remarks>
/// Corresponds to the native POINT structure used by the Windows API to define a point's coordinates.
/// </remarks>
[ExcludeFromCodeCoverage]
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Required for interop")]
internal struct Point
{
    /// <summary>
    /// The x-coordinate of the point.
    /// </summary>
    public int X;

    /// <summary>
    /// The y-coordinate of the point.
    /// </summary>
    public int Y;
}
