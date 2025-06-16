// <copyright file="ResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;
using System.Collections.Generic;
using System.Linq;
using FinalEngine.Resources.Exceptions;

internal sealed class ResourceManager : IResourceManager
{
    private static ResourceManager? instance;

    private readonly Dictionary<string, ResourceData> pathToResourceDataMap;

    private readonly Dictionary<Type, IResourceLoader> typeToLoaderMap;

    private bool isDisposed;

    public ResourceManager()
    {
        this.typeToLoaderMap = [];
        this.pathToResourceDataMap = [];
    }

    ~ResourceManager()
    {
        this.Dispose(false);
    }

    public static IResourceManager Instance
    {
        get { return instance ??= new ResourceManager(); }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public T LoadResource<T>(string filePath)
        where T : IResource
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(ResourceManager));
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!this.typeToLoaderMap.TryGetValue(typeof(T), out var loader))
        {
            throw new ResourceLoaderNotRegisteredException($"The specified {nameof(T)} parameter does not have an associated registered loader.");
        }

        if (!this.pathToResourceDataMap.TryGetValue(filePath, out var resourceData))
        {
            resourceData = new ResourceData(filePath, loader.LoadResource(filePath));
            this.pathToResourceDataMap.Add(filePath, resourceData);
        }

        resourceData.IncrementReferenceCount();

        return (T)resourceData.Reference;
    }

    public void RegisterLoader<T>(ResourceLoaderBase<T> loader)
        where T : IResource
    {
        this.RegisterLoader(loader);
    }

    void IResourceManager.RegisterLoader(IResourceLoader loader)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(ResourceManager));
        ArgumentNullException.ThrowIfNull(loader);

        var type = loader.GetResourceType();

        if (this.ContainsLoader(type))
        {
            throw new InvalidOperationException($"The specified {nameof(type)} parameter has already been registered to a resource loader: '{type.FullName}'");
        }

        this.typeToLoaderMap.Add(type, loader);
    }

    public void UnloadResource(IResource resource)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(ResourceManager));
        ArgumentNullException.ThrowIfNull(resource);

        for (int i = this.pathToResourceDataMap.Count - 1; i >= 0; i--)
        {
            var kvp = this.pathToResourceDataMap.ElementAt(i);
            var resourceData = kvp.Value;

            if (ReferenceEquals(resourceData.Reference, resource))
            {
                resourceData.DecrementReferenceCount();

                if (resourceData.ReferenceCount == 0)
                {
                    if (resourceData.Reference is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }

                    this.pathToResourceDataMap.Remove(resourceData.FilePath);
                }
            }
        }
    }

    private bool ContainsLoader(Type type)
    {
        return this.typeToLoaderMap.ContainsKey(type);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.pathToResourceDataMap != null)
            {
                for (int i = this.pathToResourceDataMap.Count - 1; i >= 0; i--)
                {
                    var kvp = this.pathToResourceDataMap.ElementAt(i);
                    var resourceData = kvp.Value;

                    if (resourceData.Reference is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }

                    this.pathToResourceDataMap.Remove(resourceData.FilePath);
                }
            }

            this.typeToLoaderMap.Clear();
        }

        this.isDisposed = true;
    }

    private sealed class ResourceData
    {
        public ResourceData(string filePath, IResource reference)
        {
            this.FilePath = filePath;
            this.Reference = reference;
        }

        public string FilePath { get; }

        public IResource Reference { get; }

        public int ReferenceCount { get; private set; }

        public void DecrementReferenceCount()
        {
            this.ReferenceCount--;
        }

        public void IncrementReferenceCount()
        {
            this.ReferenceCount++;
        }
    }
}
