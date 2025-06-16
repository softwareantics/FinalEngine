// <copyright file="TextureBinder.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Batching;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Textures;

internal sealed class TextureBinder : ITextureBinder
{
    private readonly IPipeline pipeline;

    private readonly Dictionary<ITexture2D, int> textureToSlotMap;

    public TextureBinder(IPipeline pipeline)
    {
        this.pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
        this.textureToSlotMap = new Dictionary<ITexture2D, int>(pipeline.MaxTextureSlots);
    }

    public bool ShouldReset
    {
        get { return this.textureToSlotMap.Count >= this.pipeline.MaxTextureSlots; }
    }

    public int GetTextureSlotIndex(ITexture2D texture)
    {
        ArgumentNullException.ThrowIfNull(texture);

        if (this.textureToSlotMap.TryGetValue(texture, out int value))
        {
            return value;
        }

        int slot = this.textureToSlotMap.Count;

        this.pipeline.SetTexture(texture, slot);
        this.pipeline.SetUniform($"u_textures[{slot}]", slot);

        this.textureToSlotMap.Add(texture, slot);

        return slot;
    }

    public void Reset()
    {
        this.textureToSlotMap.Clear();
    }
}
