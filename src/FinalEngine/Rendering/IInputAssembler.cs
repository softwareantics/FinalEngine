// <copyright file="IInputAssembler.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Collections.Generic;
using FinalEngine.Rendering.Buffers;

public interface IInputAssembler
{
    void SetIndexBuffer(IIndexBuffer buffer);

    void SetInputLayout(IInputLayout layout);

    void SetVertexBuffer(IVertexBuffer buffer);

    void UpdateIndexBuffer<T>(IIndexBuffer buffer, IReadOnlyCollection<T> data)
        where T : struct;

    void UpdateVertexBuffer<T>(IVertexBuffer buffer, IReadOnlyCollection<T> data, int stride)
        where T : struct;
}
