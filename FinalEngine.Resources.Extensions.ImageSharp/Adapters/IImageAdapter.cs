// <copyright file="IImageAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources.Extensions.Adapters;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

internal interface IImageAdapter
{
    Image<TPixel> Load<TPixel>(Stream stream)
        where TPixel : unmanaged, IPixel<TPixel>;
}
