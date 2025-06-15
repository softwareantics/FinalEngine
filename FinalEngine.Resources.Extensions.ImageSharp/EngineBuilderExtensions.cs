// <copyright file="EngineBuilderExtensions.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources.Extensions;

using System.Diagnostics.CodeAnalysis;
using FinalEngine.Hosting;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources.Extensions.Adapters;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class EngineBuilderExtensions
{
    public static IEngineBuilder UseImageSharp(this IEngineBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IImageAdapter, ImageAdapter>();
        builder.Services.AddSingleton<ResourceLoaderBase<ITexture2D>, Texture2DResourceLoader>();

        return builder;
    }
}
