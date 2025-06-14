// <copyright file="GdiTexture2D.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Adapters.Drawing;

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

        using (var data = this.bitmap.LockBits(ImageLockMode.WriteOnly, Format))
        {
            int stride = data.Stride;

            byte[] destBuffer = new byte[stride * this.Height];

            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    int srcIndex = ((y * this.Width) + x) * 4;
                    int destIndex = (y * stride) + (x * 4);

                    byte r = pixels[srcIndex + 0];
                    byte g = pixels[srcIndex + 1];
                    byte b = pixels[srcIndex + 2];
                    byte a = pixels[srcIndex + 3];

                    // Pre-multiply channels (for PixelFormat.Format32bppPArgb).
                    float alpha = a / 255f;

                    r = (byte)(r * alpha);
                    g = (byte)(g * alpha);
                    b = (byte)(b * alpha);

                    destBuffer[destIndex + 0] = b;
                    destBuffer[destIndex + 1] = g;
                    destBuffer[destIndex + 2] = r;
                    destBuffer[destIndex + 3] = a;
                }
            }

            Marshal.Copy(destBuffer, 0, data.Scan0, destBuffer.Length);
        }
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
