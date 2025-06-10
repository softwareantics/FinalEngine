// <copyright file="EngineBuilder.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting;

using Microsoft.Extensions.DependencyInjection;

internal sealed class EngineBuilder : IEngineBuilder
{
    public EngineBuilder(IServiceCollection services)
    {
        this.Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public IServiceCollection Services { get; }
}
