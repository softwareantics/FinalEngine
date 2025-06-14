// <copyright file="RenderDevice.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Drawing;
using FinalEngine.Rendering.Services;

internal sealed class RenderDevice : IRenderDevice
{
    private readonly IGraphicsProvider provider;

    public RenderDevice(IGraphicsProvider provider)
    {
        this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public void Clear(Color color)
    {
        this.provider.GetCurrentGraphics()?.Clear(color);
    }
}
