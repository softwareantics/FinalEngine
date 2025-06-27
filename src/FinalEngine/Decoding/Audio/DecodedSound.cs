// <copyright file="DecodedSound.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Decoding.Audio;

public readonly record struct DecodedSound(
    AudioFormat Format,
    IReadOnlyCollection<byte> Data);
