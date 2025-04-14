// <copyright file="OpenGLPipeline.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL;

using System;
using System.Collections.Generic;
using System.Numerics;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.OpenGL.Buffers;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.OpenGL.Pipeline;
using FinalEngine.Rendering.OpenGL.Textures;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using OpenTK.Graphics.OpenGL4;

internal sealed class OpenGLPipeline : IPipeline
{
    private readonly IOpenGLInvoker invoker;

    private readonly Dictionary<string, string> nameToHeaderMap;

    private IOpenGLFrameBuffer? boundFrameBuffer;

    private IOpenGLShaderProgram? boundProgram;

    private IOpenGLTexture? boundTexture;

    private int defaultFrameBufferTarget;

    public OpenGLPipeline(IOpenGLInvoker invoker)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.nameToHeaderMap = [];
    }

    public int MaxTextureSlots
    {
        get { return this.invoker.GetInteger(GetPName.MaxTextureImageUnits); }
    }

    public void AddShaderHeader(string name, string content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(content, nameof(content));

        if (this.nameToHeaderMap.ContainsKey(name))
        {
            throw new ArgumentException($"A shader header with name: '{name}' has already been added to this {nameof(OpenGLPipeline)}");
        }

        this.nameToHeaderMap.Add(name, content);
    }

    public string GetShaderHeader(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (!this.nameToHeaderMap.TryGetValue(name, out string? content))
        {
            throw new ArgumentException($"A shader header with name: '{name}' has not been added to this {nameof(OpenGLPipeline)}");
        }

        return content;
    }

    public void SetDefaultFrameBufferTarget(int frameBuffer)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(frameBuffer, 0);
        this.defaultFrameBufferTarget = frameBuffer;
    }

    public void SetFrameBuffer(IFrameBuffer? frameBuffer)
    {
        if (this.boundFrameBuffer == frameBuffer)
        {
            return;
        }

        if (frameBuffer == null)
        {
            this.boundFrameBuffer = null;
            this.invoker.BindFramebuffer(FramebufferTarget.Framebuffer, this.defaultFrameBufferTarget);
            return;
        }

        if (frameBuffer is not IOpenGLFrameBuffer glFrameBuffer)
        {
            throw new ArgumentException($"The specified {nameof(frameBuffer)} parameter is not of type {nameof(IOpenGLFrameBuffer)}.", nameof(frameBuffer));
        }

        this.boundFrameBuffer = glFrameBuffer;
        this.boundFrameBuffer.Bind();
    }

    public void SetShaderProgram(IShaderProgram program)
    {
        ArgumentNullException.ThrowIfNull(program, nameof(program));

        if (this.boundProgram == program)
        {
            return;
        }

        if (program is not IOpenGLShaderProgram glProgram)
        {
            throw new ArgumentException($"The specified {nameof(program)} parameter is not of type {nameof(IOpenGLShaderProgram)}.", nameof(program));
        }

        this.boundProgram = glProgram;
        this.boundProgram.Bind();
    }

    public void SetTexture(ITexture texture, int slot = 0)
    {
        ArgumentNullException.ThrowIfNull(texture, nameof(texture));

        if (this.boundTexture == texture)
        {
            return;
        }

        if (texture is not IOpenGLTexture glTexture)
        {
            throw new ArgumentException($"The specified {nameof(texture)} parameter is not of type {nameof(IOpenGLTexture)}.", nameof(texture));
        }

        this.boundTexture = glTexture;
        this.boundTexture.Bind(slot);
    }

    public void SetUniform(string name, int value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform1(location, value);
    }

    public void SetUniform(string name, float value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform1(location, value);
    }

    public void SetUniform(string name, double value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform1(location, value);
    }

    public void SetUniform(string name, bool value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform1(location, value ? 1 : 0);
    }

    public void SetUniform(string name, Vector2 value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform2(location, value.X, value.Y);
    }

    public void SetUniform(string name, Vector3 value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform3(location, value.X, value.Y, value.Z);
    }

    public void SetUniform(string name, Vector4 value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform4(location, value.X, value.Y, value.Z, value.W);
    }

    public void SetUniform(string name, Matrix4x4 value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        float[] values =
        [
            value.M11,
            value.M12,
            value.M13,
            value.M14,
            value.M21,
            value.M22,
            value.M23,
            value.M24,
            value.M31,
            value.M32,
            value.M33,
            value.M34,
            value.M41,
            value.M42,
            value.M43,
            value.M44,
        ];

        this.invoker.UniformMatrix4(location, 1, false, values);
    }

    private bool TryGetUniformLocation(string name, out int location)
    {
        if (this.boundProgram == null)
        {
            location = 0;
            return false;
        }

        return this.boundProgram.TryGetUniformLocation(name, out location);
    }
}
