// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame;

using FinalEngine.Platform;
using FinalEngine.Platform.Desktop.Extensions;
using FinalEngine.Runtime;
using FinalEngine.Runtime.Extensions;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///   The main game entry point.
/// </summary>
internal static class Program
{
    /// <summary>
    ///   Defines the entry point of the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        var services = new ServiceCollection()
            .AddWindows()
            .AddRuntime();

        var provider = services.BuildServiceProvider();
        var driver = provider.GetRequiredService<IEngineDriver>();

        driver.Start();
    }
}
