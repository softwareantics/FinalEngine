// <copyright file="SpriteVertex.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Primitives;

using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Buffers;

[StructLayout(LayoutKind.Sequential)]
public readonly record struct SpriteVertex(
    Vector2 Position,
    Vector4 Color,
    Vector2 TextureCoordinate,
    float TextureSlotIndex)
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
}
