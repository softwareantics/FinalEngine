// <copyright file="StencilStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.States;

using System;

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

public struct StencilStateDescription : IEquatable<StencilStateDescription>
{
    private ComparisonMode? comparisonMode;

    private StencilOperation? depthFail;

    private StencilOperation? pass;

    private int? readMask;

    private StencilOperation? stencilFail;

    private int? writeMask;

    public ComparisonMode ComparisonMode
    {
        readonly get { return this.comparisonMode ?? ComparisonMode.Always; }
        set { this.comparisonMode = value; }
    }

    public StencilOperation DepthFail
    {
        readonly get { return this.depthFail ?? StencilOperation.Keep; }
        set { this.depthFail = value; }
    }

    public bool Enabled { get; set; }

    public StencilOperation Pass
    {
        readonly get { return this.pass ?? StencilOperation.Keep; }
        set { this.pass = value; }
    }

    public int ReadMask
    {
        readonly get { return this.readMask ?? 0; }
        set { this.readMask = value; }
    }

    public int ReferenceValue { get; set; }

    public StencilOperation StencilFail
    {
        readonly get { return this.stencilFail ?? StencilOperation.Keep; }
        set { this.stencilFail = value; }
    }

    public int WriteMask
    {
        readonly get { return this.writeMask ?? -1; }
        set { this.writeMask = value; }
    }

    public static bool operator !=(StencilStateDescription left, StencilStateDescription right)
    {
        return !(left == right);
    }

    public static bool operator ==(StencilStateDescription left, StencilStateDescription right)
    {
        return left.Equals(right);
    }

    public readonly bool Equals(StencilStateDescription other)
    {
        return this.ComparisonMode == other.comparisonMode &&
               this.DepthFail == other.DepthFail &&
               this.Enabled == other.Enabled &&
               this.Pass == other.pass &&
               this.ReadMask == other.ReadMask &&
               this.ReferenceValue == other.ReferenceValue &&
               this.StencilFail == other.StencilFail &&
               this.WriteMask == other.WriteMask;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is StencilStateDescription description && this.Equals(description);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return (this.ComparisonMode.GetHashCode() * accumulator) +
               (this.DepthFail.GetHashCode() * accumulator) +
               (this.Enabled.GetHashCode() * accumulator) +
               (this.Pass.GetHashCode() * accumulator) +
               (this.ReadMask.GetHashCode() * accumulator) +
               (this.ReferenceValue.GetHashCode() * accumulator) +
               (this.StencilFail.GetHashCode() * accumulator) +
               (this.WriteMask.GetHashCode() * accumulator);
    }
}
