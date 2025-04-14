// <copyright file="GeometryRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers.Geometry;

using System;
using System.Collections.Generic;
using FinalEngine.ECS.Components;
using FinalEngine.Rendering.Geometry;

internal sealed class GeometryRenderer : IRenderQueue<Model>, IGeometryRenderer
{
    private readonly Dictionary<IMaterial, IList<RenderModel>> materialToRenderModelMap;

    private readonly IRenderDevice renderDevice;

    public GeometryRenderer(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.materialToRenderModelMap = [];
    }

    public bool CanRender
    {
        get { return this.materialToRenderModelMap.Count != 0; }
    }

    public void Clear()
    {
        this.materialToRenderModelMap.Clear();
    }

    public void Enqueue(RenderModel renderable)
    {
        ArgumentNullException.ThrowIfNull(renderable, nameof(renderable));

        if (!this.materialToRenderModelMap.TryGetValue(renderable.Material, out var batch))
        {
            batch = [];
            this.materialToRenderModelMap.Add(renderable.Material, batch);
        }

        batch.Add(renderable);
    }

    public void Enqueue(Model renderable)
    {
        ArgumentNullException.ThrowIfNull(renderable, nameof(renderable));

        if (renderable.RenderModel != null)
        {
            this.Enqueue(renderable.RenderModel);
        }

        foreach (var child in renderable.Children)
        {
            this.Enqueue(child);
        }
    }

    public void Render()
    {
        foreach (var kvp in this.materialToRenderModelMap)
        {
            var material = kvp.Key;
            var batch = kvp.Value;

            material.Bind(this.renderDevice.Pipeline);

            foreach (var renderModel in batch)
            {
                this.UpdateUniforms(renderModel.Transform);
                this.RenderBatchInstance(renderModel.Mesh);
            }
        }
    }

    private void RenderBatchInstance(IMesh? mesh)
    {
        if (mesh == null)
        {
            return;
        }

        mesh.Bind(this.renderDevice.InputAssembler);
        mesh.Draw(this.renderDevice);
    }

    private void UpdateUniforms(TransformComponent transform)
    {
        this.renderDevice.Pipeline.SetUniform("u_transform", transform.CreateTransformationMatrix());
    }
}
