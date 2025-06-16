// <copyright file="SpriteBatcher.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Batching;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Primitives;

internal sealed class SpriteBatcher : ISpriteBatcher
{
    private const int MaxCapacity = 10000;

    private readonly IInputAssembler inputAssembler;

    private readonly List<SpriteVertex> vertices;

    public SpriteBatcher(IInputAssembler inputAssembler)
    {
        this.inputAssembler = inputAssembler ?? throw new ArgumentNullException(nameof(inputAssembler));

        this.MaxVertexCount = MaxCapacity * 4;
        this.MaxIndexCount = MaxCapacity * 6;

        this.vertices = new List<SpriteVertex>(this.MaxVertexCount);
    }

    public int CurrentIndexCount { get; private set; }

    public int CurrentVertexCount { get; private set; }

    public int MaxIndexCount { get; }

    public int MaxVertexCount { get; }

    public bool ShouldReset
    {
        get { return this.CurrentVertexCount >= this.MaxVertexCount; }
    }

    public void Batch(float textureSlotIndex, Color color, Vector2 origin, Vector2 position, float rotation, Vector2 scale, int textureWidth, int textureHeight)
    {
        float x = position.X;
        float y = position.Y;

        float dx = -origin.X;
        float dy = -origin.Y;

        float w = scale.X * textureWidth;
        float h = scale.Y * textureHeight;

        float cos = (float)Math.Cos(rotation);
        float sin = (float)Math.Sin(rotation);

        var vecColor = new Vector4(
            color.R / 255.0f,
            color.G / 255.0f,
            color.B / 255.0f,
            color.A / 255.0f);

        this.vertices.Add(new SpriteVertex()
        {
            Position = new Vector2(x + ((dx + w) * cos) - (dy * sin), y + ((dx + w) * sin) + (dy * cos)),
            Color = vecColor,
            TextureCoordinate = new Vector2(1, 1),
            TextureSlotIndex = textureSlotIndex,
        });

        this.vertices.Add(new SpriteVertex()
        {
            Position = new Vector2(x + (dx * cos) - (dy * sin), y + (dx * sin) + (dy * cos)),
            Color = vecColor,
            TextureCoordinate = new Vector2(0, 1),
            TextureSlotIndex = textureSlotIndex,
        });

        this.vertices.Add(new SpriteVertex()
        {
            Position = new Vector2(x + (dx * cos) - ((dy + h) * sin), y + (dx * sin) + ((dy + h) * cos)),
            Color = vecColor,
            TextureCoordinate = new Vector2(0, 0),
            TextureSlotIndex = textureSlotIndex,
        });

        this.vertices.Add(new SpriteVertex()
        {
            Position = new Vector2(x + ((dx + w) * cos) - ((dy + h) * sin), y + ((dx + w) * sin) + ((dy + h) * cos)),
            Color = vecColor,
            TextureCoordinate = new Vector2(1, 0),
            TextureSlotIndex = textureSlotIndex,
        });

        this.CurrentIndexCount += 6;
        this.CurrentVertexCount += 4;
    }

    public void Reset()
    {
        this.vertices.Clear();
        this.CurrentIndexCount = 0;
        this.CurrentVertexCount = 0;
    }

    public void Update(IVertexBuffer vertexBuffer)
    {
        ArgumentNullException.ThrowIfNull(vertexBuffer);
        this.inputAssembler.UpdateVertexBuffer(vertexBuffer, this.vertices, SpriteVertex.SizeInBytes);
    }
}
