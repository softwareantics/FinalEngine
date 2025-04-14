// <copyright file="ViewportUpdateEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Systems;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;
using FinalEngine.ECS.Components;
using FinalEngine.Editor.Common.Blackboard;

[EntitySystemProcess(EventName = "Update")]
internal sealed class ViewportUpdateEntitySystem : EntitySystemBase
{
    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<CameraComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var camera = entity.GetComponent<CameraComponent>();
            camera.Viewport = this.World.GetResource<ViewportBlackboardResource>().Resource;
        }
    }
}
