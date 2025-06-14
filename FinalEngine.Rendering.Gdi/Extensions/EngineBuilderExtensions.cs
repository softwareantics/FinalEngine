// <copyright file="EngineBuilderExtensions.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Extensions;

using FinalEngine.Hosting;
using FinalEngine.Rendering.Adapters.Drawing;
using FinalEngine.Rendering.Services;
using Microsoft.Extensions.DependencyInjection;

public static class EngineBuilderExtensions
{
    public static IEngineBuilder UseGdi(this IEngineBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IBufferedGraphicsContextAdapter, BufferedGraphicsContextAdapter>();
        builder.Services.AddSingleton<IRenderContext.RenderContextFactory>(x => (handle, size) => new RenderContext(handle, size));

        builder.Services.AddSingleton<IGraphicsProvider, GraphicsProvider>();
        builder.Services.AddSingleton<IRenderDevice, RenderDevice>();
        return builder;
    }
}
