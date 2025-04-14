// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Extensions;

using System;
using System.IO.Abstractions;
using FinalEngine.ECS.Extensions;
using FinalEngine.Input.Extensions;
using FinalEngine.Physics.Extensions;
using FinalEngine.Rendering.Extensions;
using FinalEngine.Resources.Extensions;
using FinalEngine.Utilities;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreRuntime<TGame>(this IServiceCollection services)
        where TGame : GameContainerBase
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton<IEngineDriver, EngineDriver>();
        services.AddSingleton<GameContainerBase, TGame>();

        services.AddSingleton<IGameTime>(x =>
        {
            return new GameTime(120.0d);
        });

        services.AddECS();
        services.AddInput();
        services.AddRendering();
        services.AddResourceManager();
        services.AddPhysics();

        return services;
    }
}
