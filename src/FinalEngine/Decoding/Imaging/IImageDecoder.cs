// <copyright file="IImageDecoder.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Decoding.Imaging;

public interface IImageDecoder
{
    DecodedImage Decode(string filePath);
}
