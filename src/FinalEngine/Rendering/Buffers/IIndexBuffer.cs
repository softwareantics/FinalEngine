// <copyright file="IIndexBuffer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers;

using System;

public interface IIndexBuffer : IDisposable
{
    int Length { get; }

    BufferUsageType Type { get; }
}
