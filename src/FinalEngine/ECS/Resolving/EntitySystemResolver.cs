// <copyright file="EntitySystemResolver.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Resolving;

using System;

internal sealed class EntitySystemResolver : IEntitySystemResolver
{
    private readonly IServiceProvider serviceProvider;

    public EntitySystemResolver(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public TSystem GetEntitySystem<TSystem>()
        where TSystem : EntitySystemBase
    {
        return this.serviceProvider.GetService(typeof(TSystem)) as TSystem
            ?? throw new InvalidOperationException($"Entity system of type {typeof(TSystem).FullName} could not be resolved.");
    }
}
