// <copyright file="ServiceConfigurator.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace ExampleGame;

using ExampleGame.Services;
using FinalEngine.Hosting;
using Microsoft.Extensions.DependencyInjection;

public sealed class ServiceConfigurator : IServiceConfigurator
{
    public void Configure(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddSingleton<ITestService, TestService>();
    }
}
