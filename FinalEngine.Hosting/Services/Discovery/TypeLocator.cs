// <copyright file="TypeLocator.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting.Services.Discovery;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Extensions.Logging;

internal sealed class TypeLocator : ITypeLocator
{
    private readonly ILogger<TypeLocator> logger;

    public TypeLocator(ILogger<TypeLocator> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<Type> GetSupportedTypes(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        try
        {
            return [.. assembly.GetTypes().Where(IsSupportedType)];
        }
        catch (ReflectionTypeLoadException ex)
        {
            this.logger.LogWarning(ex, "Unable to load types from assembly '{Assembly}'.", assembly.FullName);
        }

        return [];
    }

    private static bool IsSupportedType(Type type)
    {
        // Skip abstract classes, interfaces, and generics.
        if (type is null || type.IsAbstract || type.IsInterface || type.ContainsGenericParameters)
        {
            return false;
        }

        // Ensure the type is a service configurator.
        if (!typeof(IServiceConfigurator).IsAssignableFrom(type))
        {
            return false;
        }

        if (type is TypeBuilder ||
            type == typeof(TypedReference) ||
            type == typeof(ArgIterator) ||
            type == typeof(void) ||
            type == typeof(RuntimeArgumentHandle))
        {
            return false;
        }

        // Skip arrays of unsupported types
        if (type.IsArray &&
            (type.GetElementType() == typeof(TypedReference) ||
             type.GetElementType() == typeof(ArgIterator) ||
             type.GetElementType() == typeof(void) ||
             type.GetElementType() == typeof(RuntimeArgumentHandle)))
        {
            return false;
        }

        return true;
    }
}
