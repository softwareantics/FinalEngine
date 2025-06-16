// <copyright file="SpriteVertex.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Primitives;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Buffers;

[StructLayout(LayoutKind.Sequential)]
public struct SpriteVertex : IEquatable<SpriteVertex>
{
    public static readonly int SizeInBytes = Marshal.SizeOf<SpriteVertex>();

    public static IReadOnlyCollection<InputElement> InputElements
    {
        get
        {
            return
            [
                new (0, 2, InputElementType.Float, 0),
                new (1, 4, InputElementType.Float, 2 * sizeof(float)),
                new (2, 2, InputElementType.Float, 6 * sizeof(float)),
                new (3, 1, InputElementType.Float, 8 * sizeof(float)),
            ];
        }
    }

    public Vector2 Position { get; set; }

    public Vector4 Color { get; set; }

    public Vector2 TextureCoordinate { get; set; }

    public float TextureSlotIndex { get; set; }

    public static bool operator ==(SpriteVertex left, SpriteVertex right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SpriteVertex left, SpriteVertex right)
    {
        return !(left == right);
    }

    public readonly bool Equals(SpriteVertex other)
    {
        return this.Position == other.Position &&
               this.Color == other.Color &&
               this.TextureCoordinate == other.TextureCoordinate &&
               this.TextureSlotIndex == other.TextureSlotIndex;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is SpriteVertex vertex && this.Equals(vertex);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return (this.Position.GetHashCode() * accumulator) +
               (this.Color.GetHashCode() * accumulator) +
               (this.TextureCoordinate.GetHashCode() * accumulator) +
               (this.TextureSlotIndex.GetHashCode() * accumulator);
    }
}
