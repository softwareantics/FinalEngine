// <copyright file="IAudioFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.Factories;

using CASL;

internal interface ICaslAudioFactory
{
    IAudio CreateSound(string filePath);
}
