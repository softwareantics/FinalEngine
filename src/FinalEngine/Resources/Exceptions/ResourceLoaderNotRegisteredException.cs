// <copyright file="ResourceLoaderNotRegisteredException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources.Exceptions;

using System;

public sealed class ResourceLoaderNotRegisteredException : Exception
{
    public ResourceLoaderNotRegisteredException()
        : base("Resource Loader not registered.")
    {
    }

    public ResourceLoaderNotRegisteredException(string? message)
        : base(message)
    {
    }

    public ResourceLoaderNotRegisteredException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
