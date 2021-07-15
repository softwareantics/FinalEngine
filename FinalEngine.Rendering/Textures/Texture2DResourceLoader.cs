// <copyright file="Texture2DLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures
{
    using System;
    using System.IO;
    using FinalEngine.IO;
    using FinalEngine.Rendering.Invocation;
    using FinalEngine.Resources;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="ITexture2DLoader"/>.
    /// </summary>
    /// <seealso cref="FinalEngine.Rendering.Textures.ITexture2DLoader"/>
    public class Texture2DResourceLoader : ResourceLoaderBase<ITexture2D>
    {
        /// <summary>
        ///   The GPU resource factory.
        /// </summary>
        private readonly IGPUResourceFactory factory;

        /// <summary>
        ///   The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        ///   The image invoker.
        /// </summary>
        private readonly IImageInvoker invoker;

        /// <summary>
        ///   Initializes a new instance of the <see cref="Texture2DResourceLoader"/> class.
        /// </summary>
        /// <param name="fileSystem">
        ///   The file system used to open textures to load.
        /// </param>
        /// <param name="factory">
        ///   The GPU resource factory used to create a texture once it's been loaded.
        /// </param>
        /// <param name="invoker">
        ///   The image invoker used to handle <see cref="Image"/> manipulation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="fileSystem"/>, <paramref name="factory"/> or <paramref name="invoker"/> parameter is null.
        /// </exception>
        public Texture2DResourceLoader(IFileSystem fileSystem, IGPUResourceFactory factory, IImageInvoker invoker)
        {
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem), $"The specified {nameof(fileSystem)} parameter cannot be null.");
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory), $"The specified {nameof(factory)} parameter cannot be null.");
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker), $"The specified {nameof(invoker)} parameter cannot be null.");
        }

        /// <summary>
        ///   Loads the texture from the specified <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">
        ///   The file path of the texture to load.
        /// </param>
        /// <returns>
        ///   The newly loaded texture resource.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="filePath"/> parameter is null.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///   The specified <paramref name="filePath"/> parameter cannot be located by the file system.
        /// </exception>
        public override ITexture2D LoadResource(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), $"The specified {nameof(filePath)} parameter cannot be null, empty or consist of only whitespace characters.");
            }

            if (!this.fileSystem.FileExists(filePath))
            {
                throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
            }

            Stream stream = this.fileSystem.OpenFile(filePath, FileAccessMode.Read);

            using (Image<Rgba32> image = this.invoker.Load<Rgba32>(stream))
            {
                int width = image.Width;
                int height = image.Height;

                int[] data = new int[width * height];

                // Just simple bit manipulation to convert RGBA to ABGR.
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Rgba32 color = image[x, y];
                        data[(y * width) + x] = (color.A << 24) | (color.B << 16) | (color.G << 8) | (color.R << 0);
                    }
                }

                return this.factory.CreateTexture2D(
                    new Texture2DDescription()
                    {
                        Width = width,
                        Height = height,

                        MinFilter = TextureFilterMode.Nearest,
                        MagFilter = TextureFilterMode.Linear,

                        WrapS = TextureWrapMode.Repeat,
                        WrapT = TextureWrapMode.Repeat,

                        PixelType = PixelType.Byte,
                    },
                    data);
            }
        }
    }
}