// <copyright file="ConfigurationBuilderExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Provides extension methods for the <see cref="IConfigurationBuilder"/> interface to build runtime configurations.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ConfigurationBuilderExtensions
{
    /// <summary>
    /// Builds the runtime configuration for the application using the specified <see cref="IConfigurationBuilder"/>.
    /// </summary>
    ///
    /// <param name="builder">
    /// Specifies an <see cref="IConfigurationBuilder"/> that is used to build the runtime configuration.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="builder"/> parameter is <c>null</c>.
    /// </exception>
    ///
    /// <returns>
    /// Returns the built <see cref="IConfiguration"/> instance that contains the configuration settings for the application.
    /// </returns>
    public static IConfiguration BuildRuntimeConfiguration(this IConfigurationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables(prefix: "DOTNET_")
            .Build();
    }
}
