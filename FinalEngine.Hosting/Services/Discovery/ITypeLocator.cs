// <copyright file="ITypeLocator.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting.Services.Discovery;

using System.Reflection;

internal interface ITypeLocator
{
    IEnumerable<Type> GetSupportedTypes(Assembly assembly);
}
