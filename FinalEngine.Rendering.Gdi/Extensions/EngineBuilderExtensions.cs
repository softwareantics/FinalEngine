// <copyright file="EngineBuilderExtensions.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Extensions;

using System.Diagnostics.CodeAnalysis;
using FinalEngine.Hosting;
using FinalEngine.Rendering.Adapters.Drawing;
using FinalEngine.Rendering.Services;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class EngineBuilderExtensions
{
    public static IEngineBuilder UseGdi(this IEngineBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IBufferedGraphicsContextAdapter, BufferedGraphicsContextAdapter>();

        builder.Services.AddSingleton<IRenderContext.RenderContextFactory>(x => (handle, size) => new GdiRenderContext(handle, size));
        builder.Services.AddSingleton<IBitmapAdapter.BitmapAdapterFactory>(x => (width, height, format) => new BitmapAdapter(width, height, format));

        builder.Services.AddSingleton<IGraphicsProvider, GraphicsProvider>();
        builder.Services.AddSingleton<IRenderDevice, GdiRenderDevice>();
        builder.Services.AddSingleton<IRenderResourceFactory, GdiRenderResourceFactory>();

        return builder;
    }
}
