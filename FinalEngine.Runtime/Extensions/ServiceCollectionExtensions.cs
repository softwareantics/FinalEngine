// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides extension methods for configuring the <see cref="IServiceCollection"/> to include runtime services.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the engine runtime services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    ///
    /// <param name="services">
    /// Specifies an <see cref="IServiceCollection"/> that represents the service collection to which the runtime services will be added.
    /// </param>
    ///
    /// <param name="configuration">
    /// Specifies an <see cref="IConfiguration"/> that contains the configuration settings for the runtime services, such as logging settings.
    /// </param>
    ///
    /// <returns>
    /// Returns the updated <see cref="IServiceCollection"/> with the runtime services added.
    /// </returns>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="services"/> parameter is null.
    /// </exception>
    public static IServiceCollection AddRuntime(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSingleton<IConfiguration>(configuration);

        services.AddLogging(x =>
        {
            x.ClearProviders();
            x.AddConsole();
            x.AddDebug();
            x.AddEventSourceLogger();

            if (OperatingSystem.IsWindows())
            {
                x.AddEventLog();
            }

            x.AddConfiguration(configuration.GetSection("Logging"));
        });

        services.AddSingleton<IEngineDriver, EngineDriver>();

        return services;
    }
}
