// <copyright file="IEntitySystemResolver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Resolving;

internal interface IEntitySystemResolver
{
    TSystem GetEntitySystem<TSystem>()
        where TSystem : EntitySystemBase;
}
