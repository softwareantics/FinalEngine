// <copyright file="DecodedImage.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Decoding.Imaging;

public readonly record struct DecodedImage(
    int Width,
    int Height,
    PixelFormat Format,
    SizedFormat InternalFormat,
    PixelType PixelType,
    IReadOnlyCollection<byte> Data);
