// <copyright file="OpenGLTexture2D.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using System;
using AutoMapper;
using FinalEngine.Rendering.Invocation;
using TKPixelForamt = OpenTK.Graphics.OpenGL4.PixelFormat;
using TKPixelType = OpenTK.Graphics.OpenGL4.PixelType;
using TKSizedInternalFormat = OpenTK.Graphics.OpenGL4.SizedInternalFormat;
using TKTextureMagFilter = OpenTK.Graphics.OpenGL4.TextureMagFilter;
using TKTextureMinFilter = OpenTK.Graphics.OpenGL4.TextureMinFilter;
using TKTextureParameterName = OpenTK.Graphics.OpenGL4.TextureParameterName;
using TKTextureTarget = OpenTK.Graphics.OpenGL4.TextureTarget;
using TKTextureWrapMode = OpenTK.Graphics.OpenGL4.TextureWrapMode;

internal sealed class OpenGLTexture2D : IOpenGLTexture
{
    private readonly IOpenGLInvoker invoker;

    private bool isDisposed;

    private int rendererID;

    public OpenGLTexture2D(
        IOpenGLInvoker invoker,
        IMapper mapper,
        Texture2DDescription description,
        PixelFormat format,
        SizedFormat internalFormat,
        nint data)
    {
        ArgumentNullException.ThrowIfNull(mapper);

        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));

        this.rendererID = invoker.CreateTexture(TKTextureTarget.Texture2D);

        this.Format = format;
        this.InternalFormat = internalFormat;

        this.Description = description;

        int mipmap = (int)Math.Ceiling(Math.Max(Math.Log2(description.Width + 1), Math.Log2(description.Height + 1)));

        invoker.TextureStorage2D(this.rendererID, mipmap, mapper.Map<TKSizedInternalFormat>(this.InternalFormat), description.Width, description.Height);

        invoker.TextureParameter(this.rendererID, TKTextureParameterName.TextureMinFilter, (int)mapper.Map<TKTextureMinFilter>(description.MinFilter));
        invoker.TextureParameter(this.rendererID, TKTextureParameterName.TextureMagFilter, (int)mapper.Map<TKTextureMagFilter>(description.MagFilter));
        invoker.TextureParameter(this.rendererID, TKTextureParameterName.TextureWrapS, (int)mapper.Map<TKTextureWrapMode>(description.WrapS));
        invoker.TextureParameter(this.rendererID, TKTextureParameterName.TextureWrapT, (int)mapper.Map<TKTextureWrapMode>(description.WrapT));

        if (data != nint.Zero)
        {
            invoker.TextureSubImage2D(
                texture: this.rendererID,
                level: 0,
                xoffset: 0,
                yoffset: 0,
                width: description.Width,
                height: description.Height,
                format: mapper.Map<TKPixelForamt>(this.Format),
                type: mapper.Map<TKPixelType>(description.PixelType),
                pixels: data);

            if (description.GenerateMipmaps)
            {
                invoker.GenerateTextureMipmap(this.rendererID);
            }
        }
    }

    ~OpenGLTexture2D()
    {
        this.Dispose(false);
    }

    public Texture2DDescription Description { get; }

    public PixelFormat Format { get; }

    public SizedFormat InternalFormat { get; }

    public void Bind(int unit)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenGLTexture2D));
        this.invoker.BindTextureUnit(unit, this.rendererID);
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
