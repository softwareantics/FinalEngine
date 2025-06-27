// <copyright file="IAudioResourceFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio;

using FinalEngine.Audio.Sounds;

public interface IAudioResourceFactory
{
    ISound CreateSound(SoundDescription description, IReadOnlyCollection<byte> data);
}
