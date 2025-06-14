// <copyright file="ResourceLoaderBase.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

public abstract class ResourceLoaderBase<TResource> : IResourceLoader
    where TResource : IResource
{
    public abstract TResource LoadResource(string filePath);

    IResource IResourceLoader.LoadResource(string filePath)
    {
        throw new NotImplementedException();
    }
}
