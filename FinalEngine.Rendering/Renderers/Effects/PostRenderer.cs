// <copyright file="PostRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers.Effects;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Cameras;
using FinalEngine.Rendering.Effects;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Primitives;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

internal sealed class PostRenderer : IPostRenderer, IDisposable
{
    private readonly IRenderDevice renderDevice;

    private readonly List<IRenderEffect> renderEffects;

    private ITexture2D? colorTexture;

    private ITexture2D? depthTexture;

    private IFrameBuffer? frameBuffer;

    private bool isDisposed;

    private Mesh<QuadVertex>? mesh;

    private IShaderProgram? shaderProgram;

    public PostRenderer(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

        this.renderEffects = [];

        QuadVertex[] vertices =
        [
            new QuadVertex() { Position = new Vector2(-1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), },
            new QuadVertex() { Position = new Vector2(-1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), },
            new QuadVertex() { Position = new Vector2(1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), },
            new QuadVertex() { Position = new Vector2(-1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), },
            new QuadVertex() { Position = new Vector2(1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), },
            new QuadVertex() { Position = new Vector2(1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f) },
        ];

        int[] indices =
        [
            0,
            1,
            2,
            3,
            4,
            5,
        ];

        this.mesh = new Mesh<QuadVertex>(this.renderDevice.Factory, vertices, indices, QuadVertex.InputElements, QuadVertex.SizeInBytes);
    }

    ~PostRenderer()
    {
        this.Dispose(false);
    }

    public bool CanRender
    {
        get { return this.renderEffects.Count > 0; }
    }

    private ITexture2D ColorTexture
    {
        get
        {
            return this.colorTexture ??= this.renderDevice.Factory.CreateTexture2D<byte>(
            new Texture2DDescription()
            {
                Width = this.renderDevice.Rasterizer.GetViewport().Width,
                Height = this.renderDevice.Rasterizer.GetViewport().Height,
                MinFilter = TextureFilterMode.Linear,
                MagFilter = TextureFilterMode.Linear,
                WrapS = TextureWrapMode.Repeat,
                WrapT = TextureWrapMode.Repeat,
                PixelType = PixelType.Float,
                GenerateMipmaps = false,
            },
            null,
            PixelFormat.Rgb,
            SizedFormat.Rgba16F);
        }
    }

    private ITexture2D DepthTexture
    {
        get
        {
            return this.depthTexture ??= this.renderDevice.Factory.CreateTexture2D<byte>(
            new Texture2DDescription()
            {
                Width = this.renderDevice.Rasterizer.GetViewport().Width,
                Height = this.renderDevice.Rasterizer.GetViewport().Height,
                MinFilter = TextureFilterMode.Nearest,
                MagFilter = TextureFilterMode.Nearest,
                WrapS = TextureWrapMode.Repeat,
                WrapT = TextureWrapMode.Repeat,
                PixelType = PixelType.Float,
                GenerateMipmaps = false,
            },
            null,
            PixelFormat.Depth,
            SizedFormat.Depth24);
        }
    }

    private IFrameBuffer FrameBuffer
    {
        get { return this.frameBuffer ??= this.renderDevice.Factory.CreateFrameBuffer([this.ColorTexture], this.DepthTexture); }
    }

    private IShaderProgram ShaderProgram
    {
        get { return this.shaderProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Post\\standard-post.fesp"); }
    }

    public void Clear()
    {
        this.renderEffects.Clear();
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Enqueue(IRenderEffect renderable)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        ArgumentNullException.ThrowIfNull(renderable, nameof(renderable));

        this.renderEffects.Add(renderable);
    }

    public void Render(ICamera camera, Action renderScene)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        ArgumentNullException.ThrowIfNull(camera, nameof(camera));
        ArgumentNullException.ThrowIfNull(renderScene, nameof(renderScene));

        this.renderDevice.Pipeline.SetFrameBuffer(this.FrameBuffer);
        this.renderDevice.Rasterizer.SetViewport(camera.Bounds);
        this.renderDevice.Clear(Color.Black);

        renderScene();

        this.renderDevice.Pipeline.SetFrameBuffer(null);
        this.renderDevice.Rasterizer.SetViewport(camera.Bounds);
        this.renderDevice.Clear(Color.Black);

        this.renderDevice.Pipeline.SetShaderProgram(this.ShaderProgram);

        this.renderDevice.Pipeline.SetUniform("u_screenTexture", 0);
        this.renderDevice.Pipeline.SetTexture(this.ColorTexture, 0);

        foreach (var renderEffect in this.renderEffects)
        {
            renderEffect.Bind(this.renderDevice.Pipeline);
        }

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            ReadEnabled = false,
        });

        this.mesh!.Bind(this.renderDevice.InputAssembler);
        this.mesh.Draw(this.renderDevice);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.mesh != null)
            {
                this.mesh.Dispose();
                this.mesh = null;
            }

            if (this.colorTexture != null)
            {
                this.colorTexture.Dispose();
                this.colorTexture = null;
            }

            if (this.depthTexture != null)
            {
                this.depthTexture.Dispose();
                this.depthTexture = null;
            }

            if (this.frameBuffer != null)
            {
                this.frameBuffer.Dispose();
                this.frameBuffer = null;
            }
        }

        this.isDisposed = true;
    }
}
