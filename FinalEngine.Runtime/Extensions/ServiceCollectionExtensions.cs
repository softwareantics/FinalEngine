// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

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
    /// <returns>
    /// Returns the updated <see cref="IServiceCollection"/> with the runtime services added.
    /// </returns>
    ///
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="services"/> parameter is null.
    /// </exception>
    public static IServiceCollection AddRuntime(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IEngineDriver, EngineDriver>();

        return services;
    }
}
