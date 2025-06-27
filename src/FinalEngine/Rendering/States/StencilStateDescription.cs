// <copyright file="StencilStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.States;

public enum StencilOperation
{
    Keep,

    Zero,

    Replace,

    Increment,

    IncrementWrap,

    Decrement,

    DecrementWrap,

    Invert,
}

public readonly record struct StencilStateDescription(
    ComparisonMode ComparisonMode,
    StencilOperation DepthFail,
    StencilOperation Pass,
    StencilOperation StencilFail,
    int ReadMask,
    int WriteMask,
    int ReferenceValue,
    bool Enabled)
{
    public StencilStateDescription()
        : this(ComparisonMode.Always, StencilOperation.Keep, StencilOperation.Keep, StencilOperation.Keep, 0, -1, 0, false)
    {
    }
}
