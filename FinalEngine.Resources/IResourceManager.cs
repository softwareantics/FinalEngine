// <copyright file="IResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources
{
    public interface IResourceManager
    {
        T GetResource<T>(string filePath);

        void RegisterLoader<T>(ResourceLoaderBase<T> loader)
                    where T : IResource;
    }
}