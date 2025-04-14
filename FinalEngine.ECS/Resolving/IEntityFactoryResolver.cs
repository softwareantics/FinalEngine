// <copyright file="IEntityFactoryResolver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Resolving;

internal interface IEntityFactoryResolver
{
    IEntityFactory GetEntityFactory<TFactory>()
        where TFactory : IEntityFactory;
}
