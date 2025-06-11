// <copyright file="ConfiguratorActivator.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Hosting.Services.Activation;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

internal sealed class ConfiguratorActivator : IConfiguratorActivator
{
    private readonly ILogger<ConfiguratorActivator> logger;

    public ConfiguratorActivator(ILogger<ConfiguratorActivator> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "Required to ensure parameterless constructor.")]
    public void ActivateAndConfigure(Type type, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(services);

        if (!this.IsParameterlessConstructor(type))
        {
            return;
        }

        try
        {
            if (Activator.CreateInstance(type, nonPublic: true) is IServiceConfigurator configurator)
            {
                this.logger.LogDebug("Configuring services via {Configurator}", type.FullName);
                configurator.Configure(services);
            }
        }
        catch (TargetInvocationException ex)
        {
            this.logger.LogWarning(ex, "The constructor being called for {Type} through an exception.", type.FullName);
        }
        catch (InvalidComObjectException ex)
        {
            this.logger.LogWarning(ex, "Failed to instantiate '{Type}'.", type.FullName);
        }
        catch (COMException ex)
        {
            this.logger.LogWarning(ex, "Failed to instantiate '{Type}'.", type.FullName);
        }
        catch (MethodAccessException ex)
        {
            this.logger.LogWarning(ex, "Failed to instantiate '{Type}'. The caller doesn't have permission to call the constructor.", type.FullName);
        }
        catch (MemberAccessException ex)
        {
            this.logger.LogWarning(ex, "Access issue encountered when instantiating '{Type}'.", type.FullName);
        }
    }

    private bool IsParameterlessConstructor(Type type)
    {
        // Ensure a parameterless constructor exists (either public or non-public).
        var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, Type.EmptyTypes, null);

        if (constructor is null)
        {
            this.logger.LogWarning("The service configurator was found: '{Type}' but no parameterless constructor could be located.", type.FullName);
            return false;
        }

        return true;
    }
}
