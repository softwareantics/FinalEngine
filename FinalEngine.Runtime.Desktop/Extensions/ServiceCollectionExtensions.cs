// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Desktop.Extensions;

using System.Diagnostics.CodeAnalysis;
using AutoMapper.Extensions.EnumMapping;
using FinalEngine.Platform.Desktop.Extensions;
using FinalEngine.Runtime.Desktop.Mappings.Profiles;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///   Provides extension methods for configuring an <see cref="IServiceCollection"/> with Windows runtime services.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Extension methods are not covered by unit tests.")]
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///   Adds the Windows runtime services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">
    ///   The services collection to which the Windows runtime services will be added.
    /// </param>
    /// <returns>
    ///   The <see cref="IServiceCollection"/> with the Windows runtime services added.
    /// </returns>
    /// <exception cref="System.ArgumentNullException">
    ///   Thrown when the <paramref name="services"/> parameter is null.
    /// </exception>
    public static IServiceCollection AddWindowsRuntime(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddAutoMapper(x =>
        {
            x.EnableEnumMappingValidation();
            x.AddProfile<WinFormsProfile>();
        });

        services.AddWindows();

        return services;
    }
}
