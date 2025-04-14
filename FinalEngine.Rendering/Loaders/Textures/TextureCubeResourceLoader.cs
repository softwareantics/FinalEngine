// <copyright file="TextureCubeResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Loaders.Textures;

using System;
using System.IO;
using System.IO.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

internal sealed class TextureCubeResourceLoader : ResourceLoaderBase<ITextureCube>
{
    private readonly IFileSystem fileSystem;

    private readonly IRenderDevice renderDevice;

    private readonly ResourceLoaderBase<ITexture2D> resourceLoader;

    public TextureCubeResourceLoader(IFileSystem fileSystem, IRenderDevice renderDevice, ResourceLoaderBase<ITexture2D> resourceLoader)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.resourceLoader = resourceLoader ?? throw new ArgumentNullException(nameof(resourceLoader));
    }

    public override ITextureCube LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
        }

        string content = this.fileSystem.File.ReadAllText(filePath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var skyboxFile = JsonSerializer.Deserialize<SkyboxFile>(content)
            ?? throw new InvalidOperationException($"Failed to parse {nameof(SkyboxFile)} at path: '{filePath}'");

        string path = new FileInfo(filePath).Directory!.FullName;

        var top = this.resourceLoader.LoadResource(Path.Combine(path, skyboxFile.Top));
        var bottom = this.resourceLoader.LoadResource(Path.Combine(path, skyboxFile.Bottom));
        var front = this.resourceLoader.LoadResource(Path.Combine(path, skyboxFile.Front));
        var back = this.resourceLoader.LoadResource(Path.Combine(path, skyboxFile.Back));
        var left = this.resourceLoader.LoadResource(Path.Combine(path, skyboxFile.Left));
        var right = this.resourceLoader.LoadResource(Path.Combine(path, skyboxFile.Right));

        var texture = this.renderDevice.Factory.CreateCubeTexture(
            new TextureCubeDescription()
            {
                Width = right.Description.Width,
                Height = right.Description.Height,
                WrapR = TextureWrapMode.Clamp,
                WrapS = TextureWrapMode.Clamp,
                WrapT = TextureWrapMode.Clamp,
            },
            right,
            left,
            top,
            bottom,
            back,
            front);

        top.Dispose();
        bottom.Dispose();
        front.Dispose();
        back.Dispose();
        left.Dispose();
        right.Dispose();

        return texture;
    }

    private sealed class SkyboxFile
    {
        [JsonRequired]
        public string Back { get; set; } = null!;

        [JsonRequired]
        public string Bottom { get; set; } = null!;

        [JsonRequired]
        public string Front { get; set; } = null!;

        [JsonRequired]
        public string Left { get; set; } = null!;

        [JsonRequired]
        public string Right { get; set; } = null!;

        [JsonRequired]
        public string Top { get; set; } = null!;
    }
}
