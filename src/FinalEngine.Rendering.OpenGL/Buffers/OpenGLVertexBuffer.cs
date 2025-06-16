// <copyright file="OpenGLVertexBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AutoMapper;
using FinalEngine.Rendering.Invocation;
using TKBufferUsageHint = OpenTK.Graphics.OpenGL4.BufferUsageHint;

internal sealed class OpenGLVertexBuffer<T> : IOpenGLVertexBuffer
    where T : struct
{
    private readonly IOpenGLInvoker invoker;

    private bool isDisposed;

    private int rendererID;

    public OpenGLVertexBuffer(IOpenGLInvoker invoker, IMapper mapper, BufferUsageType type, IReadOnlyCollection<T> data, int sizeInBytes, int stride)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        ArgumentNullException.ThrowIfNull(data);

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        this.Type = type;
        this.Stride = stride;

        this.rendererID = invoker.CreateBuffer();
        invoker.NamedBufferData(this.rendererID, sizeInBytes, data.ToArray(), mapper.Map<TKBufferUsageHint>(type));
    }

    ~OpenGLVertexBuffer()
    {
        this.Dispose(false);
    }

    public int Stride { get; private set; }

    public BufferUsageType Type { get; }

    public void Bind()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenGLVertexBuffer<T>));
        this.invoker.BindVertexBuffer(0, this.rendererID, nint.Zero, this.Stride);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Update<TData>(IReadOnlyCollection<TData> data, int stride)
        where TData : struct
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenGLVertexBuffer<T>));
        ArgumentNullException.ThrowIfNull(data);

        this.Stride = stride;

        this.invoker.NamedBufferSubData(this.rendererID, nint.Zero, data.Count * Marshal.SizeOf<TData>(), data.ToArray());
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.rendererID != -1)
        {
            this.invoker.DeleteBuffer(this.rendererID);
            this.rendererID = -1;
        }

        this.isDisposed = true;
    }
}
