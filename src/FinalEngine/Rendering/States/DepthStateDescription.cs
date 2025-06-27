// <copyright file="DepthStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.States;

public enum ComparisonMode
{
    Never,

    Less,

    Equal,

    LessEqual,

    Greater,

    NotEqual,

    GreaterEqual,

    Always,
}

public readonly record struct DepthStateDescription(
    ComparisonMode ComparisonMode,
    bool ReadEnabled,
    bool WriteEnabled)
{
    public DepthStateDescription()
        : this(ComparisonMode.Less, true, true)
    {
    }
}
