// <copyright file="SpaceShipUpdateEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Systems;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;
using FinalEngine.Maths;
using FinalEngine.Rendering.Components;

[Category("Physics")]
public class ShipComponent : IEntityComponent
{
    public float Amount { get; set; }
}

[EntitySystemProcess(EventName = "Update")]
public sealed class SpaceShipUpdateEntitySystem : EntitySystemBase
{
    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() && entity.ContainsComponent<ShipComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var rotate = entity.GetComponent<ShipComponent>();

            float waveRotation = (float)Math.Sin(rotate.Amount) * 2f;

            transform.Rotate(Vector3.UnitX, MathHelper.DegreesToRadians(waveRotation));
            transform.Rotate(Vector3.UnitY, MathHelper.DegreesToRadians(waveRotation));
            transform.Rotate(Vector3.UnitZ, MathHelper.DegreesToRadians(waveRotation));

            float x = (float)Math.Cos(rotate.Amount) * 0.5f;
            float y = (float)Math.Sin(rotate.Amount) * 0.5f;
            float z = (float)Math.Cos(rotate.Amount * 0.5f) * 0.5f;

            transform.Translate(new Vector3(x, y, z), 0.1f);

            rotate.Amount += 0.01f;
        }
    }
}
