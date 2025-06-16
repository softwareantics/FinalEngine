// <copyright file="EntityNotFoundException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Exceptions;

using System;
using FinalEngine.ECS;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
        : base($"An {nameof(Entity)} was not found.")
    {
    }

    public EntityNotFoundException(Guid uniqueIdentifier)
        : base($"An {nameof(Entity)} that matches ID: '{uniqueIdentifier}' was not found.")
    {
        this.UniqueIdentifier = uniqueIdentifier;
    }

    public EntityNotFoundException(string message)
        : base(message)
    {
    }

    public EntityNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public Guid UniqueIdentifier { get; }
}
