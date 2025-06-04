// <copyright file="Startup.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame;

using FinalEngine.Platform.Desktop.Extensions;
using FinalEngine.Runtime.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

        services.AddSingleton<IConfiguration>(this.Configuration);

        services.AddLogging(x =>
        {
            x.ClearProviders();
            x.AddConsole();
            x.AddDebug();
            x.AddEventSourceLogger();

            if (OperatingSystem.IsWindows())
            {
                x.AddEventLog();
            }

            x.AddConfiguration(this.Configuration.GetSection("Logging"));
        });

        services.AddWindows();
        services.AddRuntime();
    }
}
