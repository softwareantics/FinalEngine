// <copyright file="GdiTexture2D.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using System.Drawing.Imaging;
using FinalEngine.Rendering.Adapters.Drawing;
using FinalEngine.Rendering.Services;

internal sealed class GdiTexture2D : IGdiTexture2D
{
    private IBitmapAdapter? bitmap;

    private bool isDisposed;

    public GdiTexture2D(IBitmapAdapter.BitmapAdapterFactory createBitmap, int width, int height, ReadOnlySpan<byte> pixels)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0);
        ArgumentOutOfRangeException.ThrowIfLessThan(pixels.Length, width * height * 4);

        this.Width = width;
        this.Height = height;

        this.bitmap = createBitmap(width, height, Format);

        BitmapUtilities.WritePremultipliedRgbaToBgraBitmap(this.bitmap, pixels, Format);
    }

    ~GdiTexture2D()
    {
        this.Dispose(false);
    }

    public int Height { get; }

    public int Width { get; }

    private static PixelFormat Format
    {
        get { return PixelFormat.Format32bppPArgb; }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void DrawImageUnscaled(IGraphicsAdapter graphics, int x, int y)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(GdiTexture2D));
        ArgumentNullException.ThrowIfNull(graphics);

        graphics.DrawImageUnscaled(this.bitmap!, x, y);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.bitmap != null)
        {
            this.bitmap.Dispose();
            this.bitmap = null;
        }

        this.isDisposed = true;
    }
}
