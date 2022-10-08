// <copyright file="ShaderResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions.Resources.Loaders
{
    using System;
    using System.IO;
    using FinalEngine.IO;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Resources;

    public class ShaderResourceLoader : ResourceLoaderBase<IShader>
    {
        private readonly IGPUResourceFactory factory;

        private readonly IFileSystem fileSystem;

        public ShaderResourceLoader(IGPUResourceFactory factory, IFileSystem fileSystem)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public override IShader LoadResource(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!this.fileSystem.FileExists(filePath))
            {
                throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
            }

            var target = GetPipelineTarget(filePath);

            using (var stream = this.fileSystem.OpenFile(filePath, FileAccessMode.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    return this.factory.CreateShader(target, reader.ReadToEnd());
                }
            }
        }

        private static PipelineTarget GetPipelineTarget(string filePath)
        {
            string? extension = Path.GetExtension(filePath);

            return extension switch
            {
                ".vert" => PipelineTarget.Vertex,
                ".frag" => PipelineTarget.Fragment,
                _ => throw new NotSupportedException($"The file extension specified is not supported: '{extension}'"),
            };
        }
    }
}