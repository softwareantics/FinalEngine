// <copyright file="EngineBuilderExtensions.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Extensions;

using System.Diagnostics.CodeAnalysis;
using AutoMapper.Extensions.EnumMapping;
using FinalEngine.Hosting;
using FinalEngine.Platform.Adapters.Applications;
using FinalEngine.Platform.Adapters.Forms;
using FinalEngine.Platform.Adapters.Native;
using FinalEngine.Platform.Mappings.Profiles;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class EngineBuilderExtensions
{
    public static IEngineBuilder UseWindows(this IEngineBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.SystemAware);

        builder.Services.AddAutoMapper(x =>
        {
            x.EnableEnumMappingValidation();
            x.AddProfile<WinFormsProfile>();
        });

        builder.Services.AddTransient<IFormAdapter, FormAdapter>();
        builder.Services.AddTransient<INativeAdapter, NativeAdapter>();
        builder.Services.AddTransient<IApplicationAdapter, ApplicationAdapter>();

        builder.Services.AddSingleton<IWindow, WinFormsWindow>();
        builder.Services.AddSingleton<IEventsProcessor, WinFormsEventsProcessor>();

        return builder;
    }
}
