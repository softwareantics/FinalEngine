// <copyright file="SpinUpdateEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Physics.Systems;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;
using FinalEngine.ECS.Components;
using FinalEngine.Physics.Components;
using FinalEngine.Utilities;

[EntitySystemProcess(EventName = "Update")]
public sealed class SpinUpdateEntitySystem : EntitySystemBase
{
    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() && entity.ContainsComponent<SpinComponent>() && entity.ContainsComponent<VelocityComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var spin = entity.GetComponent<SpinComponent>();
            var velocity = entity.GetComponent<VelocityComponent>();

            transform.Rotate(spin.Axis, velocity.Speed * GameTime.Delta);
        }
    }
}
