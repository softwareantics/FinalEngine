// <copyright file="QuadVertex.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Primitives;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Buffers;

[StructLayout(LayoutKind.Sequential)]
public struct QuadVertex : IEquatable<QuadVertex>
{
    public static readonly int SizeInBytes = Marshal.SizeOf<QuadVertex>();

    public static IReadOnlyCollection<InputElement> InputElements
    {
        get
        {
            return
            [
                new (0, 2, InputElementType.Float, 0),
                new (1, 2, InputElementType.Float, 2 * sizeof(float)),
            ];
        }
    }

    public Vector2 Position { get; set; }

    public Vector2 TextureCoordinate { get; set; }

    public static bool operator ==(QuadVertex left, QuadVertex right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(QuadVertex left, QuadVertex right)
    {
        return !(left == right);
    }

    public readonly bool Equals(QuadVertex other)
    {
        return this.Position == other.Position && this.TextureCoordinate == other.TextureCoordinate;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is QuadVertex vertex && this.Equals(vertex);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return (this.Position.GetHashCode() * accumulator) +
               (this.TextureCoordinate.GetHashCode() * accumulator);
    }
}
