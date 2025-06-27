// <copyright file="RasterStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.States;

public enum FaceCullMode
{
    Front,

    Back,
}

public enum RasterMode
{
    Solid,

    Wireframe,
}

public enum WindingDirection
{
    Clockwise,

    CounterClockwise,
}

public readonly record struct RasterStateDescription(
    FaceCullMode CullMode,
    RasterMode FillMode,
    WindingDirection WindingDirection,
    bool CullEnabled,
    bool ScissorEnabled,
    bool MultiSamplingEnabled)
{
    public RasterStateDescription()
        : this(FaceCullMode.Back, RasterMode.Solid, WindingDirection.CounterClockwise, false, false, false)
    {
    }
}
