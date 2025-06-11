// <copyright file="IAssemblyLoader.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting.Services.Loading;

using System.Reflection;

internal interface IAssemblyLoader
{
    IEnumerable<Assembly> LoadAssemblies();
}
