// <copyright file="SoundResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.Loaders;

using System.IO.Abstractions;
using FinalEngine.Audio;
using FinalEngine.Audio.Sounds;
using FinalEngine.Decoding.Audio;
using FinalEngine.Resources;

internal sealed class SoundResourceLoader : ResourceLoaderBase<ISound>
{
    private readonly IFileSystem fileSystem;

    private readonly IAudioResourceFactory resourceFactory;

    private readonly ISoundDecoder soundDecoder;

    public SoundResourceLoader(IFileSystem fileSystem, ISoundDecoder soundDecoder, IAudioResourceFactory resourceFactory)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.soundDecoder = soundDecoder ?? throw new ArgumentNullException(nameof(soundDecoder));
        this.resourceFactory = resourceFactory ?? throw new ArgumentNullException(nameof(resourceFactory));
    }

    public override ISound LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified file does not exist: {filePath}", filePath);
        }

        var decoded = this.soundDecoder.Decode(filePath);
        var description = new SoundDescription(decoded.Format);

        return this.resourceFactory.CreateSound(description, decoded.Data);
    }
}
