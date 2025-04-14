// <copyright file="ISkyboxRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers.Skyboxes;

using FinalEngine.Rendering.Cameras;
using FinalEngine.Rendering.Textures;

internal interface ISkyboxRenderer
{
    void Render(Camera camera);

    void SetSkybox(ITextureCube? texture);
}
