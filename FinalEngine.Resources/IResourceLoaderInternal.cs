// <copyright file="IResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources
{
    internal interface IResourceLoaderInternal
    {
        IResource LoadResource(string filePath);
    }
}