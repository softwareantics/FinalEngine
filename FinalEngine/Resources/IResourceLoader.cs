// <copyright file="IResourceLoader.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

internal interface IResourceLoader
{
    IResource LoadResource(string filePath);
}
