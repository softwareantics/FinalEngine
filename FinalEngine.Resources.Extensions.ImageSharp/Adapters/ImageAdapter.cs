// <copyright file="ImageInvoker.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources.Extensions.Adapters;

using System.Diagnostics.CodeAnalysis;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

[ExcludeFromCodeCoverage]
internal sealed class ImageAdapter : IImageAdapter
{
    public Image<TPixel> Load<TPixel>(Stream stream)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        return Image.Load<TPixel>(stream);
    }
}
