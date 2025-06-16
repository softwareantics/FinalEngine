// <copyright file="IEntitySystemResolver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes.Systems;
internal interface IEntitySystemResolver
{
    EntitySystemBase GetEntitySystem<TSystem>()
        where TSystem : EntitySystemBase;
}
