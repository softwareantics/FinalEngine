// <copyright file="ShaderProgramResourceLoader.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

using FinalEngine.Resources;

namespace FinalEngine.Rendering.Loaders;

using System.IO.Abstractions;
using System.Text.Json;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Pipeline;

internal sealed class ShaderProgramResourceLoader : ResourceLoaderBase<IShaderProgram>
{
    private readonly IFileSystem fileSystem;

    private readonly IRenderDevice renderDevice;

    private readonly ResourceLoaderBase<IShader> shaderResourceLoader;

    public ShaderProgramResourceLoader(ResourceLoaderBase<IShader> shaderResourceLoader, IRenderDevice renderDevice, IFileSystem fileSystem)
    {
        this.shaderResourceLoader = shaderResourceLoader ?? throw new ArgumentNullException(nameof(shaderResourceLoader));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public override IShaderProgram LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} couldn't be located.", filePath);
        }

        string content = this.fileSystem.File.ReadAllText(filePath);
        var data = JsonSerializer.Deserialize<ProgramData>(content)
            ?? throw new ArgumentException($"The specified {nameof(filePath)} couldn't be converted into an {nameof(IShaderProgram)}, please check your formatting.", nameof(filePath));

        string directory = this.fileSystem.FileInfo.New(filePath).Directory?.FullName ?? string.Empty;

        var vertexShader = this.shaderResourceLoader.LoadResource($"{directory}\\{data.Vertex}");
        var fragmentShader = this.shaderResourceLoader.LoadResource($"{directory}\\{data.Fragment}");

        var shaderProgram = this.renderDevice.Factory.CreateShaderProgram([vertexShader, fragmentShader]);

        vertexShader.Dispose();
        fragmentShader.Dispose();

        return shaderProgram;
    }

    private sealed class ProgramData
    {
        public string Fragment { get; set; } = null!;

        public string Vertex { get; set; } = null!;
    }
}
