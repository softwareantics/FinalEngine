// <copyright file="BlendStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.States;

using System.Drawing;

public enum BlendEquationMode
{
    Add,

    Subtract,

    ReverseSubtract,

    Min,

    Max,
}

public enum BlendMode
{
    Zero,

    One,

    SourceColor,

    OneMinusSourceColor,

    DestinationColor,

    OneMinusDestinationColor,

    SourceAlpha,

    OneMinusSourceAlpha,

    DestinationAlpha,

    OneMinusDestinationAlpha,
}

public readonly record struct BlendStateDescription(
    Color Color,
    BlendMode DestinationMode,
    BlendEquationMode EquationMode,
    BlendMode SourceMode,
    bool Enabled = false)
{
    public BlendStateDescription()
        : this(Color.Black, BlendMode.Zero, BlendEquationMode.Add, BlendMode.One, false)
    {
    }
}
