// <copyright file="Startup.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame;

using FinalEngine.Runtime.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal sealed class Startup
{
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddRuntime(this.Configuration);
    }
}
