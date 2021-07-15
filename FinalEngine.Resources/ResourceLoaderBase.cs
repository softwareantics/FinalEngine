// <copyright file="ResourceLoaderBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources
{
    public abstract class ResourceLoaderBase<T> : IResourceLoaderInternal
        where T : IResource
    {
        public abstract T LoadResource(string filePath);

        object IResourceLoaderInternal.LoadResource(string filePath)
        {
            return this.LoadResource(filePath);
        }
    }
}