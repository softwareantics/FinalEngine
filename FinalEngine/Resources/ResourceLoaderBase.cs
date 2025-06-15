// <copyright file="ResourceLoaderBase.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public abstract class ResourceLoaderBase<TResource> : IResourceLoader
    where TResource : IResource
{
    public abstract TResource LoadResource(string filePath);

    IResource IResourceLoader.LoadResource(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);
        return this.LoadResource(filePath);
    }
}
