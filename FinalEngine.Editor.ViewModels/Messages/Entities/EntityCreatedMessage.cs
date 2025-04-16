// <copyright file="EntityCreatedMessage.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Messages.Entities;

using FinalEngine.ECS;

internal sealed class EntityCreatedMessage
{
    public EntityCreatedMessage(Entity entity)
    {
        this.Entity = entity ?? throw new System.ArgumentNullException(nameof(entity));
    }

    public Entity Entity { get; }
}
