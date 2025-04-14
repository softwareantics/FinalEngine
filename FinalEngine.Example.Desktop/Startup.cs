// <copyright file="Startup.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Example.Desktop;

using System;
using FinalEngine.Runtime.Extensions;
using Microsoft.Extensions.DependencyInjection;

internal sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        services.AddRuntime<Game>();
    }
}
