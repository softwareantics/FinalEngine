// <copyright file="EngineOptions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Modules;

using FinalEngine.Utilities.Modules;
using Microsoft.Extensions.DependencyInjection;

internal sealed class EngineOptions : IEngineOptions
{
    public EngineOptions(IServiceCollection services)
    {
        this.Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public IServiceCollection Services { get; }
}
