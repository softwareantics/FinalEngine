// <copyright file="IRenderCoordinator.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

public interface IRenderCoordinator
{
    bool CanRenderEffects { get; }

    bool CanRenderGeometry { get; }

    bool CanRenderLights { get; }

    void ClearQueues();
}
