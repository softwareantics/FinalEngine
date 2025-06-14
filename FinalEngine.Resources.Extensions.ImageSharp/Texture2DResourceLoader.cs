// <copyright file="Texture2DResourceLoader.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources.Extensions;

using System.IO.Abstractions;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources.Extensions.Adapters;
using SixLabors.ImageSharp.PixelFormats;

internal sealed class Texture2DResourceLoader : ResourceLoaderBase<ITexture2D>
{
    private readonly IFileSystem fileSystem;

    private readonly IImageAdapter imageSharp;

    private readonly IRenderResourceFactory resourceFactory;

    public Texture2DResourceLoader(IFileSystem fileSystem, IImageAdapter imageSharp, IRenderResourceFactory resourceFactory)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.imageSharp = imageSharp ?? throw new ArgumentNullException(nameof(imageSharp));
        this.resourceFactory = resourceFactory ?? throw new ArgumentNullException(nameof(resourceFactory));
    }

    public override ITexture2D LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified file does not exist: {filePath}", filePath);
        }

        using (var stream = this.fileSystem.File.OpenRead(filePath))
        {
            var image = this.imageSharp.Load<Rgba32>(stream);

            int width = image.Width;
            int height = image.Height;

            byte[] pixels = new byte[width * height * 4];
            image.CopyPixelDataTo(pixels);

            return this.resourceFactory.CreateTexture(width, height, pixels);
        }
    }
}
