// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting.Extensions;

using System.Reflection;
using FinalEngine.Hosting.Services;
using FinalEngine.Hosting.Services.Activation;
using FinalEngine.Hosting.Services.Discovery;
using FinalEngine.Hosting.Services.Loading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

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

        services.AddLogging(ConfigureLogging(configuration));

        services.AddGameServices(configuration);
        services.AddSingleton<GameContainerBase, TGameContainer>();

        return services;
    }

    private static void AddGameServices(this IServiceCollection services, IConfiguration configuration)
    {
        using (var factory = LoggerFactory.Create(ConfigureLogging(configuration)))
        {
            var assemblyLoader = new AssembyLoader(factory.CreateLogger<AssembyLoader>());
            var typeLocator = new TypeLocator(factory.CreateLogger<TypeLocator>());
            var activator = new ConfiguratorActivator(factory.CreateLogger<ConfiguratorActivator>());

            foreach (var assembly in assemblyLoader.LoadAssemblies())
            {
                foreach (var type in typeLocator.GetSupportedTypes(assembly))
                {
                    activator.ActivateAndConfigure(type, services);
                }
            }
        }
    }

    private static Action<ILoggingBuilder> ConfigureLogging(IConfiguration configuration)
    {
        return x =>
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
        };
    }
}
