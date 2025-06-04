// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Provides extension methods for configuring services in <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configures options with validation to the <see cref="IServiceCollection"/>.
    /// </summary>
    ///
    /// <typeparam name="TOptions">
    /// The type of options to be configured.
    /// </typeparam>
    ///
    /// <param name="services">
    /// Specifies an <see cref="IServiceCollection"/> that represents the service collection to which the options will be added.
    /// </param>
    ///
    /// <param name="configuration">
    /// Specifies an <see cref="IConfiguration"/> that contains the configuration settings for the options.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown when one of the following parameters is null:
    /// <list type="bullet">
    ///     <item>
    ///         <paramref name="services"/>
    ///     </item>
    ///     <item>
    ///         <paramref name="configuration"/>
    ///     </item>
    /// </list>
    /// </exception>
    ///
    /// <returns>
    ///   Returns an <see cref="OptionsBuilder{TOptions}"/> instance for further configuration.
    /// </returns>
    public static OptionsBuilder<TOptions> AddValidatedOptions<TOptions>(this IServiceCollection services, IConfiguration configuration)
            where TOptions : class
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        return services.AddOptions<TOptions>()
            .Bind(configuration.GetSection(typeof(TOptions).Name))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
