// <copyright file="TagComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes.Components;

using System.ComponentModel;
using FinalEngine.Scenes;

[Category("Core")]
public sealed class TagComponent : IEntityComponent
{
    public TagComponent(string name = "Entity")
    {
        this.Name = name;
    }

    public string Name { get; }
}
