// <copyright file="UniformNotLocatedException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Exceptions;

using System;

[Serializable]
public sealed class UniformNotLocatedException : Exception
{
    public UniformNotLocatedException()
    {
    }

    public UniformNotLocatedException(string? message)
        : base(message)
    {
    }

    public UniformNotLocatedException(string? message, Exception? inner)
        : base(message, inner)
    {
    }

    public UniformNotLocatedException(string? message, string uniformName)
        : base(message)
    {
        this.UniformName = uniformName;
    }

    public string? UniformName { get; }
}
