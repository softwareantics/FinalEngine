// <copyright file="IScene.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes;

using FinalEngine.Scenes.Entities;
using FinalEngine.Scenes.Systems;

public interface IScene : IDisposable
{
    void AddEntity(Entity entity);

    void AddSystem(EntitySystemBase system);

    void AddSystem<TSystem>()
        where TSystem : EntitySystemBase;

    Entity CreateEntity<TFactory>()
        where TFactory : IEntityFactory;

    void ProcessAll(string eventName);

    void RemoveEntity(Entity entity);

    void RemoveSystem(Type type);

    internal void Initialize();

    internal void LoadContent();

    internal void Render(float deltaTime);

    internal void UnloadContent();

    internal void Update(float deltaTime);
}
