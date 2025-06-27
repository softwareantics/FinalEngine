// <copyright file="SoundDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.Sounds;

using FinalEngine.Decoding.Audio;

public readonly record struct SoundDescription(
    AudioFormat Format);
