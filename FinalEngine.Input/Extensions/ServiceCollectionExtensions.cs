// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Extensions;

using System;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInput(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<IKeyboard, Keyboard>();
        services.AddSingleton<IMouse, Mouse>();
        services.AddSingleton<IInputDriver, InputDriver>();

        return services;
    }
}
