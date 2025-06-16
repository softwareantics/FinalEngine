// <copyright file="IRenderDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Drawing;

public enum PrimitiveTopology
{
    Line,

    LineStrip,

    Triangle,

    TriangleStrip,
}

public interface IRenderDevice
{
    IRenderResourceFactory Factory { get; }

    IInputAssembler InputAssembler { get; }

    IOutputMerger OutputMerger { get; }

    IPipeline Pipeline { get; }

    IRasterizer Rasterizer { get; }

    void Clear(Color color, float depth = 1.0f, int stencil = 0);

    void DrawIndices(PrimitiveTopology topology, int first, int count);
}
