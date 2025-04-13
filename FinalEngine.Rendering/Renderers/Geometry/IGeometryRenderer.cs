// <copyright file="IGeometryRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers.Geometry;

using FinalEngine.Rendering.Geometry;

internal interface IGeometryRenderer : IRenderQueue<RenderModel>
{
    void Render();
}
