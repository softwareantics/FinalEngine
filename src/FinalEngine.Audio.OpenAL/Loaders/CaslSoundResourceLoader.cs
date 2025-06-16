// <copyright file="SoundResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.Loaders;

using System;
using System.IO;
using System.IO.Abstractions;
using FinalEngine.Audio.Factories;
using FinalEngine.Resources;

internal sealed class CaslSoundResourceLoader : ResourceLoaderBase<ISound>
{
    private readonly ICaslAudioFactory factory;

    private readonly IFileSystem fileSystem;

    public CaslSoundResourceLoader(IFileSystem fileSystem, ICaslAudioFactory factory)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    protected override ISound LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
        }

        return new CaslSound(this.factory.CreateSound(filePath));
    }
}
