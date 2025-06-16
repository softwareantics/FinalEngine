// <copyright file="OpenGLRenderDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using System.Drawing;
using AutoMapper;
using FinalEngine.Rendering.Invocation;
using OpenTK.Graphics.OpenGL4;
using TKPrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

internal sealed class OpenGLRenderDevice : IRenderDevice
{
    private readonly IOpenGLInvoker invoker;

    private readonly IMapper mapper;

    public OpenGLRenderDevice(IOpenGLInvoker invoker, IMapper mapper)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        this.Factory = new OpenGLRenderResourceFactory(invoker, this.mapper);
        this.InputAssembler = new OpenGLInputAssembler();
        this.OutputMerger = new OpenGLOutputMerger(invoker, this.mapper);
        this.Pipeline = new OpenGLPipeline(invoker);
        this.Rasterizer = new OpenGLRasterizer(invoker, this.mapper);
    }

    public IRenderResourceFactory Factory { get; }

    public IInputAssembler InputAssembler { get; }

    public IOutputMerger OutputMerger { get; }

    public IPipeline Pipeline { get; }

    public IRasterizer Rasterizer { get; }

    public void Clear(Color color, float depth = 1, int stencil = 0)
    {
        this.invoker.ClearColor(color);
        this.invoker.ClearDepth(depth);
        this.invoker.ClearStencil(stencil);
        this.invoker.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
    }

    public void DrawIndices(PrimitiveTopology topology, int first, int count)
    {
        this.invoker.DrawElements(this.mapper.Map<TKPrimitiveType>(topology), count, DrawElementsType.UnsignedInt, first);
    }
}
