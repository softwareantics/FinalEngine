// <copyright file="ISoundDecoder.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Decoding.Audio;

public interface ISoundDecoder
{
    DecodedSound Decode(string filePath);
}
