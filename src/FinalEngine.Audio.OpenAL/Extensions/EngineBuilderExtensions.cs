// <copyright file="EngineBuilderExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.Extensions;

using FinalEngine.Audio.Factories;
using FinalEngine.Audio.Loaders;
using FinalEngine.Hosting;
using FinalEngine.Resources;
using Microsoft.Extensions.DependencyInjection;

public static class EngineBuilderExtensions
{
    public static IEngineBuilder UseCasl(this IEngineBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<ICaslAudioFactory, CaslAudioFactory>();
        builder.Services.AddSingleton<ResourceLoaderBase<ISound>, CaslSoundResourceLoader>();

        return builder;
    }
}
