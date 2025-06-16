// <copyright file="OpenGLOutputMerger.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;
using AutoMapper;
using FinalEngine.Rendering.Invocation;
using FinalEngine.Rendering.States;
using OpenTK.Graphics.OpenGL4;
using TKBlendEquationMode = OpenTK.Graphics.OpenGL4.BlendEquationMode;
using TKBlendingFactor = OpenTK.Graphics.OpenGL4.BlendingFactor;
using TKDepthFunction = OpenTK.Graphics.OpenGL4.DepthFunction;
using TKStencilFunction = OpenTK.Graphics.OpenGL4.StencilFunction;
using TKStencilOp = OpenTK.Graphics.OpenGL4.StencilOp;

internal sealed class OpenGLOutputMerger : IOutputMerger
{
    private readonly IOpenGLInvoker invoker;

    private readonly IMapper mapper;

    public OpenGLOutputMerger(IOpenGLInvoker invoker, IMapper mapper)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public void SetBlendState(BlendStateDescription description)
    {
        this.invoker.Cap(EnableCap.Blend, description.Enabled);
        this.invoker.BlendColor(description.Color);
        this.invoker.BlendEquation(this.mapper.Map<TKBlendEquationMode>(description.EquationMode));
        this.invoker.BlendFunc(this.mapper.Map<TKBlendingFactor>(description.SourceMode), this.mapper.Map<TKBlendingFactor>(description.DestinationMode));
    }

    public void SetDepthState(DepthStateDescription description)
    {
        this.invoker.Cap(EnableCap.DepthTest, description.ReadEnabled);
        this.invoker.DepthMask(description.WriteEnabled);
        this.invoker.DepthFunc(this.mapper.Map<TKDepthFunction>(description.ComparisonMode));
    }

    public void SetStencilState(StencilStateDescription description)
    {
        this.invoker.Cap(EnableCap.StencilTest, description.Enabled);
        this.invoker.StencilMask(description.WriteMask);
        this.invoker.StencilFunc(this.mapper.Map<TKStencilFunction>(description.ComparisonMode), description.ReferenceValue, description.ReadMask);
        this.invoker.StencilOp(
            this.mapper.Map<TKStencilOp>(description.StencilFail),
            this.mapper.Map<TKStencilOp>(description.DepthFail),
            this.mapper.Map<TKStencilOp>(description.Pass));
    }
}
