// <copyright file="BlendStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.States;

using System;
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

public struct BlendStateDescription : IEquatable<BlendStateDescription>
{
    private Color? color;

    private BlendMode? destinationMode;

    private BlendEquationMode? equationMode;

    private BlendMode? sourceMode;

    public Color Color
    {
        readonly get { return this.color ?? Color.Black; }
        set { this.color = value; }
    }

    public BlendMode DestinationMode
    {
        readonly get { return this.destinationMode ?? BlendMode.Zero; }
        set { this.destinationMode = value; }
    }

    public bool Enabled { get; set; }

    public BlendEquationMode EquationMode
    {
        readonly get { return this.equationMode ?? BlendEquationMode.Add; }
        set { this.equationMode = value; }
    }

    public BlendMode SourceMode
    {
        readonly get { return this.sourceMode ?? BlendMode.One; }
        set { this.sourceMode = value; }
    }

    public static bool operator !=(BlendStateDescription left, BlendStateDescription right)
    {
        return !(left == right);
    }

    public static bool operator ==(BlendStateDescription left, BlendStateDescription right)
    {
        return left.Equals(right);
    }

    public readonly bool Equals(BlendStateDescription other)
    {
        return this.Color == other.Color &&
               this.DestinationMode == other.DestinationMode &&
               this.Enabled == other.Enabled &&
               this.EquationMode == other.EquationMode &&
               this.SourceMode == other.SourceMode;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is BlendStateDescription description && this.Equals(description);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return (this.Color.GetHashCode() * accumulator) +
               (this.DestinationMode.GetHashCode() * accumulator) +
               (this.Enabled.GetHashCode() * accumulator) +
               (this.EquationMode.GetHashCode() * accumulator) +
               (this.SourceMode.GetHashCode() * accumulator);
    }
}
