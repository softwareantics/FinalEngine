// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFinalEngine(this IServiceCollection services, Action<IEngineBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var configuration = new ConfigurationBuilder()
            .BuildRuntimeConfiguration();

        var builder = new EngineBuilder(services);
        configure?.Invoke(builder);

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
