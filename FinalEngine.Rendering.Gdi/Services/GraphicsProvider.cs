// <copyright file="GraphicsProvider.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Services;

using FinalEngine.Rendering.Adapters.Drawing;

internal sealed class GraphicsProvider : IGraphicsProvider
{
    public IGraphicsAdapter? GetCurrentGraphics()
    {
        return GdiRenderContext.CurrentGraphics;
    }
}
