// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Physics.Extensions;

using FinalEngine.Physics.Systems;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPhysics(this IServiceCollection services)
    {
        services.AddSingleton<CameraUpdateEntitySystem>();
        services.AddSingleton<SpinUpdateEntitySystem>();

        return services;
    }
}
