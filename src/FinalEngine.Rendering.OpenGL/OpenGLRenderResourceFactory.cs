// <copyright file="OpenGLGPUResourceFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AutoMapper;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Invocation;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using PixelFormat = Textures.PixelFormat;

internal sealed class OpenGLRenderResourceFactory : IRenderResourceFactory
{
    private readonly IOpenGLInvoker invoker;

    private readonly IMapper mapper;

    public OpenGLRenderResourceFactory(IOpenGLInvoker invoker, IMapper mapper)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public IIndexBuffer CreateIndexBuffer<T>(BufferUsageType type, IReadOnlyCollection<T> data, int sizeInBytes)
            where T : struct
    {
        ArgumentNullException.ThrowIfNull(data);
        return new OpenGLIndexBuffer<T>(this.invoker, this.mapper, type, data, sizeInBytes);
    }

    public IInputLayout CreateInputLayout(IReadOnlyCollection<InputElement> elements)
    {
        ArgumentNullException.ThrowIfNull(elements);
        return new OpenGLInputLayout(this.invoker, this.mapper, elements);
    }

    public IShader CreateShader(PipelineTarget target, string sourceCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceCode);
        return new OpenGLShader(this.invoker, this.mapper, target, sourceCode);
    }

    public IShaderProgram CreateShaderProgram(IReadOnlyCollection<IShader> shaders)
    {
        ArgumentNullException.ThrowIfNull(shaders);
        return new OpenGLShaderProgram(this.invoker, shaders.Cast<IOpenGLShader>().ToList().AsReadOnly());
    }

    public ITexture2D CreateTexture2D<T>(
        Texture2DDescription description,
        IReadOnlyCollection<T>? data,
        PixelFormat format = PixelFormat.Rgba,
        SizedFormat internalFormat = SizedFormat.Rgba8)
    {
        var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        nint ptr = data == null ? nint.Zero : handle.AddrOfPinnedObject();

        var result = new OpenGLTexture2D(this.invoker, this.mapper, description, format, internalFormat, ptr);

        handle.Free();

        return result;
    }

    public IVertexBuffer CreateVertexBuffer<T>(BufferUsageType type, IReadOnlyCollection<T> data, int sizeInBytes, int stride)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(data);
        return new OpenGLVertexBuffer<T>(this.invoker, this.mapper, type, data, sizeInBytes, stride);
    }
}
