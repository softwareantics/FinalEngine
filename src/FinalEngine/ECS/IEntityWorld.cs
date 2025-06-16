// <copyright file="IEntityWorld.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

using System;

public interface IEntityWorld
{
    void AddEntity(Entity entity);

    void AddSystem<TSystem>()
        where TSystem : EntitySystemBase;

    void AddSystem(EntitySystemBase system);

    void ProcessAll(string eventName);

    void RemoveEntity(Entity entity);

    void RemoveSystem(Type type);
}
