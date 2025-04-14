// <copyright file="EntityWorld.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using FinalEngine.ECS.Attributes;
using FinalEngine.ECS.Blackboard;
using FinalEngine.ECS.Exceptions;
using FinalEngine.ECS.Resolving;

internal sealed class EntityWorld : IEntityWorld
{
    private readonly ObservableCollection<Entity> entities;

    private readonly IEntityFactoryResolver factoryResolver;

    private readonly IEntitySystemResolver systemResolver;

    private readonly List<EntitySystemBase> systems;

    private readonly Dictionary<Type, IBlackboardResource> typeToResourceMap;

    public EntityWorld(IEntitySystemResolver systemResolver, IEntityFactoryResolver factoryResolver)
    {
        this.systemResolver = systemResolver ?? throw new ArgumentNullException(nameof(systemResolver));
        this.factoryResolver = factoryResolver ?? throw new ArgumentNullException(nameof(factoryResolver));
        this.typeToResourceMap = [];

        this.entities = [];
        this.systems = [];
    }

    public IReadOnlyCollection<Entity> Entities
    {
        get { return this.entities; }
    }

    public void AddEntity(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        if (this.entities.Contains(entity))
        {
            throw new ArgumentException($"The specified {nameof(entity)} parameter has already been added to this entity world.", nameof(entity));
        }

        entity.OnComponentsChanged += this.Entity_OnComponentsChanged;

        foreach (var system in this.systems)
        {
            system.AddOrRemoveByAspect(entity);
        }

        this.entities.Add(entity);
    }

    public void AddEntityFromFactory<TFactory>()
        where TFactory : IEntityFactory
    {
        this.AddEntity(this.factoryResolver.GetEntityFactory<TFactory>().CreateEntity());
    }

    public void AddResource<T>(T resource)
            where T : IBlackboardResource
    {
        ArgumentNullException.ThrowIfNull(resource);

        var key = typeof(T);

        if (this.typeToResourceMap.ContainsKey(key))
        {
            throw new InvalidOperationException($"A resource of type '{key.Name}' has already been added.");
        }

        this.typeToResourceMap[key] = resource;
    }

    public void AddSystem(EntitySystemBase system)
    {
        ArgumentNullException.ThrowIfNull(system, nameof(system));

        foreach (var other in this.systems)
        {
            if (other.GetType() == system.GetType())
            {
                throw new ArgumentException($"The specified {nameof(system)} is a type that has already been added to this entity world.", nameof(system));
            }
        }

        foreach (var entity in this.entities)
        {
            system.AddOrRemoveByAspect(entity);
        }

        system.SetWorld(this);
        this.systems.Add(system);
    }

    public void AddSystem<TSystem>()
        where TSystem : EntitySystemBase
    {
        this.AddSystem(this.systemResolver.GetEntitySystem<TSystem>());
    }

    public T GetResource<T>()
            where T : IBlackboardResource
    {
        if (!this.typeToResourceMap.TryGetValue(typeof(T), out var r))
        {
            throw new KeyNotFoundException($"No resource of type '{typeof(T).Name}' has been added.");
        }

        return (T)r;
    }

    public void ProcessAll(string eventName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(eventName);

        foreach (var system in this.systems)
        {
            var attribute = system.GetType().GetCustomAttribute<EntitySystemProcessAttribute>();

            if (attribute != null && attribute.EventName == eventName)
            {
                system.Process();
            }
        }
    }

    public void RemoveEntity(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        if (!this.entities.Contains(entity))
        {
            throw new EntityNotFoundException(entity.UniqueIdentifier);
        }

        entity.OnComponentsChanged -= this.Entity_OnComponentsChanged;

        foreach (var system in this.systems)
        {
            system.AddOrRemoveByAspect(entity, true);
        }

        this.entities.Remove(entity);
    }

    public void RemoveSystem(Type type)
    {
        ArgumentNullException.ThrowIfNull(type, nameof(type));

        if (!typeof(EntitySystemBase).IsAssignableFrom(type))
        {
            throw new ArgumentException($"The specified {nameof(type)} parameter does not inherit from {nameof(EntitySystemBase)}.", nameof(type));
        }

        for (int i = this.systems.Count - 1; i >= 0; i--)
        {
            if (this.systems[i].GetType() == type)
            {
                var system = this.systems[i];

                system.RemoveAllEntities();
                this.systems.RemoveAt(i);

                return;
            }
        }

        throw new ArgumentException($"The specified {nameof(type)} parameter is not an entity system type that has been added to this entity world.", nameof(type));
    }

    private void Entity_OnComponentsChanged(object? sender, EventArgs e)
    {
        if (sender is not Entity entity)
        {
            throw new ArgumentException($"The specified {nameof(sender)} parameter is not of type {nameof(Entity)}.", nameof(sender));
        }

        foreach (var system in this.systems)
        {
            system.AddOrRemoveByAspect(entity);
        }
    }
}
