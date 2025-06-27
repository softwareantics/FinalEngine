// <copyright file="Texture2DDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using FinalEngine.Decoding.Imaging;

public readonly record struct Texture2DDescription(
    int Width,
    int Height,
    PixelFormat Format,
    SizedFormat InternalFormat,
    PixelType PixelType,
    TextureFilterMode MinFilter,
    TextureFilterMode MagFilter,
    TextureWrapMode WrapS,
    TextureWrapMode WrapT,
    bool GenerateMipmaps)
{
    public Texture2DDescription()
        : this(
            Width: 0,
            Height: 0,
            Format: PixelFormat.Rgba,
            InternalFormat: SizedFormat.Rgba8,
            PixelType: PixelType.Byte,
            MinFilter: TextureFilterMode.Nearest,
            MagFilter: TextureFilterMode.Nearest,
            WrapS: TextureWrapMode.Repeat,
            WrapT: TextureWrapMode.Repeat,
            GenerateMipmaps: false)
    {
    }
}
