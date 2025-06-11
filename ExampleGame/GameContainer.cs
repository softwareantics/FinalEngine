// <copyright file="GameContainer.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace ExampleGame;

using FinalEngine;

public sealed class GameContainer : GameContainerBase
{
    public GameContainer()
    {
        this.Window.Title = "Hello, World!";
    }
}
