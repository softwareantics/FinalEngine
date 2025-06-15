// <copyright file="GdiRenderResourceFactory.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Services;
using FinalEngine.Rendering.Textures;

internal sealed class GdiRenderDevice : IRenderDevice
{
    private readonly IGraphicsProvider provider;

    public GdiRenderDevice(IGraphicsProvider provider)
    {
        this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public void Clear(Color color)
    {
        this.provider.GetCurrentGraphics()?.Clear(color);
    }

    public void DrawTexture(ITexture2D texture, Vector2 position)
    {
        this.DrawTexture(texture, position.X, position.Y);
    }

    public void DrawTexture(ITexture2D texture, float x, float y)
    {
        ArgumentNullException.ThrowIfNull(texture);

        var graphics = this.provider.GetCurrentGraphics();

        if (graphics == null)
        {
            return;
        }

        if (texture is not IGdiTexture2D gdiTexture)
        {
            throw new ArgumentException($"The specified {nameof(texture)} is not a valid GDI texture.", nameof(texture));
        }

        gdiTexture.DrawImageUnscaled(graphics, (int)x, (int)y);
    }
}
