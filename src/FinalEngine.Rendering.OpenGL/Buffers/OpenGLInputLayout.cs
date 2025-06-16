// <copyright file="OpenGLInputLayout.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers;

using System;
using System.Collections.Generic;
using AutoMapper;
using FinalEngine.Rendering.Invocation;
using TKVertexAttribType = OpenTK.Graphics.OpenGL4.VertexAttribType;

internal sealed class OpenGLInputLayout : IOpenGLInputLayout
{
    private readonly IOpenGLInvoker invoker;

    private readonly IMapper mapper;

    public OpenGLInputLayout(IOpenGLInvoker invoker, IMapper mapper, IReadOnlyCollection<InputElement> elements)
    {
        this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.Elements = elements ?? throw new ArgumentNullException(nameof(elements));
    }

    public IEnumerable<InputElement> Elements { get; }

    public void Bind()
    {
        foreach (var element in this.Elements)
        {
            this.invoker.VertexAttribFormat(element.Index, element.Size, this.mapper.Map<TKVertexAttribType>(element.Type), false, element.RelativeOffset);
            this.invoker.VertexAttribBinding(element.Index, 0);
            this.invoker.EnableVertexAttribArray(element.Index);
        }
    }

    public void Unbind()
    {
        foreach (var element in this.Elements)
        {
            this.invoker.DisableVertexAttribArray(element.Index);
        }
    }
}
