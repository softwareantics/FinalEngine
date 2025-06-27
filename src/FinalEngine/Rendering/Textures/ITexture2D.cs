// <copyright file="ITexture2D.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using FinalEngine.Decoding.Imaging;
using FinalEngine.Resources;

public interface ITexture2D : IResource
{
    Texture2DDescription Description { get; }

    PixelFormat Format { get; }

    SizedFormat InternalFormat { get; }
}
