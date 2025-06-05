// <copyright file="Program.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame;

using FinalEngine.Runtime;
using FinalEngine.Runtime.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///   The main game entry point.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Defines the entry point of the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .BuildRuntimeConfiguration();

        var startup = new Startup(configuration);
        var services = new ServiceCollection();

        startup.ConfigureServices(services);

        var provider = services.BuildServiceProvider(true);

        using (var driver = provider.GetRequiredService<IEngineDriver>())
        {
            driver.Start();
        }
    }
}
