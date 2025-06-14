// <copyright file="BitmapDataAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Adapters.Imaging;

using System.Drawing.Imaging;
using FinalEngine.Rendering.Adapters.Drawing;

internal sealed class BitmapDataAdapter : IBitmapDataAdapter
{
    private readonly IBitmapAdapter bitmap;

    private readonly BitmapData? bitmapData;

    private bool isDisposed;

    public BitmapDataAdapter(IBitmapAdapter bitmap, BitmapData bitmapData)
    {
        this.bitmap = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
        this.bitmapData = bitmapData ?? throw new ArgumentNullException(nameof(bitmapData));
    }

    ~BitmapDataAdapter()
    {
        this.Dispose(false);
    }

    public nint Scan0
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BitmapDataAdapter));
            return this.bitmapData!.Scan0;
        }
    }

    public int Stride
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BitmapDataAdapter));
            return this.bitmapData!.Stride;
        }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public BitmapData GetImplementation()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BitmapDataAdapter));
        return this.bitmapData!;
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            this.bitmap.UnlockBits(this);
        }

        this.isDisposed = true;
    }
}
