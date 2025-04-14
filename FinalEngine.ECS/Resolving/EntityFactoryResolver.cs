// <copyright file="EntityFactoryResolver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Resolving;

using System;
using Microsoft.Extensions.DependencyInjection;

internal sealed class EntityFactoryResolver : IEntityFactoryResolver
{
    private readonly IServiceProvider serviceProvider;

    public EntityFactoryResolver(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public IEntityFactory GetEntityFactory<TFactory>()
        where TFactory : IEntityFactory
    {
        return this.serviceProvider.GetRequiredService<TFactory>();
    }
}
