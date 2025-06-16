// <copyright file="IRasterizer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Drawing;
using FinalEngine.Rendering.States;

public interface IRasterizer
{
    RasterStateDescription GetRasterState();

    Rectangle GetViewport();

    void SetRasterState(RasterStateDescription description);

    void SetViewport(Rectangle rectangle, float near = 0.0f, float far = 1.0f);
}
