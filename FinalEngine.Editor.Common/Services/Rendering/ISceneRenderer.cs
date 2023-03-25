// <copyright file="ISceneRenderer.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Rendering;

public interface ISceneRenderer
{
    void ChangeProjection(int projectionWidth, int projectionHeight);

    void Render();
}