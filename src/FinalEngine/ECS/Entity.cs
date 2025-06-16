// <copyright file="Entity.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

using System;
using System.Collections.Generic;
using System.Dynamic;

public class Entity : DynamicObject, IReadOnlyEntity
{
    private readonly Dictionary<Type, IEntityComponent> typeToComponentMap;

    public Entity(Guid? uniqueID = null)
    {
        this.UniqueIdentifier = uniqueID ?? Guid.NewGuid();
        this.typeToComponentMap = [];
    }

    public ICollection<IEntityComponent> Components
    {
        get { return this.typeToComponentMap.Values; }
    }

    public Guid UniqueIdentifier { get; }

    internal EventHandler<EventArgs>? OnComponentsChanged { get; set; }

    public void AddComponent(IEntityComponent component)
    {
        ArgumentNullException.ThrowIfNull(component);

        var type = component.GetType();

        if (this.ContainsComponent(type))
        {
            throw new ArgumentException($"The specified {nameof(component)} parameters type has already been added to this entity", nameof(component));
        }

        this.typeToComponentMap.Add(type, component);
        this.OnComponentsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddComponent<TComponent>()
        where TComponent : IEntityComponent, new()
    {
        this.AddComponent(Activator.CreateInstance<TComponent>());
    }

    public bool ContainsComponent(IEntityComponent component)
    {
        ArgumentNullException.ThrowIfNull(component);

        foreach (var other in this.typeToComponentMap.Values)
        {
            if (other == component)
            {
                return true;
            }
        }

        return false;
    }

    public bool ContainsComponent(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return !typeof(IEntityComponent).IsAssignableFrom(type)
            ? throw new ArgumentException($"The specified {nameof(type)} parameter does not implement {nameof(IEntityComponent)}.", nameof(type))
            : this.typeToComponentMap.ContainsKey(type);
    }

    public bool ContainsComponent<TComponent>()
        where TComponent : IEntityComponent
    {
        return this.ContainsComponent(typeof(TComponent));
    }

    public IEntityComponent GetComponent(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (!typeof(IEntityComponent).IsAssignableFrom(type))
        {
            throw new ArgumentException($"The specified {nameof(type)} parameter does not implement {nameof(IEntityComponent)}.", nameof(type));
        }

        return !this.ContainsComponent(type)
            ? throw new ArgumentException($"The specified {nameof(type)} parameter is not a component type that has been added to this entity.", nameof(type))
            : this.typeToComponentMap[type];
    }

    public TComponent GetComponent<TComponent>()
        where TComponent : IEntityComponent
    {
        return (TComponent)this.GetComponent(typeof(TComponent));
    }

    public void RemoveComponent(IEntityComponent component)
    {
        ArgumentNullException.ThrowIfNull(component);

        if (!this.ContainsComponent(component))
        {
            throw new ArgumentException($"The specified {nameof(component)} parameter has not been added to this entity.", nameof(component));
        }

        this.RemoveComponent(component.GetType());
    }

    public void RemoveComponent(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (!typeof(IEntityComponent).IsAssignableFrom(type))
        {
            throw new ArgumentException($"The specified {nameof(type)} parameter does not implement {nameof(IEntityComponent)}.", nameof(type));
        }

        if (!this.ContainsComponent(type))
        {
            throw new ArgumentException($"The specified {nameof(type)} parameter is not a component type that has been added to this entity.", nameof(type));
        }

        this.typeToComponentMap.Remove(type);
        this.OnComponentsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveComponent<TComponent>()
        where TComponent : IEntityComponent
    {
        this.RemoveComponent(typeof(TComponent));
    }

    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        ArgumentNullException.ThrowIfNull(binder);

        string? memberName = binder.Name;

        foreach (var kvp in this.typeToComponentMap)
        {
            string? typeName = kvp.Key.Name;

            if (typeName.EndsWith("Component", StringComparison.CurrentCulture))
            {
                if (typeName == memberName)
                {
                    result = kvp.Value;
                    return true;
                }

                typeName = typeName[..^"Component".Length];
            }

            if (typeName == memberName)
            {
                result = kvp.Value;
                return true;
            }
        }

        result = null;
        return false;
    }
}
