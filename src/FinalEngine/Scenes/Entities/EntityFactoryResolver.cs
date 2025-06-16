// <copyright file="EntityFactoryResolver.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes.Entities;

internal sealed class EntityFactoryResolver : IEntityFactoryResolver
{
    private readonly Func<Type, IEntityFactory> getEntityFactory;

    public EntityFactoryResolver(Func<Type, IEntityFactory> getEntityFactory)
    {
        this.getEntityFactory = getEntityFactory ?? throw new ArgumentNullException(nameof(getEntityFactory));
    }

    public IEntityFactory GetEntityFactory<TFactory>()
        where TFactory : IEntityFactory
    {
        return this.getEntityFactory(typeof(TFactory));
    }
}
