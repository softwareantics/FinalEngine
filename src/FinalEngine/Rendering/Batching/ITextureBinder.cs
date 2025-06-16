// <copyright file="ITextureBinder.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Batching;

using FinalEngine.Rendering.Textures;

internal interface ITextureBinder
{
    bool ShouldReset { get; }

    int GetTextureSlotIndex(ITexture2D texture);

    void Reset();
}
