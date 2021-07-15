// <copyright file="IEntityWorld.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    using System;

    /// <summary>
    ///   Defines an interface that creates the connection between <see cref="Entity"/> and <see cref="EntitySystemBase"/>.
    /// </summary>
    public interface IEntityWorld
    {
        /// <summary>
        ///   Adds the specified <paramref name="entity"/> to this <see cref="IEntityWorld"/>.
        /// </summary>
        /// <param name="entity">
        ///   The entity to add to this <see cref="EntityWorld"/>.
        /// </param>
        void AddEntity(Entity entity);

        /// <summary>
        ///   Adds the specified <paramref name="system"/> to this <see cref="IEntityWorld"/>.
        /// </summary>
        /// <param name="system">
        ///   The system to add to this <see cref="IEntityWorld"/>.
        /// </param>
        void AddSystem(EntitySystemBase system);

        void ProcessAll(GameLoopType type);

        /// <summary>
        ///   Removes the specified <paramref name="entity"/> from this <see cref="IEntityWorld"/>.
        /// </summary>
        /// <param name="entity">
        ///   The entity to remove from this <see cref="IEntityWorld"/>.
        /// </param>
        void RemoveEntity(Entity entity);

        /// <summary>
        ///   Removes a system of the specified <paramref name="type"/> from this <see cref="IEntityWorld"/>.
        /// </summary>
        /// <param name="type">
        ///   The type of system to remove from this <see cref="IEntityWorld"/>.
        /// </param>
        /// <remarks>
        ///   The specified <paramref name="type"/> must inherit from <see cref="EntitySystemBase"/>.
        /// </remarks>
        void RemoveSystem(Type type);
    }
}