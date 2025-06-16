// <copyright file="EngineBuilderExtensions.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Extensions;

using FinalEngine.Hosting;
using Microsoft.Extensions.DependencyInjection;

public static class EngineBuilderExtensions
{
    public static IServiceCollection AddFinalEngine(this IServiceCollection services, Action<IEngineBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        var builder = new EngineBuilder(services);
        configure(builder);

        var configurator = new ServiceConfigurator();
        configurator.ConfigureServices(builder);

        return services;
    }
}
