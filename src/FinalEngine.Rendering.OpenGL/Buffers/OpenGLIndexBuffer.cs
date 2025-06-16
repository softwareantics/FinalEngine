// <copyright file="OpenGLIndexBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AutoMapper;
using FinalEngine.Rendering.Invocation;
using OpenTK.Graphics.OpenGL4;

internal sealed class OpenGLIndexBuffer<T> : IOpenGLIndexBuffer
    where T : struct
{
    private readonly IOpenGLInvoker invoker;

    private bool isDisposed;

    private int rendererID;

    public OpenGLIndexBuffer(IOpenGLInvoker invoker, IMapper mapper, BufferUsageType type, IReadOnlyCollection<T> data, int sizeInBytes)
    {
        ArgumentNullException.ThrowIfNull(mapper);
        ArgumentNullException.ThrowIfNull(data);

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        this.Type = type;
        this.Length = data.Count;

        this.rendererID = invoker.CreateBuffer();
        invoker.NamedBufferData(this.rendererID, sizeInBytes, data.ToArray(), mapper.Map<BufferUsageHint>(type));
    }

    ~OpenGLIndexBuffer()
    {
        this.Dispose(false);
    }

    public int Length { get; private set; }

    public BufferUsageType Type { get; }

    public void Bind()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenGLIndexBuffer<T>));
        this.invoker.BindBuffer(BufferTarget.ElementArrayBuffer, this.rendererID);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Update<TData>(IReadOnlyCollection<TData> data)
        where TData : struct
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenGLIndexBuffer<T>));
        ArgumentNullException.ThrowIfNull(data);

        this.Length = data.Count;
        this.invoker.NamedBufferSubData(this.rendererID, nint.Zero, this.Length * Marshal.SizeOf<TData>(), data.ToArray());
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
