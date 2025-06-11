// <copyright file="IConfiguratorActivator.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting.Services.Activation;

using Microsoft.Extensions.DependencyInjection;

internal interface IConfiguratorActivator
{
    void ActivateAndConfigure(Type type, IServiceCollection services);
}
