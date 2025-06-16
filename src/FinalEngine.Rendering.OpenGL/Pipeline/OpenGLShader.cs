// <copyright file="OpenGLShader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Pipeline;

using System;
using AutoMapper;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.Invocation;
using TKShaderType = OpenTK.Graphics.OpenGL4.ShaderType;

internal sealed class OpenGLShader : IOpenGLShader
{
    private readonly IOpenGLInvoker invoker;

    private bool isDisposed;

    private int rendererID;

    public OpenGLShader(IOpenGLInvoker invoker, IMapper mapper, PipelineTarget target, string sourceCode)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceCode);

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        this.EntryPoint = target;

        this.rendererID = invoker.CreateShader(mapper.Map<TKShaderType>(target));
        invoker.ShaderSource(this.rendererID, sourceCode);
        invoker.CompileShader(this.rendererID);

        string? log = invoker.GetShaderInfoLog(this.rendererID);

        if (!string.IsNullOrWhiteSpace(log))
        {
            throw new ShaderCompilationErrorException($"The {nameof(OpenGLShader)} failed to compile.", log);
        }
    }

    ~OpenGLShader()
    {
        this.Dispose(false);
    }

    public PipelineTarget EntryPoint { get; }

    public void Attach(int program)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenGLShader));
        this.invoker.AttachShader(program, this.rendererID);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.rendererID != -1)
        {
            this.invoker.DeleteShader(this.rendererID);
            this.rendererID = -1;
        }

        this.isDisposed = true;
    }
}
