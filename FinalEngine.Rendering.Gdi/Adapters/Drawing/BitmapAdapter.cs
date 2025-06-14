// <copyright file="BitmapAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Adapters.Drawing;

using System.Drawing;
using System.Drawing.Imaging;
using FinalEngine.Rendering.Adapters.Imaging;

internal sealed class BitmapAdapter : IBitmapAdapter
{
    private Bitmap? bitmap;

    private bool isDisposed;

    public BitmapAdapter(int width, int height, PixelFormat format)
    {
        this.bitmap = new Bitmap(width, height, format);
    }

    ~BitmapAdapter()
    {
        this.Dispose(false);
    }

    public int Height
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BitmapAdapter));
            return this.bitmap!.Height;
        }
    }

    public PixelFormat PixelFormat
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BitmapAdapter));
            return this.bitmap!.PixelFormat;
        }
    }

    public int Width
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BitmapAdapter));
            return this.bitmap!.Width;
        }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Bitmap GetImplementation()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BitmapAdapter));
        return this.bitmap!;
    }

    public IBitmapDataAdapter LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BitmapAdapter));
        return new BitmapDataAdapter(this, this.bitmap!.LockBits(rect, flags, format));
    }

    public IBitmapDataAdapter LockBits(ImageLockMode flags, PixelFormat format)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BitmapAdapter));
        return this.LockBits(new Rectangle(0, 0, this.bitmap!.Width, this.bitmap.Height), flags, format);
    }

    public void UnlockBits(IBitmapDataAdapter bitmapdata)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BitmapAdapter));
        ArgumentNullException.ThrowIfNull(bitmapdata);

        this.bitmap!.UnlockBits(bitmapdata.GetImplementation());
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
