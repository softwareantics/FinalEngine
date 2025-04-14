// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Extensions;

using System;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Common.Services.Environment;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.Common.Services.Factories.Entities.Cameras;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.Common.Systems;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage(Justification = "Extensions")]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<IApplicationContext, ApplicationContext>();
        services.AddSingleton<IEnvironmentContext, EnvironmentContext>();
        services.AddSingleton<ISceneManager, SceneManager>();

        services.AddSingleton<ViewportUpdateEntitySystem>();
        services.AddSingleton<EditorCameraEntityFactory>();

        return services;
    }

    public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddTransient<TService, TImplementation>();
        services.AddSingleton<Func<TService>>(x =>
        {
            return () =>
            {
                return x.GetRequiredService<TService>();
            };
        });

        services.AddSingleton<IFactory<TService>, Factory<TService>>();
    }
}
