// <copyright file="IOutputMerger.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using FinalEngine.Rendering.States;

public interface IOutputMerger
{
    void SetBlendState(BlendStateDescription description);

    void SetDepthState(DepthStateDescription description);

    void SetStencilState(StencilStateDescription description);
}
