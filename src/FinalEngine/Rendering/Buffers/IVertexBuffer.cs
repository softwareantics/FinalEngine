// <copyright file="IVertexBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers;

using System;

public interface IVertexBuffer : IDisposable
{
    int Stride { get; }

    BufferUsageType Type { get; }
}
