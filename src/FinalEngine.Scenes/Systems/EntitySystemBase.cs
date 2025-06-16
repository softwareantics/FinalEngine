// <copyright file="EntitySystemBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes.Systems;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.Resources;
using FinalEngine.Scenes.Entities;

public abstract class EntitySystemBase
{
    private readonly List<Entity> entities;

    protected EntitySystemBase()
    {
        this.UniqueIdentifier = Guid.NewGuid();
        this.entities = [];
    }

    public Guid UniqueIdentifier { get; }

    protected IResourceManager ResourceManager { get; private set; }

    public void Process()
    {
        this.Process([.. this.entities]);
    }

    internal void AddOrRemoveByAspect(Entity entity, bool forceRemove = false)
    {
        ArgumentNullException.ThrowIfNull(entity);

        bool isMatch = this.IsMatch(entity);
        bool isAdded = this.entities.Contains(entity);

        if ((forceRemove && isAdded) || (!isMatch && isAdded))
        {
            this.entities.Remove(entity);
            this.OnEntityRemoved(entity);
        }
        else if (isMatch && !isAdded)
        {
            this.entities.Add(entity);
            this.OnEntityAdded(entity);
        }
    }

    internal void RemoveAllEntities()
    {
        this.entities.Clear();
    }

    internal void SetResourceManager(IResourceManager resourceManager)
    {
        ArgumentNullException.ThrowIfNull(resourceManager);
        this.ResourceManager = resourceManager;
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
