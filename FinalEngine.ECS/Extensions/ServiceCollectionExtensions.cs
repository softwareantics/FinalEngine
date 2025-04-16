// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Extensions;

using System;
using FinalEngine.ECS.Resolving;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddECS(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddTransient<IEntityWorld, EntityWorld>();
        services.AddSingleton<IEntitySystemResolver, EntitySystemResolver>();

        return services;
    }
}
