// <copyright file="SkyboxRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers.Skyboxes;

using System;
using System.Numerics;
using FinalEngine.Rendering.Cameras;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Primitives;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

internal sealed class SkyboxRenderer : ISkyboxRenderer, IDisposable
{
    private readonly IRenderDevice renderDevice;

    private bool isDisposed;

    private Mesh<SkyboxVertex>? mesh;

    private IShaderProgram? skyboxProgram;

    private ITextureCube? texture;

    public SkyboxRenderer(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

        SkyboxVertex[] vertices =
        {
            new () { Position = new Vector3(-1.0f, 1.0f, -1.0f) },
            new () { Position = new Vector3(1.0f, 1.0f, -1.0f) },
            new () { Position = new Vector3(1.0f, 1.0f, 1.0f) },
            new () { Position = new Vector3(-1.0f, 1.0f, 1.0f) },
            new () { Position = new Vector3(-1.0f, -1.0f, 1.0f) },
            new () { Position = new Vector3(1.0f, -1.0f, 1.0f) },
            new () { Position = new Vector3(1.0f, -1.0f, -1.0f) },
            new () { Position = new Vector3(-1.0f, -1.0f, -1.0f) },
            new () { Position = new Vector3(-1.0f, 1.0f, -1.0f) },
            new () { Position = new Vector3(-1.0f, 1.0f, 1.0f) },
            new () { Position = new Vector3(-1.0f, -1.0f, 1.0f) },
            new () { Position = new Vector3(-1.0f, -1.0f, -1.0f) },
            new () { Position = new Vector3(1.0f, 1.0f, 1.0f) },
            new () { Position = new Vector3(1.0f, 1.0f, -1.0f) },
            new () { Position = new Vector3(1.0f, -1.0f, -1.0f) },
            new () { Position = new Vector3(1.0f, -1.0f, 1.0f) },
            new () { Position = new Vector3(1.0f, 1.0f, -1.0f) },
            new () { Position = new Vector3(-1.0f, 1.0f, -1.0f) },
            new () { Position = new Vector3(-1.0f, -1.0f, -1.0f) },
            new () { Position = new Vector3(1.0f, -1.0f, -1.0f) },
            new () { Position = new Vector3(-1.0f, 1.0f, 1.0f) },
            new () { Position = new Vector3(1.0f, 1.0f, 1.0f) },
            new () { Position = new Vector3(1.0f, -1.0f, 1.0f) },
            new () { Position = new Vector3(-1.0f, -1.0f, 1.0f) },
        };

        int[] indices =
        {
            0, 1, 2, 0, 2, 3,
            4, 5, 6, 4, 6, 7,
            8, 9, 10, 8, 10, 11,
            12, 13, 14, 12, 14, 15,
            16, 17, 18, 16, 18, 19,
            20, 21, 22, 20, 22, 23,
        };

        this.mesh = new Mesh<SkyboxVertex>(this.renderDevice.Factory, vertices, indices, SkyboxVertex.InputElements, SkyboxVertex.SizeInBytes);
    }

    private IShaderProgram SkyboxProgram
    {
        get { return this.skyboxProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Skybox\\standard-skybox.fesp"); }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Render(Camera camera)
    {
        ArgumentNullException.ThrowIfNull(camera, nameof(camera));
        ObjectDisposedException.ThrowIf(this.isDisposed, this);

        if (this.texture == null)
        {
            return;
        }

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            WriteEnabled = true,
            ReadEnabled = true,
            ComparisonMode = ComparisonMode.LessEqual,
        });

        this.renderDevice.Rasterizer.SetRasterState(new RasterStateDescription()
        {
            CullEnabled = true,
            CullMode = FaceCullMode.Back,
            WindingDirection = WindingDirection.CounterClockwise,
            MultiSamplingEnabled = true,
        });

        this.renderDevice.Pipeline.SetShaderProgram(this.SkyboxProgram);
        this.renderDevice.Pipeline.SetUniform("u_projection", camera.Projection);

        // Remove translation from the view matrix
        var view = camera.View;
        Matrix4x4.Decompose(view, out var scale, out var rotation, out _);

        var viewNoTranslation = Matrix4x4.CreateScale(scale) * Matrix4x4.CreateFromQuaternion(rotation);

        this.renderDevice.Pipeline.SetUniform("u_view", viewNoTranslation);
        this.renderDevice.Pipeline.SetTexture(this.texture, 0);

        this.mesh!.Bind(this.renderDevice.InputAssembler);
        this.mesh.Draw(this.renderDevice);
    }

    public void SetSkybox(ITextureCube? texture)
    {
        this.texture = texture;
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
        }

        this.isDisposed = true;
    }
}
