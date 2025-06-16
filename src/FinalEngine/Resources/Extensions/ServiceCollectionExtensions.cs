// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources.Extensions;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddResourceLoader<TResource, TResourceLoader>(this IServiceCollection services)
        where TResource : IResource
        where TResourceLoader : ResourceLoaderBase<TResource>
    {
        services.AddSingleton<ResourceLoaderBase<TResource>, TResourceLoader>();
        services.AddSingleton<IResourceLoader, TResourceLoader>();

        return services;
    }
}
