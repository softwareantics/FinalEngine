// <copyright file="EngineBuilderExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Extensions;

using FinalEngine.Hosting;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Platform.Adapters;
using Microsoft.Extensions.DependencyInjection;
using OpenTK.Windowing.Common;

public static class EngineBuilderExtensions
{
    public static IEngineBuilder UseOpenTK(this IEngineBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<INativeWindowAdapter, NativeWindowAdapter>();

        builder.Services.AddSingleton<IGraphicsContext>(x => x.GetRequiredService<INativeWindowAdapter>().Context);

        builder.Services.AddSingleton<IKeyboardDevice, OpenTKKeyboardDevice>();
        builder.Services.AddSingleton<IMouseDevice, OpenTKMouseDevice>();
        builder.Services.AddSingleton<IWindow, OpenTKWindow>();
        builder.Services.AddSingleton<IEventsProcessor>(x => (IEventsProcessor)x.GetRequiredService<IWindow>());

        return builder;
    }
}
