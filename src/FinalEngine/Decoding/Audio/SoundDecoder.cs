// <copyright file="SoundDecoder.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Decoding.Audio;

using System.IO.Abstractions;

internal sealed class SoundDecoder : ISoundDecoder
{
    private readonly IFileSystem fileSystem;

    public SoundDecoder(IFileSystem fileSystem)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
    }

    public DecodedSound Decode(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified file does not exist: {filePath}", filePath);
        }

        string extension = this.fileSystem.Path.GetExtension(filePath).ToUpperInvariant();

        using (var stream = this.fileSystem.File.OpenRead(filePath))
        {
            if (stream.Length == 0)
            {
                throw new InvalidOperationException($"The specified file is empty: {filePath}");
            }

            byte[] buffer = new byte[stream.Length];
            stream.ReadExactly(buffer, 0, buffer.Length);

            return new DecodedSound(GetAudioFormat(extension), buffer);
        }
    }

    private static AudioFormat GetAudioFormat(string extension)
    {
        return extension switch
        {
            ".OGG" => AudioFormat.Ogg,
            ".MP3" => AudioFormat.Mp3,
            _ => throw new NotSupportedException($"The audio format '{extension}' is not supported."),
        };
    }
}
