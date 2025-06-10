// <copyright file="ServiceLocator.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine;

//// TODO: Unit tests

internal static class ServiceLocator
{
    public static IServiceProvider Instance { get; private set; } = null!;

    public static void SetServiceProvider(IServiceProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);
        Instance = provider;
    }
}
