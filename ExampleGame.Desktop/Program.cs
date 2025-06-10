// <copyright file="Program.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace ExampleGame.Desktop;

using FinalEngine.Hosting.Extensions;
using FinalEngine.Runtime;
using Microsoft.Extensions.DependencyInjection;

internal static class Program
{
    private static void Main()
    {
        var provider = new ServiceCollection()
            .AddFinalEngine<GameContainer>()
            .BuildServiceProvider();

        using (var driver = provider.GetRequiredService<IEngineDriver>())
        {
            driver.Start();
        }
    }
}
