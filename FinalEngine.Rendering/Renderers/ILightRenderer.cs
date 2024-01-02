// <copyright file="ILightRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

using FinalEngine.Rendering.Lighting;

public interface ILightRenderer
{
    void Render(Light light);
}