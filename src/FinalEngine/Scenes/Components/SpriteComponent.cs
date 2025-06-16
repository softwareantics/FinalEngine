// <copyright file="SpriteComponent.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes.Components;

using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Textures;

public sealed class SpriteComponent : IEntityComponent
{
    public SpriteComponent()
    {
        this.Origin = Vector2.Zero;
        this.Color = Color.White;
    }

    public Color Color { get; set; }

    public Vector2 Origin { get; set; }

    public ITexture2D? Texture { get; set; }
}
