// <copyright file="EngineBuilderExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Extensions;

using AutoMapper.Extensions.EnumMapping;
using FinalEngine.Hosting;
using FinalEngine.Rendering.Invocation;
using FinalEngine.Rendering.Profiles;
using Microsoft.Extensions.DependencyInjection;
using OpenTK;
using OpenTK.Windowing.GraphicsLibraryFramework;

public static class EngineBuilderExtensions
{
    public static IEngineBuilder UseOpenGL(this IEngineBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddAutoMapper(x =>
        {
            x.EnableEnumMappingValidation();
            x.AddProfile<OpenTKProfile>();
        });

        builder.Services.AddSingleton<IOpenGLInvoker, OpenGLInvoker>();
        builder.Services.AddSingleton<IBindingsContext, GLFWBindingsContext>();

        builder.Services.AddSingleton<IRenderContext, OpenGLRenderContext>();
        builder.Services.AddSingleton<IRenderPipeline, OpenGLRenderPipeline>();
        builder.Services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();

        builder.Services.AddSingleton(x => x.GetRequiredService<IRenderDevice>().Factory);
        builder.Services.AddSingleton(x => x.GetRequiredService<IRenderDevice>().InputAssembler);
        builder.Services.AddSingleton(x => x.GetRequiredService<IRenderDevice>().OutputMerger);
        builder.Services.AddSingleton(x => x.GetRequiredService<IRenderDevice>().Pipeline);
        builder.Services.AddSingleton(x => x.GetRequiredService<IRenderDevice>().Rasterizer);

        return builder;
    }
}
