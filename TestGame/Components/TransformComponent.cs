// <copyright file="TransformComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame.Components
{
    using System.Numerics;
    using FinalEngine.ECS;

    public sealed class TransformComponent : IComponent
    {
        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        public Vector2 Scale { get; set; } = new Vector2(256, 256);
    }
}