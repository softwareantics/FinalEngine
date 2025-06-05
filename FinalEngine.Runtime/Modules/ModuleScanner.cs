// <copyright file="ModuleScanner.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Modules;

using System.Collections.Generic;
using System.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using FinalEngine.Utilities.Modules;
using Microsoft.Extensions.Logging;

internal sealed class ModuleScanner : IModuleScanner
{
    private readonly ILogger<ModuleScanner> logger;

    public ModuleScanner(ILogger<ModuleScanner> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<IEngineModule> Scan()
    {
        var assemblies = this.GetLoadedAssembliesInDirectory(AppContext.BaseDirectory);

        var configuration = new ContainerConfiguration()
            .WithAssemblies(assemblies);

        try
        {
            using (var container = configuration.CreateContainer())
            {
                return container.GetExports<IEngineModule>();
            }
        }
        catch (ReflectionTypeLoadException ex)
        {
            foreach (var loaderException in ex.LoaderExceptions)
            {
                this.logger.LogError(loaderException, "Loader exception during MEF composition.");
            }

            foreach (var t in ex.Types)
            {
                this.logger.LogError("Type: {Type}", t?.FullName ?? "null");
            }

            throw;
        }
    }

    private IEnumerable<Assembly> GetLoadedAssembliesInDirectory(string directory)
    {
        var assemblies = new List<Assembly>();

        this.logger.LogInformation("Scanning directory for assemblies: '{Directory}'", directory);

        foreach (string filePath in Directory.GetFiles(directory, "*.dll", SearchOption.TopDirectoryOnly))
        {
            string fullPath = Path.GetFullPath(filePath);
            string fileName = Path.GetFileName(fullPath);

            if (fileName.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase) ||
                fileName.StartsWith("System", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            this.logger.LogTrace("Found DLL on disk: {File}", filePath);

            if (!this.TryLoadAssembly(fullPath, out var assembly))
            {
                continue;
            }

            assemblies.Add(assembly!);
        }

        this.logger.LogInformation("Discovered {Count} assemblies.", assemblies.Count);

        return assemblies;
    }

    private bool TryLoadAssembly(string filePath, out Assembly? assembly)
    {
        assembly = null;

        try
        {
            assembly = Assembly.Load(File.ReadAllBytes(filePath));
            return true;
        }
        catch (FileNotFoundException ex)
        {
            this.logger.LogWarning(ex, "Assembly not found: {Assembly}", filePath);
        }
        catch (BadImageFormatException ex)
        {
            this.logger.LogWarning(ex, "Invalid assembly format: {Assembly}", filePath);
        }
        catch (FileLoadException ex)
        {
            this.logger.LogWarning(ex, "Failed to load assembly: {Assembly}", filePath);
        }

        return false;
    }
}
