// <copyright file="IEntityWorld.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

using System;
using System.Collections.Generic;
using FinalEngine.ECS.Blackboard;

public interface IEntityWorld
{
    IReadOnlyCollection<Entity> Entities { get; }

    void AddEntity(Entity entity);

    void AddEntityFromFactory<TFactory>()
            where TFactory : IEntityFactory;

    void AddResource<T>(T resource)
        where T : IBlackboardResource;

    void AddSystem<TSystem>()
                    where TSystem : EntitySystemBase;

    void AddSystem(EntitySystemBase system);

    T GetResource<T>()
        where T : IBlackboardResource;

    void ProcessAll(string eventName);

    void RemoveEntity(Entity entity);

    void RemoveSystem(Type type);
}
