// <copyright file="SpriteComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame.Components
{
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.ECS;
    using FinalEngine.Rendering.Textures;

    public sealed class SpriteComponent : IComponent
    {
        public Color Color { get; set; } = Color.White;

        public Vector2 Origin { get; set; } = new Vector2(128, 128);

        public ITexture2D Texture { get; set; }
    }
}