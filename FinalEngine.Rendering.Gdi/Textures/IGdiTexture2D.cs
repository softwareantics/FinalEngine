// <copyright file="IGdiTexture2D.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using FinalEngine.Rendering.Adapters.Drawing;

internal interface IGdiTexture2D : ITexture2D
{
    void DrawImageUnscaled(IGraphicsAdapter graphics, int x, int y);
}
