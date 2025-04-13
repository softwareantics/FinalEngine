// <copyright file="OpenGLGPUResourceFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.OpenGL.Buffers;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.OpenGL.Pipeline;
using FinalEngine.Rendering.OpenGL.Textures;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using FinalEngine.Utilities;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = FinalEngine.Rendering.Textures.PixelFormat;

internal sealed class OpenGLGPUResourceFactory : IGPUResourceFactory
{
    private readonly IOpenGLInvoker invoker;

    private readonly IEnumMapper mapper;

    public OpenGLGPUResourceFactory(IOpenGLInvoker invoker, IEnumMapper mapper)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public ITextureCube CreateCubeTexture(
        TextureCubeDescription description,
        ITexture2D right,
        ITexture2D left,
        ITexture2D top,
        ITexture2D bottom,
        ITexture2D back,
        ITexture2D front,
        SizedFormat internalFormat = SizedFormat.Rgba8)
    {
        ArgumentNullException.ThrowIfNull(right, nameof(right));
        ArgumentNullException.ThrowIfNull(left, nameof(left));
        ArgumentNullException.ThrowIfNull(left, nameof(top));
        ArgumentNullException.ThrowIfNull(left, nameof(bottom));
        ArgumentNullException.ThrowIfNull(left, nameof(back));
        ArgumentNullException.ThrowIfNull(left, nameof(front));

        var cubeFaces = new List<ITexture2D>()
        {
            right, left, top, bottom, front, back,
        };

        return new OpenGLTextureCube(this.invoker, this.mapper, description, internalFormat, cubeFaces.Cast<IOpenGLTexture>().ToArray().AsReadOnly());
    }

    public IFrameBuffer CreateFrameBuffer(IReadOnlyCollection<ITexture2D>? colorTargets, ITexture2D? depthTarget = null)
    {
        if (depthTarget is not null and not IOpenGLTexture)
        {
            throw new ArgumentException($"The specified {nameof(depthTarget)} parameter is not of type {nameof(IOpenGLTexture)}.", nameof(depthTarget));
        }

        return new OpenGLFrameBuffer(this.invoker, colorTargets?.Cast<IOpenGLTexture>().ToList().AsReadOnly(), (IOpenGLTexture?)depthTarget);
    }

    public IIndexBuffer CreateIndexBuffer<T>(BufferUsageType type, IReadOnlyCollection<T> data, int sizeInBytes)
            where T : struct
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        return new OpenGLIndexBuffer<T>(this.invoker, this.mapper, this.mapper.Forward<BufferUsageHint>(type), data, sizeInBytes);
    }

    public IInputLayout CreateInputLayout(IReadOnlyCollection<InputElement> elements)
    {
        ArgumentNullException.ThrowIfNull(elements, nameof(elements));
        return new OpenGLInputLayout(this.invoker, this.mapper, elements);
    }

    public IShader CreateShader(PipelineTarget target, string sourceCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceCode, nameof(sourceCode));
        return new OpenGLShader(this.invoker, this.mapper, this.mapper.Forward<ShaderType>(target), sourceCode);
    }

    public IShaderProgram CreateShaderProgram(IReadOnlyCollection<IShader> shaders)
    {
        ArgumentNullException.ThrowIfNull(shaders, nameof(shaders));
        return new OpenGLShaderProgram(this.invoker, shaders.Cast<IOpenGLShader>().ToList().AsReadOnly());
    }

    public ITexture2D CreateTexture2D<T>(
        Texture2DDescription description,
        IReadOnlyCollection<T>? data,
        PixelFormat format = PixelFormat.Rgba,
        SizedFormat internalFormat = SizedFormat.Rgba8)
    {
        var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        IntPtr ptr = data == null ? IntPtr.Zero : handle.AddrOfPinnedObject();

        var result = new OpenGLTexture2D(this.invoker, this.mapper, description, format, internalFormat, ptr);

        handle.Free();

        return result;
    }

    public IVertexBuffer CreateVertexBuffer<T>(BufferUsageType type, IReadOnlyCollection<T> data, int sizeInBytes, int stride)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        return new OpenGLVertexBuffer<T>(this.invoker, this.mapper, this.mapper.Forward<BufferUsageHint>(type), data, sizeInBytes, stride);
    }
}
