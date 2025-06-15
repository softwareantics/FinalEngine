// <copyright file="GdiRenderResourceFactory.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using FinalEngine.Rendering.Adapters.Drawing;
using FinalEngine.Rendering.Textures;

internal sealed class GdiRenderResourceFactory : IRenderResourceFactory
{
    private readonly IBitmapAdapter.BitmapAdapterFactory createBitmap;

    public GdiRenderResourceFactory(IBitmapAdapter.BitmapAdapterFactory createBitmap)
    {
        this.createBitmap = createBitmap ?? throw new ArgumentNullException(nameof(createBitmap));
    }

    public ITexture2D CreateTexture(int width, int height, ReadOnlySpan<byte> pixels)
    {
        return new GdiTexture2D(this.createBitmap, width, height, pixels);
    }
}
