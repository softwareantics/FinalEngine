// <copyright file="RasterStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.States;

using System;

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

public struct RasterStateDescription : IEquatable<RasterStateDescription>
{
    private FaceCullMode? cullMode;

    private RasterMode? fillMode;

    private WindingDirection? windingDirection;

    public bool CullEnabled { get; set; }

    public FaceCullMode CullMode
    {
        readonly get { return this.cullMode ?? FaceCullMode.Back; }
        set { this.cullMode = value; }
    }

    public RasterMode FillMode
    {
        readonly get { return this.fillMode ?? RasterMode.Solid; }
        set { this.fillMode = value; }
    }

    public bool MultiSamplingEnabled { get; set; }

    public bool ScissorEnabled { get; set; }

    public WindingDirection WindingDirection
    {
        readonly get { return this.windingDirection ?? WindingDirection.CounterClockwise; }
        set { this.windingDirection = value; }
    }

    public static bool operator !=(RasterStateDescription left, RasterStateDescription right)
    {
        return !(left == right);
    }

    public static bool operator ==(RasterStateDescription left, RasterStateDescription right)
    {
        return left.Equals(right);
    }

    public readonly bool Equals(RasterStateDescription other)
    {
        return this.CullEnabled == other.CullEnabled &&
               this.CullMode == other.CullMode &&
               this.FillMode == other.FillMode &&
               this.ScissorEnabled == other.ScissorEnabled &&
               this.WindingDirection == other.WindingDirection &&
               this.MultiSamplingEnabled == other.MultiSamplingEnabled;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is RasterStateDescription description && this.Equals(description);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return (this.CullEnabled.GetHashCode() * accumulator) +
               (this.CullMode.GetHashCode() * accumulator) +
               (this.FillMode.GetHashCode() * accumulator) +
               (this.ScissorEnabled.GetHashCode() * accumulator) +
               (this.WindingDirection.GetHashCode() * accumulator) +
               (this.MultiSamplingEnabled.GetHashCode() * accumulator);
    }
}
