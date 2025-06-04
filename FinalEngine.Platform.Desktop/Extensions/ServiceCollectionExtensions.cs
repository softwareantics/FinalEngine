// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Extensions;

using System.Diagnostics.CodeAnalysis;
using AutoMapper.Extensions.EnumMapping;
using FinalEngine.Platform.Desktop.Invocation.Applications;
using FinalEngine.Platform.Desktop.Invocation.Forms;
using FinalEngine.Platform.Desktop.Invocation.Native;
using FinalEngine.Platform.Desktop.Mappings.Profiles;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for configuring an <see cref="IServiceCollection"/> with Windows Forms platform services.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Windows Forms platform services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    ///
    /// <param name="services">
    /// Specifies an <see cref="IServiceCollection"/> that represents the service collection to which the Windows Forms platform services will be added.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="services"/> parameter is null.
    /// </exception>
    ///
    /// <returns>
    /// Returns the <see cref="IServiceCollection"/> with the Windows Forms platform services added.
    /// </returns>
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        //// TODO: Add support to configure this via module support.
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

        return services;
    }
}
