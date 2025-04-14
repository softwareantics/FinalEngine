// <copyright file="EntitySystemProcessAttribute.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Attributes;

using System;

/// <summary>
///   Provides an attribute used to determine when an <see cref="EntitySystemBase"/> will execute.
/// </summary>
/// <seealso cref="System.Attribute"/>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class EntitySystemProcessAttribute : Attribute
{
    /// <summary>
    ///   Gets or sets the name of the event that will execute the system.
    /// </summary>
    /// <value>
    ///   The event that will execute the system.
    /// </value>
    public string? EventName { get; set; }
}
