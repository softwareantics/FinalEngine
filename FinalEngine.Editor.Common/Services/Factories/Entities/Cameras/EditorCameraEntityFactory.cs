// <copyright file="EditorCameraEntityFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Factories.Entities.Cameras;

using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Physics.Components;
using FinalEngine.Rendering.Components;

internal sealed class EditorCameraEntityFactory : IEntityFactory
{
    public Entity CreateEntity()
    {
        var entity = new Entity();

        entity.AddComponent<TransformComponent>();
        entity.AddComponent<VelocityComponent>();
        entity.AddComponent<PerspectiveComponent>();
        entity.AddComponent<CameraComponent>();

        return entity;
    }
}
