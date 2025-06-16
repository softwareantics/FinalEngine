// <copyright file="Scene.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes;

using System;
using FinalEngine.Scenes.Entities;
using FinalEngine.Scenes.Systems;
using FinalEngine.Scenes.World;

internal sealed class Scene : IScene
{
    private readonly IEntityWorld world;

    private bool isDisposed;

    public Scene(IEntityWorld world)
    {
        this.world = world ?? throw new ArgumentNullException(nameof(world));
        this.world.OnEntitySystemAdded += this.World_OnEntitySystemAdded;
    }

    ~Scene()
    {
        this.Dispose(false);
    }

    public void AddEntity(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.world.AddEntity(entity);
    }

    public void AddSystem(EntitySystemBase system)
    {
        ArgumentNullException.ThrowIfNull(system);
        this.world.AddSystem(system);
    }

    public void AddSystem<TSystem>()
        where TSystem : EntitySystemBase
    {
        this.world.AddSystem<TSystem>();
    }

    public Entity CreateEntity<TFactory>()
        where TFactory : IEntityFactory
    {
        return this.world.AddEntity<TFactory>();
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    void IScene.Initialize()
    {
        this.world.ProcessAll("Initialize");
    }

    void IScene.LoadContent()
    {
        this.world.ProcessAll("LoadContent");
    }

    public void ProcessAll(string eventName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(eventName);
        this.world.ProcessAll(eventName);
    }

    public void RemoveEntity(Entity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.world.RemoveEntity(entity);
    }

    public void RemoveSystem(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        this.world.RemoveSystem(type);
    }

    void IScene.Render(float deltaTime)
    {
        this.world.ProcessAll("Render");
    }

    void IScene.UnloadContent()
    {
        this.world.ProcessAll("UnloadContent");
    }

    void IScene.Update(float deltaTime)
    {
        this.world.ProcessAll("Update");
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            this.world.OnEntitySystemAdded -= this.World_OnEntitySystemAdded;
        }

        this.isDisposed = true;
    }

    private void World_OnEntitySystemAdded(object sender, EntitySystemBase system)
    {
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(system);
    }
}
