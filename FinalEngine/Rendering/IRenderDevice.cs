// <copyright file="IRenderDevice.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Drawing;
using System.Numerics;
using FinalEngine.Rendering.Textures;

public interface IRenderDevice
{
    void Clear(Color color);

    void DrawTexture(ITexture2D texture, Vector2 position);

    void DrawTexture(ITexture2D texture, float x, float y);
}
