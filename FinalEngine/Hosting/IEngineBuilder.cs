// <copyright file="IEngineBuilder.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting;

using Microsoft.Extensions.DependencyInjection;

public interface IEngineBuilder
{
    IServiceCollection Services { get; }
}
