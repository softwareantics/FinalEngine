// <copyright file="OpenGLShaderProgram.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Pipeline;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.Invocation;

internal sealed class OpenGLShaderProgram : IOpenGLShaderProgram
{
    private readonly IOpenGLInvoker invoker;

    private readonly Dictionary<string, int> uniformNameToLocationMap;

    private bool isDisposed;

    private int rendererID;

    public OpenGLShaderProgram(IOpenGLInvoker invoker, IReadOnlyCollection<IOpenGLShader> shaders)
    {
        ArgumentNullException.ThrowIfNull(shaders);

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.uniformNameToLocationMap = [];

        this.rendererID = this.invoker.CreateProgram();

        foreach (var shader in shaders)
        {
            if (shader == null)
            {
                continue;
            }

            shader.Attach(this.rendererID);
        }

        this.invoker.LinkProgram(this.rendererID);
        this.invoker.ValidateProgram(this.rendererID);

        string? log = this.invoker.GetProgramInfoLog(this.rendererID);

        if (!string.IsNullOrWhiteSpace(log))
        {
            throw new ProgramLinkingException($"The {nameof(OpenGLShaderProgram)} failed to link.", log);
        }
    }

    ~OpenGLShaderProgram()
    {
        this.Dispose(false);
    }

    public void Bind()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenGLShaderProgram));
        this.invoker.UseProgram(this.rendererID);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public bool TryGetUniformLocation(string name, out int location)
    {
        if (!this.uniformNameToLocationMap.TryGetValue(name, out location))
        {
            location = this.invoker.GetUniformLocation(this.rendererID, name);

            if (location == -1)
            {
                return false;
            }

            this.uniformNameToLocationMap.Add(name, location);
        }

        return true;
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.rendererID != -1)
        {
            this.invoker.DeleteProgram(this.rendererID);
            this.rendererID = -1;
        }

        this.isDisposed = true;
    }
}
