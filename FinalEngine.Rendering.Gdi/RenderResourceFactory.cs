// <copyright file="RenderResourceFactory.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using FinalEngine.Rendering.Adapters.Drawing;
using FinalEngine.Rendering.Textures;

internal sealed class RenderResourceFactory : IRenderResourceFactory
{
    private readonly IBitmapAdapter.BitmapAdapterFactory createBitmap;

    public RenderResourceFactory(IBitmapAdapter.BitmapAdapterFactory createBitmap)
    {
        this.createBitmap = createBitmap ?? throw new ArgumentNullException(nameof(createBitmap));
    }

    public ITexture2D CreateTexture(int width, int height, ReadOnlySpan<byte> pixels)
    {
        return new GdiTexture2D(this.createBitmap, width, height, pixels);
    }
}
