// <copyright file="WinFormsServiceConfigurator.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Configurators;

using System.Diagnostics.CodeAnalysis;
using AutoMapper.Extensions.EnumMapping;
using FinalEngine.Hosting;
using FinalEngine.Platform.Adapters.Applications;
using FinalEngine.Platform.Adapters.Forms;
using FinalEngine.Platform.Adapters.Native;
using FinalEngine.Platform.Mappings.Profiles;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
internal sealed class WinFormsServiceConfigurator : IServiceConfigurator
{
    public void Configure(IServiceCollection services)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.SystemAware);

        services.AddAutoMapper(x =>
        {
            x.EnableEnumMappingValidation();
            x.AddProfile<WinFormsProfile>();
        });

        services.AddTransient<IFormAdapter, FormAdapter>();
        services.AddTransient<INativeAdapter, NativeAdapter>();
        services.AddTransient<IApplicationAdapter, ApplicationAdapter>();

        services.AddSingleton<IWindow, WinFormsWindow>();
        services.AddSingleton<IEventsProcessor, WinFormsEventsProcessor>();
    }
}