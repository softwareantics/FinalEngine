// <copyright file="CameraComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components;

using System.ComponentModel;
using System.Drawing;

[Category("Core")]
public class CameraComponent : IEntityComponent
{
    public bool IsEnabled { get; set; } = true;

    public bool IsLocked { get; set; }

    public Rectangle Viewport { get; set; }
}
