// <copyright file="OpenGLRenderPipeline.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using FinalEngine.Rendering.Invocation;

internal sealed class OpenGLRenderPipeline : IRenderPipeline
{
    private readonly IOpenGLInvoker invoker;

    private bool isDisposed;

    private int vao;

    public OpenGLRenderPipeline(IOpenGLInvoker invoker)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
    }

    ~OpenGLRenderPipeline()
    {
        this.Dispose(false);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Initialize()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenGLRenderPipeline));

#if DEBUG
        this.invoker.Debug();
#endif

        this.vao = this.invoker.GenVertexArray();
        this.invoker.BindVertexArray(this.vao);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.vao != -1)
        {
            this.invoker.DeleteVertexArray(this.vao);
            this.vao = -1;
        }

        this.isDisposed = true;
    }
}
