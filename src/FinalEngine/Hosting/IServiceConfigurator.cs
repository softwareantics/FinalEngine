// <copyright file="IServiceConfigurator.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting;

public interface IServiceConfigurator
{
    void ConfigureServices(IEngineBuilder builder);
}
