// <copyright file="ShaderResourceLoader.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources.Loaders;

using System.IO.Abstractions;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Pipeline;

internal sealed class ShaderResourceLoader : ResourceLoaderBase<IShader>
{
    private readonly IFileSystem fileSystem;

    private readonly IRenderDevice renderDevice;

    public ShaderResourceLoader(IFileSystem fileSystem, IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public override IShader LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} couldn't be located.", filePath);
        }

        var target = GetPipelineTarget(this.fileSystem.Path.GetExtension(filePath));

        using (var stream = this.fileSystem.File.OpenRead(filePath))
        {
            using (var reader = new StreamReader(stream))
            {
                return this.renderDevice.Factory.CreateShader(target, reader.ReadToEnd());
            }
        }
    }

    private static PipelineTarget GetPipelineTarget(string? extension)
    {
        return extension switch
        {
            ".vs" or ".vert" => PipelineTarget.Vertex,
            ".fs" or ".frag" => PipelineTarget.Fragment,
            _ => throw new NotSupportedException($"The specified {nameof(extension)} is not supported by the {nameof(ShaderResourceLoader)}."),
        };
    }
}
