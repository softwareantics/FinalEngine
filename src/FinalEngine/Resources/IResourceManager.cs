// <copyright file="IResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;

public interface IResourceManager : IDisposable
{
    T LoadResource<T>(string filePath)
        where T : IResource;

    void RegisterLoader<T>(ResourceLoaderBase<T> loader)
        where T : IResource;

    void UnloadResource(IResource resource);

    internal void RegisterLoader(IResourceLoader loader);
}
