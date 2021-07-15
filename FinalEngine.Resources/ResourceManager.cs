// <copyright file="ResourceManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources
{
    using System;
    using System.Collections.Generic;

    public class ResourceManager : IResourceManager
    {
        private readonly IDictionary<string, IResource> pathToResourceMap;

        private readonly IDictionary<Type, IResourceLoaderInternal> typeToLoaderMap;

        public ResourceManager()
        {
            this.typeToLoaderMap = new Dictionary<Type, IResourceLoaderInternal>();
            this.pathToResourceMap = new Dictionary<string, IResource>();
        }

        ~ResourceManager()
        {
            this.Dispose(false);
        }

        protected bool IsDisposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T GetResource<T>(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), $"The specified {nameof(filePath)} parameter cannot be null, empty of consist of only whitespace characters.");
            }

            if (!this.typeToLoaderMap.TryGetValue(typeof(T), out IResourceLoaderInternal? loader))
            {
                throw new Exception($"A register for the specified type {nameof(T)} has not been reigstered to this resource manager.");
            }

            if (!this.pathToResourceMap.TryGetValue(filePath, out IResource? resource))
            {
                resource = loader.LoadResource(filePath);
            }

            return (T)resource;
        }

        public void RegisterLoader<T>(ResourceLoaderBase<T> loader)
            where T : IResource
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader), $"The specified {nameof(loader)} parameter cannot be null.");
            }

            Type type = typeof(T);

            if (this.typeToLoaderMap.ContainsKey(type))
            {
                throw new ArgumentException($"The specified {nameof(T)} loader type has already been registered.", nameof(T));
            }

            this.typeToLoaderMap.Add(type, loader);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.pathToResourceMap.Count != 0)
                {
                    foreach (IResource resource in this.pathToResourceMap.Values)
                    {
                        resource.Dispose();
                    }

                    this.pathToResourceMap.Clear();
                }

                this.IsDisposed = true;
            }
        }
    }
}