// <copyright file="ProgramLinkingException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Exceptions;

using System;

[Serializable]
public sealed class ProgramLinkingException : Exception
{
    public ProgramLinkingException()
    {
    }

    public ProgramLinkingException(string? message)
        : base(message)
    {
    }

    public ProgramLinkingException(string? message, Exception? inner)
        : base(message, inner)
    {
    }

    public ProgramLinkingException(string? message, string errorLog)
        : base(message)
    {
        this.ErrorLog = errorLog;
    }

    public string? ErrorLog { get; }
}
