// <copyright file="ISceneRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers.Scenes;

using FinalEngine.Rendering.Cameras;

internal interface ISceneRenderer
{
    void Render(Camera camera, bool useBuiltInShader);
}
