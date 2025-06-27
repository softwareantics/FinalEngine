// <copyright file="Texture2DResourceLoader.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Loaders;

using System.IO.Abstractions;
using FinalEngine.Decoding.Imaging;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

internal sealed class Texture2DResourceLoader : ResourceLoaderBase<ITexture2D>
{
    private readonly IFileSystem fileSystem;

    private readonly IImageDecoder imageDecoder;

    private readonly IRenderResourceFactory resourceFactory;

    public Texture2DResourceLoader(IFileSystem fileSystem, IImageDecoder imageDecoder, IRenderResourceFactory resourceFactory)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.imageDecoder = imageDecoder ?? throw new ArgumentNullException(nameof(imageDecoder));
        this.resourceFactory = resourceFactory ?? throw new ArgumentNullException(nameof(resourceFactory));
    }

    public override ITexture2D LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified file does not exist: {filePath}", filePath);
        }

        var decoded = this.imageDecoder.Decode(filePath);

        //// TODO: TextureQualitySettings.

        var description = new Texture2DDescription()
        {
            Width = decoded.Width,
            Height = decoded.Height,
            PixelType = decoded.PixelType,
            WrapS = TextureWrapMode.Repeat,
            WrapT = TextureWrapMode.Repeat,
            GenerateMipmaps = true,
        };

        return this.resourceFactory.CreateTexture2D(
            description: description,
            data: decoded.Data,
            format: decoded.Format,
            internalFormat: decoded.InternalFormat);
    }
}
