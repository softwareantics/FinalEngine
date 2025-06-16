// <copyright file="AudioFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.Factories;

using System;
using System.Diagnostics.CodeAnalysis;
using CASL;

[ExcludeFromCodeCoverage]
internal sealed class CaslAudioFactory : ICaslAudioFactory
{
    public IAudio CreateSound(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        return new Audio(filePath, BufferType.Full);
    }
}
