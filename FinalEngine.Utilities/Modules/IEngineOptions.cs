// <copyright file="IEngineOptions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities.Modules;

using Microsoft.Extensions.DependencyInjection;

public interface IEngineOptions
{
    IServiceCollection Services { get; }
}
