// <copyright file="ServiceConfigurator.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting;

using System.Diagnostics.CodeAnalysis;
using FinalEngine.Runtime;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
internal sealed class ServiceConfigurator : IServiceConfigurator
{
    public void Configure(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddSingleton<IEngineDriver, EngineDriver>();
    }
}