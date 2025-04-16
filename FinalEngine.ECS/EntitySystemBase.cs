// <copyright file="EntitySystemBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public abstract class EntitySystemBase
{
    private readonly List<Entity> entities;

    protected EntitySystemBase()
    {
        this.UniqueIdentifier = Guid.NewGuid();
        this.entities = [];
    }

    public Guid UniqueIdentifier { get; }

    protected IEntityWorld World { get; private set; }

    public void Process()
    {
        this.Process([.. this.entities]);
    }

    internal void AddOrRemoveByAspect(Entity entity, bool forceRemove = false)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        bool isMatch = this.IsMatch(entity);
        bool isAdded = this.entities.Contains(entity);

        if (forceRemove && isAdded)
        {
            this.entities.Remove(entity);
            this.OnEntityRemoved(entity);
        }
        else if (isMatch && !isAdded)
        {
            this.entities.Add(entity);
            this.OnEntityAdded(entity);
        }
        else if (!isMatch && isAdded)
        {
            this.entities.Remove(entity);
            this.OnEntityRemoved(entity);
        }
    }

    internal void RemoveAllEntities()
    {
        this.entities.Clear();
    }

    internal void SetWorld(IEntityWorld world)
    {
        this.World = world ?? throw new ArgumentNullException(nameof(world));
    }

    protected virtual bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return false;
    }

    protected virtual void OnEntityAdded(Entity entity)
    {
    }

    protected virtual void OnEntityRemoved(Entity entity)
    {
    }

    protected abstract void Process([NotNull] IEnumerable<Entity> entities);
}
