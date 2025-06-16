// <copyright file="ISpriteBatcher.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Batching;

using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Buffers;

internal interface ISpriteBatcher
{
    int CurrentIndexCount { get; }

    int CurrentVertexCount { get; }

    int MaxIndexCount { get; }

    int MaxVertexCount { get; }

    bool ShouldReset { get; }

    void Batch(float textureSlotIndex, Color color, Vector2 origin, Vector2 position, float rotation, Vector2 scale, int textureWidth, int textureHeight);

    void Reset();

    void Update(IVertexBuffer vertexBuffer);
}
