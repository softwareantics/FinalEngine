// <copyright file="IResourceLoader.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

public interface IResourceLoader
{
    Type GetResourceType();

    IResource LoadResource(string filePath);
}
