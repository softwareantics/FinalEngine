// <copyright file="IServiceConfigurator.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting;

using Microsoft.Extensions.DependencyInjection;

public interface IServiceConfigurator
{
    void Configure(IServiceCollection services);
}
