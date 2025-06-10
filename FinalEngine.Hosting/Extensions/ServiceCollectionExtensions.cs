// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting.Extensions;

using FinalEngine.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFinalEngine<TGameContainer>(this IServiceCollection services, Action<IEngineBuilder>? configure = null)
        where TGameContainer : GameContainerBase
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

        services.AddGameServices();

        services.AddSingleton<IEngineDriver, EngineDriver>();
        services.AddSingleton<GameContainerBase, TGameContainer>();

        return services;
    }

    private static IServiceCollection AddGameServices(this IServiceCollection services)
    {
        var container = new ServiceCollection();

        container.Scan(scanner =>
        {
            scanner
                .FromApplicationDependencies()
                .AddClasses(x => x.AssignableTo<IServiceConfigurator>())
                .As<IServiceConfigurator>()
                .WithSingletonLifetime();
        });

        var provider = container.BuildServiceProvider();

        foreach (var configurator in provider.GetServices<IServiceConfigurator>())
        {
            configurator.Configure(services);
        }

        return services;
    }
}
