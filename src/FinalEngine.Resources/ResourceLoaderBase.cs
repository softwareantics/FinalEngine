// <copyright file="ResourceLoaderBase.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

public abstract class ResourceLoaderBase<TResource> : IResourceLoader
    where TResource : IResource
{
    IResource IResourceLoader.LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        return this.LoadResource(filePath);
    }

    protected abstract TResource LoadResource(string filePath);
}
