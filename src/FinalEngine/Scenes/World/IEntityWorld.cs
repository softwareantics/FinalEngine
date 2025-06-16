// <copyright file="IEntityWorld.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes.World;

using System;
using FinalEngine.Scenes.Entities;
using FinalEngine.Scenes.Systems;

internal delegate void EntitySystemAddedEventHandler(object sender, EntitySystemBase system);

internal delegate void EntitySystemRemovedEventHandler(object sender, EntitySystemBase system);

internal interface IEntityWorld
{
    event EntitySystemAddedEventHandler? OnEntitySystemAdded;

    event EntitySystemRemovedEventHandler? OnEntitySystemRemoved;

    Entity AddEntity(Entity entity);

    Entity AddEntity<TFactory>()
        where TFactory : IEntityFactory;

    void AddSystem(EntitySystemBase system);

    void AddSystem<TSystem>()
        where TSystem : EntitySystemBase;

    void ProcessAll(string eventName);

    void RemoveEntity(Entity entity);

    void RemoveSystem(Type type);
}
