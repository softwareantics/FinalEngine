// <copyright file="IRenderingEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using FinalEngine.Rendering.Cameras;

public interface IRenderingEngine
{
    void Render(Camera camera);
}
