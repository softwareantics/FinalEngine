// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Extensions;

using System.Diagnostics.CodeAnalysis;
using AutoMapper.Extensions.EnumMapping;
using FinalEngine.Platform.Desktop.Invocation;
using FinalEngine.Platform.Desktop.Invocation.Native;
using FinalEngine.Platform.Desktop.Mappings.Profiles;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///   Provides extension methods for configuring an <see cref="IServiceCollection"/> with Windows Forms platform services.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///   Adds the Windows Forms platform services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">
    ///   The services collection to which the Windows Forms platform services will be added.
    /// </param>
    /// <returns>
    ///   The <see cref="IServiceCollection"/> with the Windows Forms platform services added.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///   Thrown when the <paramref name="services"/> parameter is null.
    /// </exception>
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.SystemAware);

        services.AddAutoMapper(x =>
        {
            x.EnableEnumMappingValidation();
            x.AddProfile<WinFormsProfile>();
        });

        services.AddTransient<IFormAdapter, FormAdapter>();
        services.AddTransient<IPInvokeAdapter, PInvokeAdapter>();

        services.AddSingleton<IWindow, WinFormsWindow>();
        services.AddSingleton<IEventsProcessor, WinFormsEventsProcessor>();

        return services;
    }
}
