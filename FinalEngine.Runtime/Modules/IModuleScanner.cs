// <copyright file="IModuleScanner.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Modules;

using FinalEngine.Utilities.Modules;

internal interface IModuleScanner
{
    IEnumerable<IEngineModule> Scan();
}
