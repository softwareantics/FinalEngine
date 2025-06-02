// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame;

using FinalEngine.Platform;
using FinalEngine.Platform.Desktop.Extensions;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///   The main game entry point.
/// </summary>
internal static class Program
{
    /// <summary>
    ///   Defines the entry point of the application.
    /// </summary>
    private static void Main()
    {
        var services = new ServiceCollection()
            .AddWindows();

        var window = services.BuildServiceProvider().GetRequiredService<IWindow>();

        while (!window.IsClosing)
        {
            Application.DoEvents();
        }

        window.Dispose();
    }
}
