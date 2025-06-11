// <copyright file="AssembyLoader.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting.Services.Loading;

using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;

internal sealed class AssembyLoader : IAssemblyLoader
{
    private readonly ILogger<AssembyLoader> logger;

    public AssembyLoader(ILogger<AssembyLoader> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<Assembly> LoadAssemblies()
    {
        var loadedAssemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(x => !IsIgnoredAssembly(x))
            .ToList();

        foreach (var assembly in loadedAssemblies)
        {
            yield return assembly;
        }

        var assemblyNames = loadedAssemblies
            .Select(a => a.FullName!)
            .ToHashSet();

        foreach (string dllPath in Directory.EnumerateFiles(AppContext.BaseDirectory, "*.dll"))
        {
            Assembly? assembly = null;

            try
            {
                var name = AssemblyName.GetAssemblyName(dllPath);

                if (assemblyNames.Add(name.FullName))
                {
                    assembly = Assembly.Load(name);
                }
            }
            catch
            {
                // Ignore non-.NET or invalid assemblies
            }

            if (assembly is not null && !IsIgnoredAssembly(assembly))
            {
                yield return assembly;
            }
        }
    }

    private static bool IsIgnoredAssembly(Assembly assembly)
    {
        string? name = assembly.FullName;

        return name is null ||
               assembly.IsDynamic ||
               assembly.IsCollectible ||
               name.StartsWith("System.", StringComparison.OrdinalIgnoreCase) ||
               name.StartsWith("Microsoft.", StringComparison.OrdinalIgnoreCase) ||
               name.StartsWith("PresentationFramework", StringComparison.OrdinalIgnoreCase) ||
               name.StartsWith("WindowBase", StringComparison.OrdinalIgnoreCase);
    }
}
