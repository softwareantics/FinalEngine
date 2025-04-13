// <copyright file="SkyboxVertex.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Primitives;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Buffers;

[StructLayout(LayoutKind.Sequential)]
public struct SkyboxVertex : IEquatable<SkyboxVertex>
{
    public static readonly int SizeInBytes = Marshal.SizeOf<SkyboxVertex>();

    public static IReadOnlyCollection<InputElement> InputElements
    {
        get
        {
            return
            [
                new (0, 3, InputElementType.Float, 0),
            ];
        }
    }

    public Vector3 Position { get; set; }

    public static bool operator ==(SkyboxVertex left, SkyboxVertex right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SkyboxVertex left, SkyboxVertex right)
    {
        return !(left == right);
    }

    public readonly bool Equals(SkyboxVertex other)
    {
        return this.Position == other.Position;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is SkyboxVertex vertex && this.Equals(vertex);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return this.Position.GetHashCode() * accumulator;
    }
}
