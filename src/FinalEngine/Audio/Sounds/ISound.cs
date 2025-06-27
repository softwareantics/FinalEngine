// <copyright file="ISound.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.Sounds;

using FinalEngine.Resources;

public interface ISound : IResource
{
    bool IsLooping { get; set; }

    float Volume { get; set; }

    void Pause();

    void Play();

    void Reset();
}
