// <copyright file="RenderContextException.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Exceptions;

using System;

[Serializable]
public sealed class RenderContextException : Exception
{
    public RenderContextException()
    {
    }

    public RenderContextException(string? message)
        : base(message)
    {
    }

    public RenderContextException(string? message, Exception? inner)
        : base(message, inner)
    {
    }
}
