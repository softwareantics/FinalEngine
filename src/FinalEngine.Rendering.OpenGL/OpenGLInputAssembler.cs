// <copyright file="OpenGLInputAssembler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;

internal sealed class OpenGLInputAssembler : IInputAssembler
{
    private IOpenGLIndexBuffer? boundIndexBuffer;

    private IOpenGLInputLayout? boundLayout;

    private IOpenGLVertexBuffer? boundVertexBuffer;

    public void SetIndexBuffer(IIndexBuffer buffer)
    {
        ArgumentNullException.ThrowIfNull(buffer, nameof(buffer));

        if (this.boundIndexBuffer == buffer)
        {
            return;
        }

        if (buffer is not IOpenGLIndexBuffer glIndexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLIndexBuffer)}.", nameof(buffer));
        }

        this.boundIndexBuffer = glIndexBuffer;
        this.boundIndexBuffer.Bind();
    }

    public void SetInputLayout(IInputLayout layout)
    {
        ArgumentNullException.ThrowIfNull(layout, nameof(layout));

        if (this.boundLayout == layout)
        {
            return;
        }

        if (layout is not IOpenGLInputLayout glInputLayout)
        {
            throw new ArgumentException($"The specified {nameof(layout)} parameter is not of type {nameof(IOpenGLInputLayout)}.", nameof(layout));
        }

        this.boundLayout = glInputLayout;
        this.boundLayout.Bind();
    }

    public void SetVertexBuffer(IVertexBuffer buffer)
    {
        ArgumentNullException.ThrowIfNull(buffer, nameof(buffer));

        if (this.boundVertexBuffer == buffer)
        {
            return;
        }

        if (buffer is not IOpenGLVertexBuffer glVertexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLVertexBuffer)}.", nameof(buffer));
        }

        this.boundVertexBuffer = glVertexBuffer;
        this.boundVertexBuffer.Bind();
    }

    public void UpdateIndexBuffer<T>(IIndexBuffer buffer, IReadOnlyCollection<T> data)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(buffer, nameof(buffer));
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        if (buffer is not IOpenGLIndexBuffer glIndexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLVertexBuffer)}.", nameof(buffer));
        }

        glIndexBuffer.Update(data);
    }

    public void UpdateVertexBuffer<T>(IVertexBuffer buffer, IReadOnlyCollection<T> data, int stride)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(buffer, nameof(buffer));
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        if (buffer is not IOpenGLVertexBuffer glVertexBuffer)
        {
            throw new ArgumentException($"The specified {nameof(buffer)} parameter is not of type {nameof(IOpenGLVertexBuffer)}.", nameof(buffer));
        }

        glVertexBuffer.Update(data, stride);
    }
}
