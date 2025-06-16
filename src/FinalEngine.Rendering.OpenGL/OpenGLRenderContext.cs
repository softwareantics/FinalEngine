// <copyright file="OpenGLRenderContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using FinalEngine.Rendering.Exceptions;
using FinalEngine.Rendering.Invocation;
using OpenTK;
using OpenTK.Windowing.Common;

internal sealed class OpenGLRenderContext : IRenderContext
{
    private readonly IGraphicsContext context;

    public OpenGLRenderContext(IOpenGLInvoker invoker, IBindingsContext bindings, IGraphicsContext context)
    {
        ArgumentNullException.ThrowIfNull(bindings);
        ArgumentNullException.ThrowIfNull(invoker);

        this.context = context ?? throw new ArgumentNullException(nameof(context));

        context.MakeCurrent();
        invoker.LoadBindings(bindings);
    }

    public void SwapBuffers()
    {
        if (!this.context.IsCurrent)
        {
            throw new RenderContextException($"This {nameof(OpenGLRenderContext)} is not current on the calling thread.");
        }

        this.context.SwapBuffers();
    }
}
