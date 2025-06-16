// <copyright file="IOpenGLInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Invocation;

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

internal interface IOpenGLInvoker
{
    void AttachShader(int program, int shader);

    void BindBuffer(BufferTarget target, int buffer);

    void BindFramebuffer(FramebufferTarget target, int framebuffers);

    void BindTexture(TextureTarget target, int texture);

    void BindTextureUnit(int unit, int texture);

    void BindVertexArray(int array);

    void BindVertexBuffer(int bindingindex, int buffer, nint offset, int stride);

    void BindVertexBuffers(int first, int count, int[] buffers, nint[] offsets, int[] strides);

    void BlendColor(Color color);

    void BlendEquation(BlendEquationMode mode);

    void BlendFunc(BlendingFactor sfactor, BlendingFactor dfactor);

    void Cap(EnableCap cap, bool value);

    FramebufferStatus CheckNamedFramebufferStatus(int framebuffer, FramebufferTarget target);

    void Clear(ClearBufferMask mask);

    void ClearColor(Color color);

    void ClearDepth(float depth);

    void ClearStencil(int s);

    void CompileShader(int shader);

    void CopyImageSubData(
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
        int srcDepth);

    int CreateBuffer();

    int CreateFramebuffer();

    int CreateProgram();

    int CreateShader(ShaderType type);

    int CreateTexture(TextureTarget target);

    void CullFace(CullFaceMode mode);

    void Debug();

    void DeleteBuffer(int buffers);

    void DeleteFramebuffer(int framebuffers);

    void DeleteProgram(int program);

    void DeleteShader(int shader);

    void DeleteTexture(int textures);

    void DeleteVertexArray(int arrays);

    void DepthFunc(DepthFunction func);

    void DepthMask(bool flag);

    void DepthRange(float n, float f);

    void Disable(EnableCap cap);

    void DisableVertexAttribArray(int index);

    void DrawElements(PrimitiveType mode, int count, DrawElementsType type, int indices);

    void Enable(EnableCap cap);

    void EnableVertexAttribArray(int index);

    void FrontFace(FrontFaceDirection mode);

    void GenerateTextureMipmap(int texture);

    int GenVertexArray();

    void GetInteger(GetIndexedPName target, int index, int[] data);

    int GetInteger(GetPName pname);

    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Must Match API")]
    void GetProgram(int program, GetProgramParameterName pname, out int @params);

    string GetProgramInfoLog(int program);

    string GetShaderInfoLog(int shader);

    int GetUniformLocation(int program, string name);

    void LinkProgram(int program);

    void LoadBindings(IBindingsContext context);

    void NamedBufferData<T2>(int buffer, int size, T2[] data, BufferUsageHint usage)
        where T2 : struct;

    void NamedBufferSubData<T3>(int buffer, nint offset, int size, T3[] data)
        where T3 : struct;

    void NamedFramebufferDrawBuffer(int framebuffer, DrawBufferMode buf);

    void NamedFramebufferReadBuffer(int framebuffer, ReadBufferMode buf);

    void NamedFramebufferTexture(int framebuffer, FramebufferAttachment attachment, int texture, int level);

    void PolygonMode(MaterialFace face, PolygonMode mode);

    void Scissor(int x, int y, int width, int height);

    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Must Match API")]
    [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Must Much API")]
    void ShaderSource(int shader, string @string);

    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Must Match API")]
    void StencilFunc(StencilFunction func, int @ref, int mask);

    void StencilMask(int mask);

    void StencilOp(StencilOp fail, StencilOp zfail, StencilOp zpass);

    void TextureParameter(int texture, TextureParameterName pname, int param);

    void TextureStorage2D(int texture, int levels, SizedInternalFormat internalFormat, int width, int height);

    void TextureSubImage2D(int texture, int level, int xoffset, int yoffset, int width, int height, PixelFormat format, PixelType type, nint pixels);

    void Uniform1(int location, int x);

    void Uniform1(int location, float v0);

    void Uniform1(int location, double x);

    void Uniform2(int location, float v0, float v1);

    void Uniform3(int location, float v0, float v1, float v2);

    void Uniform4(int location, float v0, float v1, float v2, float v3);

    void UniformMatrix4(int location, int count, bool transpose, float[] value);

    void UseProgram(int program);

    void ValidateProgram(int program);

    void VertexAttribBinding(int attribindex, int bindingindex);

    void VertexAttribFormat(int attribindex, int size, VertexAttribType type, bool normalized, int relativeoffset);

    void Viewport(Rectangle rectangle);
}
