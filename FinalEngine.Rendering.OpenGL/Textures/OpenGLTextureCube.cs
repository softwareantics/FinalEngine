// <copyright file="OpenGLTextureCube.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Textures;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.OpenGL.Invocation;
using FinalEngine.Rendering.Textures;
using FinalEngine.Utilities;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = FinalEngine.Rendering.Textures.PixelFormat;
using TKTextureWrapMode = OpenTK.Graphics.OpenGL4.TextureWrapMode;

internal sealed class OpenGLTextureCube : ITextureCube, IOpenGLTexture, IDisposable
{
    private readonly IOpenGLInvoker invoker;

    private bool isDisposed;

    private int rendererID;

    public OpenGLTextureCube(
        IOpenGLInvoker invoker,
        IEnumMapper mapper,
        TextureCubeDescription description,
        SizedFormat internalFormat,
        IReadOnlyList<IOpenGLTexture> data)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        if (data.Count != 6)
        {
            throw new ArgumentOutOfRangeException(nameof(data), data.Count, $"An {nameof(OpenGLTextureCube)} must have six textures for each side.");
        }

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.rendererID = invoker.CreateTexture(TextureTarget.TextureCubeMap);

        this.InternalFormat = internalFormat;
        this.Description = description;

        int mipmap = (int)Math.Ceiling(Math.Max(Math.Log2(description.Width + 1), Math.Log2(description.Height + 1)));
        invoker.TextureStorage2D(this.rendererID, mipmap, mapper.Forward<SizedInternalFormat>(this.InternalFormat), description.Width, description.Height);

        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureMinFilter, (int)mapper.Forward<TextureMinFilter>(description.MinFilter));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureMagFilter, (int)mapper.Forward<TextureMagFilter>(description.MagFilter));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureWrapS, (int)mapper.Forward<TKTextureWrapMode>(description.WrapS));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureWrapT, (int)mapper.Forward<TKTextureWrapMode>(description.WrapT));
        invoker.TextureParameter(this.rendererID, TextureParameterName.TextureWrapR, (int)mapper.Forward<TKTextureWrapMode>(description.WrapR));

        for (int i = 0; i < data.Count; i++)
        {
            var texture = data[i];

            texture.CopyImageSubData(
                srcLevel: 0,
                srcX: 0,
                srcY: 0,
                srcZ: 0,
                dstName: this.rendererID,
                dstTarget: ImageTarget.TextureCubeMap,
                dstLevel: 0,
                dstX: 0,
                dstY: 0,
                dstZ: i);

            if (description.GenerateMipmaps)
            {
                invoker.GenerateTextureMipmap(this.rendererID);
            }
        }
    }

    ~OpenGLTextureCube()
    {
        this.Dispose(false);
    }

    public TextureCubeDescription Description { get; }

    public PixelFormat Format { get; }

    public SizedFormat InternalFormat { get; }

    public void Attach(FramebufferAttachment type, int framebuffer)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        throw new NotImplementedException();
    }

    public void Bind(int unit)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        this.invoker.BindTextureUnit(unit, this.rendererID);
    }

    public void CopyImageSubData(
    int srcLevel,
    int srcX,
    int srcY,
    int srcZ,
    int dstName,
    ImageTarget dstTarget,
    int dstLevel,
    int dstX,
    int dstY,
    int dstZ)
    {
        this.invoker.CopyImageSubData(
            this.rendererID,
            ImageTarget.TextureCubeMap,
            srcLevel,
            srcX,
            srcY,
            srcZ,
            dstName,
            dstTarget,
            dstLevel,
            dstX,
            dstY,
            dstZ,
            this.Description.Width,
            this.Description.Height,
            1);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.rendererID != -1)
        {
            this.invoker.DeleteTexture(this.rendererID);
            this.rendererID = -1;
        }

        this.isDisposed = true;
    }
}
