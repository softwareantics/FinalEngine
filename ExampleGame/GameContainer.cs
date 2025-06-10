// <copyright file="GameContainer.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace ExampleGame;

using ExampleGame.Services;
using FinalEngine;

public sealed class GameContainer : GameContainerBase
{
    public GameContainer(ITestService testService)
    {
        ArgumentNullException.ThrowIfNull(testService);
        testService.DoSomething();

        this.Window.Title = "Hello, World!";
    }
}
