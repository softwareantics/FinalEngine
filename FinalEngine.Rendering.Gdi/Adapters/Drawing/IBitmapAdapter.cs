// <copyright file="IBitmapAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Adapters.Drawing;

using System.Drawing.Imaging;
using FinalEngine.Rendering.Adapters.Imaging;

internal interface IBitmapAdapter : IDisposable
{
    internal delegate IBitmapAdapter BitmapAdapterFactory(int width, int height, PixelFormat format);

    int Height { get; }

    PixelFormat PixelFormat { get; }

    int Width { get; }

    Bitmap GetImplementation();

    IBitmapDataAdapter LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format);

    IBitmapDataAdapter LockBits(ImageLockMode flags, PixelFormat format);

    void UnlockBits(IBitmapDataAdapter bitmapdata);
}
