// <copyright file="IRenderResourceFactory.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using FinalEngine.Rendering.Textures;

public interface IRenderResourceFactory
{
    ITexture2D CreateTexture(int width, int height, ReadOnlySpan<byte> pixels);
}
