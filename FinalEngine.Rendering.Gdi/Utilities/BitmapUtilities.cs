// <copyright file="BitmapUtilities.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Utilities;

using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Adapters.Drawing;

internal static class BitmapUtilities
{
    public static void WritePremultipliedRgbaToBgraBitmap(IBitmapAdapter bitmap, ReadOnlySpan<byte> pixels, PixelFormat format = PixelFormat.Format32bppPArgb)
    {
        ArgumentNullException.ThrowIfNull(bitmap);
        ArgumentOutOfRangeException.ThrowIfLessThan(pixels.Length, bitmap.Width * bitmap.Height * 4);

        int width = bitmap.Width;
        int height = bitmap.Height;

        using (var data = bitmap.LockBits(ImageLockMode.WriteOnly, format))
        {
            int stride = data.Stride;

            byte[] destBuffer = new byte[stride * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int srcIndex = (y * width + x) * 4;
                    int destIndex = y * stride + x * 4;

                    byte r = pixels[srcIndex + 0];
                    byte g = pixels[srcIndex + 1];
                    byte b = pixels[srcIndex + 2];
                    byte a = pixels[srcIndex + 3];

                    // Pre-multiply channels (for PixelFormat.Format32bppPArgb).
                    float alpha = a / 255f;

                    r = (byte)(r * alpha);
                    g = (byte)(g * alpha);
                    b = (byte)(b * alpha);

                    // RGBA -> BGRA (GDI+ uses BGRA channel order unless indexed).
                    destBuffer[destIndex + 0] = b;
                    destBuffer[destIndex + 1] = g;
                    destBuffer[destIndex + 2] = r;
                    destBuffer[destIndex + 3] = a;
                }
            }

            Marshal.Copy(destBuffer, 0, data.Scan0, destBuffer.Length);
        }
    }
}
