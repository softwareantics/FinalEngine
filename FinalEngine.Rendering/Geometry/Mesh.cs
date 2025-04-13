// <copyright file="Mesh.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Geometry;

using System;
using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;

public sealed class Mesh<TVertex> : IMesh, IDisposable
    where TVertex : struct
{
    private readonly IInputLayout inputLayout;

    private IIndexBuffer? indexBuffer;

    private bool isDisposed;

    private IVertexBuffer? vertexBuffer;

    public Mesh(
        IGPUResourceFactory factory,
        TVertex[] vertices,
        int[] indices,
        IReadOnlyCollection<InputElement> inputElements,
        int vertexStride)
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        ArgumentNullException.ThrowIfNull(vertices, nameof(vertices));
        ArgumentNullException.ThrowIfNull(indices, nameof(indices));
        ArgumentNullException.ThrowIfNull(inputElements, nameof(inputElements));

        this.vertexBuffer = factory.CreateVertexBuffer(
            BufferUsageType.Static,
            vertices,
            vertices.Length * vertexStride,
            vertexStride);

        this.indexBuffer = factory.CreateIndexBuffer(
            BufferUsageType.Static,
            indices,
            indices.Length * sizeof(int));

        this.inputLayout = factory.CreateInputLayout(inputElements);
    }

    ~Mesh()
    {
        this.Dispose(false);
    }

    public delegate void CalculateNormals(TVertex[] vertices, int[] indices);

    public delegate void CalculateTangents(TVertex[] vertices, int[] indices);

    public void Bind(IInputAssembler inputAssembler)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        ArgumentNullException.ThrowIfNull(inputAssembler, nameof(inputAssembler));

        inputAssembler.SetInputLayout(this.inputLayout);
        inputAssembler.SetVertexBuffer(this.vertexBuffer!);
        inputAssembler.SetIndexBuffer(this.indexBuffer!);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Draw(IRenderDevice renderDevice)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        ArgumentNullException.ThrowIfNull(renderDevice, nameof(renderDevice));

        renderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, this.indexBuffer!.Length);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.indexBuffer != null)
            {
                this.indexBuffer.Dispose();
                this.indexBuffer = null;
            }

            if (this.vertexBuffer != null)
            {
                this.vertexBuffer.Dispose();
                this.vertexBuffer = null;
            }
        }

        this.isDisposed = true;
    }
}
