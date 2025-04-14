// <copyright file="IRenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Numerics;
using FinalEngine.Rendering.Cameras;
using FinalEngine.Rendering.Textures;

public interface IRenderingEngine
{
    void Render(Camera camera);

    void SetAmbientLight(Vector3 color, float strength);

    void SetSkybox(ITextureCube texture);
}
