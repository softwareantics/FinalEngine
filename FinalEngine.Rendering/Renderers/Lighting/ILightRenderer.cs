// <copyright file="ILightRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers.Lighting;

using System;
using System.Numerics;
using FinalEngine.Rendering.Lighting;

internal interface ILightRenderer : IRenderQueue<Light>
{
    void Render(Action renderScene);

    void SetAmbientLight(Vector3 color, float intensity);
}
