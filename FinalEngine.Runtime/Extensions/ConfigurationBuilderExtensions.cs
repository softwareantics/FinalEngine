// <copyright file="ConfigurationBuilderExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Extensions;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

[ExcludeFromCodeCoverage]
public static class ConfigurationBuilderExtensions
{
    public static IConfiguration BuildRuntimeConfiguration(this IConfigurationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables(prefix: "DOTNET_")
            .Build();
    }
}
