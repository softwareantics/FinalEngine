// <copyright file="ImageSharpImageDecoder.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Decoding.Imaging;

using System.IO.Abstractions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

internal sealed class ImageSharpImageDecoder : IImageDecoder
{
    private readonly IFileSystem fileSystem;

    public ImageSharpImageDecoder(IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public DecodedImage Decode(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file '{filePath}' does not exist.", filePath);
        }

        using (var stream = this.fileSystem.File.OpenRead(filePath))
        {
            var image = Image.Load<Rgba32>(stream);

            int width = image.Width;
            int height = image.Height;

            byte[] pixels = new byte[width * height * 4];
            image.CopyPixelDataTo(pixels);

            return new DecodedImage(
                Width: width,
                Height: height,
                Format: PixelFormat.Rgba,
                InternalFormat: SizedFormat.Rgba8,
                PixelType: PixelType.Byte,
                Data: pixels);
        }
    }
}
