// <copyright file="OpenGLPipeline.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Numerics;
using FinalEngine.Rendering.Invocation;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using OpenTK.Graphics.OpenGL4;

internal sealed class OpenGLPipeline : IPipeline
{
    private readonly IOpenGLInvoker invoker;

    private IOpenGLShaderProgram? boundProgram;

    private IOpenGLTexture? boundTexture;

    public OpenGLPipeline(IOpenGLInvoker invoker)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
    }

    public int MaxTextureSlots
    {
        get { return this.invoker.GetInteger(GetPName.MaxTextureImageUnits); }
    }

    public void SetShaderProgram(IShaderProgram program)
    {
        ArgumentNullException.ThrowIfNull(program);

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

    public void SetTexture(ITexture2D texture, int slot = 0)
    {
        ArgumentNullException.ThrowIfNull(texture);

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
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform1(location, value);
    }

    public void SetUniform(string name, float value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform1(location, value);
    }

    public void SetUniform(string name, double value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform1(location, value);
    }

    public void SetUniform(string name, bool value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform1(location, value ? 1 : 0);
    }

    public void SetUniform(string name, Vector2 value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform2(location, value.X, value.Y);
    }

    public void SetUniform(string name, Vector3 value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform3(location, value.X, value.Y, value.Z);
    }

    public void SetUniform(string name, Vector4 value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!this.TryGetUniformLocation(name, out int location))
        {
            return;
        }

        this.invoker.Uniform4(location, value.X, value.Y, value.Z, value.W);
    }

    public void SetUniform(string name, Matrix4x4 value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

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
