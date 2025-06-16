// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting.Extensions;

using System.IO.Abstractions;
using FinalEngine.Adapters;
using FinalEngine.ECS;
using FinalEngine.ECS.Resolving;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Rendering.Batching;
using FinalEngine.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFinalEngine(this IServiceCollection services, Action<IEngineBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        var configuration = new ConfigurationBuilder()
            .BuildRuntimeConfiguration();

        var builder = new EngineBuilder(services);
        configure(builder);

        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<IFileSystem, FileSystem>();

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

        services.AddTransient<IStopwatchAdapter, StopwatchAdapter>();

        services.AddTransient<IEntityWorld, EntityWorld>();
        services.AddSingleton<IEntitySystemResolver, EntitySystemResolver>();

        services.AddSingleton<IKeyboard, Keyboard>();
        services.AddSingleton<IMouse, Mouse>();

        services.AddSingleton<ISpriteBatcher, SpriteBatcher>();
        services.AddSingleton<ITextureBinder, TextureBinder>();
        services.AddSingleton<ISpriteDrawer, SpriteDrawer>();

        services.AddTransient<IResourceManager, ResourceManager>();

        services.AddSingleton<IGameTime, GameTime>();

        services.AddSingleton<IEngineDriver, EngineDriver>();

        return services;
    }
}
