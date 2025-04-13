// <copyright file="MeshVertex.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Primitives;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Buffers;

[StructLayout(LayoutKind.Sequential)]
public struct MeshVertex : IEquatable<MeshVertex>
{
    public static readonly int SizeInBytes = Marshal.SizeOf<MeshVertex>();

    public static IReadOnlyCollection<InputElement> InputElements
    {
        get
        {
            return
            [
                new (0, 3, InputElementType.Float, 0),
                new (1, 2, InputElementType.Float, 3 * sizeof(float)),
                new (2, 3, InputElementType.Float, 5 * sizeof(float)),
                new (3, 3, InputElementType.Float, 8 * sizeof(float)),
            ];
        }
    }

    public Vector3 Position { get; set; }

    public Vector2 TextureCoordinate { get; set; }

    public Vector3 Normal { get; set; }

    public Vector3 Tangent { get; set; }

    public static bool operator ==(MeshVertex left, MeshVertex right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MeshVertex left, MeshVertex right)
    {
        return !(left == right);
    }

    public static void CalculateNormals(MeshVertex[] vertices, int[] indices)
    {
        ArgumentNullException.ThrowIfNull(vertices, nameof(vertices));
        ArgumentNullException.ThrowIfNull(indices, nameof(indices));

        for (int i = 0; i < indices.Length; i += 3)
        {
            int i0 = indices[i];
            int i1 = indices[i + 1];
            int i2 = indices[i + 2];

            var v1 = vertices[i1].Position - vertices[i0].Position;
            var v2 = vertices[i2].Position - vertices[i0].Position;

            var normal = Vector3.Normalize(Vector3.Cross(v1, v2));

            vertices[i0].Normal = normal;
            vertices[i1].Normal = normal;
            vertices[i2].Normal = normal;
        }
    }

    public static void CalculateTangents(MeshVertex[] vertices, int[] indices)
    {
        ArgumentNullException.ThrowIfNull(vertices, nameof(vertices));
        ArgumentNullException.ThrowIfNull(indices, nameof(indices));

        for (int i = 0; i < indices.Length; i += 3)
        {
            int i0 = indices[i];
            int i1 = indices[i + 1];
            int i2 = indices[i + 2];

            var edge1 = vertices[i1].Position - vertices[i0].Position;
            var edge2 = vertices[i2].Position - vertices[i0].Position;

            float deltaU1 = vertices[i1].TextureCoordinate.X - vertices[i0].TextureCoordinate.X;
            float deltaU2 = vertices[i2].TextureCoordinate.X - vertices[i0].TextureCoordinate.X;
            float deltaV1 = vertices[i1].TextureCoordinate.Y - vertices[i0].TextureCoordinate.Y;
            float deltaV2 = vertices[i2].TextureCoordinate.Y - vertices[i0].TextureCoordinate.Y;

            float dividend = (deltaU1 * deltaV2) - (deltaU2 * deltaV1);
            float f = dividend == 0.0f ? 0.0f : 1.0f / dividend;

            var tangent = new Vector3(
                f * ((deltaV2 * edge1.X) - (deltaV1 * edge2.X)),
                f * ((deltaV2 * edge1.Y) - (deltaV1 * edge2.Y)),
                f * ((deltaV2 * edge1.Z) - (deltaV1 * edge2.Z)));

            vertices[i0].Tangent = tangent;
            vertices[i1].Tangent = tangent;
            vertices[i2].Tangent = tangent;
        }
    }

    public readonly bool Equals(MeshVertex other)
    {
        return this.Position == other.Position &&
               this.TextureCoordinate == other.TextureCoordinate &&
               this.Normal == other.Normal &&
               this.Tangent == other.Tangent;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is MeshVertex vertex && this.Equals(vertex);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return (this.Position.GetHashCode() * accumulator) +
               (this.TextureCoordinate.GetHashCode() * accumulator) +
               (this.Normal.GetHashCode() * accumulator) +
               (this.Tangent.GetHashCode() * accumulator);
    }
}
