// <copyright file="OpenGLInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Invocation;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using FinalEngine.Rendering.Exceptions;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

[ExcludeFromCodeCoverage]
internal sealed class OpenGLInvoker : IOpenGLInvoker
{
    private static readonly DebugProc DebugProcCallback = DebugCallback;

    private static GCHandle debugProcCallbackHandle;

    ~OpenGLInvoker()
    {
        if (debugProcCallbackHandle != default)
        {
            debugProcCallbackHandle.Free();
        }
    }

    public void AttachShader(int program, int shader)
    {
        GL.AttachShader(program, shader);
    }

    public void BindBuffer(BufferTarget target, int buffer)
    {
        GL.BindBuffer(target, buffer);
    }

    public void BindFramebuffer(FramebufferTarget target, int framebuffers)
    {
        GL.BindFramebuffer(target, framebuffers);
    }

    public void BindTexture(TextureTarget target, int texture)
    {
        GL.BindTexture(target, texture);
    }

    public void BindTextureUnit(int unit, int texture)
    {
        GL.BindTextureUnit(unit, texture);
    }

    public void BindVertexArray(int array)
    {
        GL.BindVertexArray(array);
    }

    public void BindVertexBuffer(int bindingindex, int buffer, nint offset, int stride)
    {
        GL.BindVertexBuffer(bindingindex, buffer, offset, stride);
    }

    public void BindVertexBuffers(int first, int count, int[] buffers, nint[] offsets, int[] strides)
    {
        GL.BindVertexBuffers(first, count, buffers, offsets, strides);
    }

    public void BlendColor(Color color)
    {
        GL.BlendColor(color);
    }

    public void BlendEquation(BlendEquationMode mode)
    {
        GL.BlendEquation(mode);
    }

    public void BlendFunc(BlendingFactor sfactor, BlendingFactor dfactor)
    {
        GL.BlendFunc(sfactor, dfactor);
    }

    public void Cap(EnableCap cap, bool value)
    {
        if (value)
        {
            this.Enable(cap);
        }
        else
        {
            this.Disable(cap);
        }
    }

    public FramebufferStatus CheckNamedFramebufferStatus(int framebuffer, FramebufferTarget target)
    {
        return GL.CheckNamedFramebufferStatus(framebuffer, target);
    }

    public void Clear(ClearBufferMask mask)
    {
        GL.Clear(mask);
    }

    public void ClearColor(Color color)
    {
        GL.ClearColor(color);
    }

    public void ClearDepth(float depth)
    {
        GL.ClearDepth(depth);
    }

    public void ClearStencil(int s)
    {
        GL.ClearStencil(s);
    }

    public void CompileShader(int shader)
    {
        GL.CompileShader(shader);
    }

    public void CopyImageSubData(
        int srcName,
        ImageTarget srcTarget,
        int srcLevel,
        int srcX,
        int srcY,
        int srcZ,
        int dstName,
        ImageTarget dstTarget,
        int dstLevel,
        int dstX,
        int dstY,
        int dstZ,
        int srcWidth,
        int srcHeight,
        int srcDepth)
    {
        GL.CopyImageSubData(srcName, srcTarget, srcLevel, srcX, srcY, srcZ, dstName, dstTarget, dstLevel, dstX, dstY, dstZ, srcWidth, srcHeight, srcDepth);
    }

    public int CreateBuffer()
    {
        GL.CreateBuffers(1, out int result);

        return result;
    }

    public int CreateFramebuffer()
    {
        GL.CreateFramebuffers(1, out int framebuffers);
        return framebuffers;
    }

    public int CreateProgram()
    {
        return GL.CreateProgram();
    }

    public int CreateShader(ShaderType type)
    {
        return GL.CreateShader(type);
    }

    public int CreateTexture(TextureTarget target)
    {
        GL.CreateTextures(target, 1, out int result);

        return result;
    }

    public void CullFace(CullFaceMode mode)
    {
        GL.CullFace(mode);
    }

    public void Debug()
    {
        debugProcCallbackHandle = GCHandle.Alloc(DebugProcCallback);

        GL.DebugMessageCallback(DebugProcCallback, nint.Zero);

        GL.Enable(EnableCap.DebugOutput);
        GL.Enable(EnableCap.DebugOutputSynchronous);

        Console.WriteLine($"Vendor: {GL.GetString(StringName.Vendor)}");
        Console.WriteLine($"Version: {GL.GetString(StringName.Version)}");
        Console.WriteLine($"Renderer: {GL.GetString(StringName.Renderer)}");
        Console.WriteLine($"Shader Version: {GL.GetString(StringName.ShadingLanguageVersion)}");
    }

    public void DeleteBuffer(int buffers)
    {
        GL.DeleteBuffer(buffers);
    }

    public void DeleteFramebuffer(int framebuffers)
    {
        GL.DeleteFramebuffer(framebuffers);
    }

    public void DeleteProgram(int program)
    {
        GL.DeleteProgram(program);
    }

    public void DeleteShader(int shader)
    {
        GL.DeleteShader(shader);
    }

    public void DeleteTexture(int textures)
    {
        GL.DeleteTexture(textures);
    }

    public void DeleteVertexArray(int arrays)
    {
        GL.DeleteVertexArray(arrays);
    }

    public void DepthFunc(DepthFunction func)
    {
        GL.DepthFunc(func);
    }

    public void DepthMask(bool flag)
    {
        GL.DepthMask(flag);
    }

    public void DepthRange(float n, float f)
    {
        GL.DepthRange(n, f);
    }

    public void Disable(EnableCap cap)
    {
        GL.Disable(cap);
    }

    public void DisableVertexAttribArray(int index)
    {
        GL.DisableVertexAttribArray(index);
    }

    public void DrawElements(PrimitiveType mode, int count, DrawElementsType type, int indices)
    {
        GL.DrawElements(mode, count, type, indices);
    }

    public void Enable(EnableCap cap)
    {
        GL.Enable(cap);
    }

    public void EnableVertexAttribArray(int index)
    {
        GL.EnableVertexAttribArray(index);
    }

    public void FrontFace(FrontFaceDirection mode)
    {
        GL.FrontFace(mode);
    }

    public void GenerateTextureMipmap(int texture)
    {
        GL.GenerateTextureMipmap(texture);
    }

    public int GenVertexArray()
    {
        return GL.GenVertexArray();
    }

    public void GetInteger(GetIndexedPName target, int index, int[] data)
    {
        GL.GetInteger(target, index, data);
    }

    public int GetInteger(GetPName pname)
    {
        return GL.GetInteger(pname);
    }

    public void GetProgram(int program, GetProgramParameterName pname, out int @params)
    {
        GL.GetProgram(program, pname, out @params);
    }

    public string GetProgramInfoLog(int program)
    {
        return GL.GetProgramInfoLog(program);
    }

    public string GetShaderInfoLog(int shader)
    {
        return GL.GetShaderInfoLog(shader);
    }

    public int GetUniformLocation(int program, string name)
    {
        return GL.GetUniformLocation(program, name);
    }

    public void LinkProgram(int program)
    {
        GL.LinkProgram(program);
    }

    public void LoadBindings(IBindingsContext context)
    {
        GL.LoadBindings(context);
    }

    public void NamedBufferData<T2>(int buffer, int size, T2[] data, BufferUsageHint usage)
        where T2 : struct
    {
        GL.NamedBufferData(buffer, size, data, usage);
    }

    public void NamedBufferSubData<T3>(int buffer, nint offset, int size, T3[] data)
        where T3 : struct
    {
        GL.NamedBufferSubData(buffer, offset, size, data);
    }

    public void NamedFramebufferDrawBuffer(int framebuffer, DrawBufferMode buf)
    {
        GL.NamedFramebufferDrawBuffer(framebuffer, buf);
    }

    public void NamedFramebufferReadBuffer(int framebuffer, ReadBufferMode buf)
    {
        GL.NamedFramebufferReadBuffer(framebuffer, buf);
    }

    public void NamedFramebufferTexture(int framebuffer, FramebufferAttachment attachment, int texture, int level)
    {
        GL.NamedFramebufferTexture(framebuffer, attachment, texture, level);
    }

    public void PolygonMode(MaterialFace face, PolygonMode mode)
    {
        GL.PolygonMode(face, mode);
    }

    public void Scissor(int x, int y, int width, int height)
    {
        GL.Scissor(x, y, width, height);
    }

    public void ShaderSource(int shader, string @string)
    {
        GL.ShaderSource(shader, @string);
    }

    public void StencilFunc(StencilFunction func, int @ref, int mask)
    {
        GL.StencilFunc(func, @ref, mask);
    }

    public void StencilMask(int mask)
    {
        GL.StencilMask(mask);
    }

    public void StencilOp(StencilOp fail, StencilOp zfail, StencilOp zpass)
    {
        GL.StencilOp(fail, zfail, zpass);
    }

    public void TextureParameter(int texture, TextureParameterName pname, int param)
    {
        GL.TextureParameter(texture, pname, param);
    }

    public void TextureStorage2D(int texture, int levels, SizedInternalFormat internalFormat, int width, int height)
    {
        GL.TextureStorage2D(texture, levels, internalFormat, width, height);
    }

    public void TextureSubImage2D(int texture, int level, int xoffset, int yoffset, int width, int height, PixelFormat format, PixelType type, nint pixels)
    {
        GL.TextureSubImage2D(texture, level, xoffset, yoffset, width, height, format, type, pixels);
    }

    public void Uniform1(int location, int x)
    {
        GL.Uniform1(location, x);
    }

    public void Uniform1(int location, float v0)
    {
        GL.Uniform1(location, v0);
    }

    public void Uniform1(int location, double x)
    {
        GL.Uniform1(location, x);
    }

    public void Uniform2(int location, float v0, float v1)
    {
        GL.Uniform2(location, v0, v1);
    }

    public void Uniform3(int location, float v0, float v1, float v2)
    {
        GL.Uniform3(location, v0, v1, v2);
    }

    public void Uniform4(int location, float v0, float v1, float v2, float v3)
    {
        GL.Uniform4(location, v0, v1, v2, v3);
    }

    public void UniformMatrix4(int location, int count, bool transpose, float[] value)
    {
        GL.UniformMatrix4(location, count, transpose, value);
    }

    public void UseProgram(int program)
    {
        GL.UseProgram(program);
    }

    public void ValidateProgram(int program)
    {
        GL.ValidateProgram(program);
    }

    public void VertexAttribBinding(int attribindex, int bindingindex)
    {
        GL.VertexAttribBinding(attribindex, bindingindex);
    }

    public void VertexAttribFormat(int attribindex, int size, VertexAttribType type, bool normalized, int relativeoffset)
    {
        GL.VertexAttribFormat(attribindex, size, type, normalized, relativeoffset);
    }

    public void Viewport(Rectangle rectangle)
    {
        GL.Viewport(rectangle);
    }

    private static void DebugCallback(
        DebugSource source,
        DebugType type,
        int id,
        DebugSeverity severity,
        int length,
        nint message,
        nint userParam)
    {
        string messageString = Marshal.PtrToStringAnsi(message, length);

        if (type == DebugType.DebugTypeError)
        {
            throw new RenderContextException($"{severity} {type} | {messageString}");
        }
    }
}
