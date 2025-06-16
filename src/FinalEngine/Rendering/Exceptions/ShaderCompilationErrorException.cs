// <copyright file="ShaderCompilationErrorException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Exceptions;

using System;

[Serializable]
public sealed class ShaderCompilationErrorException : Exception
{
    public ShaderCompilationErrorException()
    {
    }

    public ShaderCompilationErrorException(string? message)
        : base(message)
    {
    }

    public ShaderCompilationErrorException(string? message, Exception? inner)
        : base(message, inner)
    {
    }

    public ShaderCompilationErrorException(string? message, string errorLog)
        : base(message)
    {
        this.ErrorLog = errorLog;
    }

    public string? ErrorLog { get; }
}
