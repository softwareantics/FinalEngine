// <copyright file="EntitySystemResolver.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes.Systems;

using System;

internal sealed class EntitySystemResolver : IEntitySystemResolver
{
    private readonly Func<Type, EntitySystemBase> getEntitySystem;

    public EntitySystemResolver(Func<Type, EntitySystemBase> getEntitySystem)
    {
        this.getEntitySystem = getEntitySystem ?? throw new ArgumentNullException(nameof(getEntitySystem));
    }

    public EntitySystemBase GetEntitySystem<TSystem>()
        where TSystem : EntitySystemBase
    {
        return this.getEntitySystem(typeof(TSystem));
    }
}
