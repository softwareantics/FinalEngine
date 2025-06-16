// <copyright file="DepthStateDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.States;

using System;

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

public struct DepthStateDescription : IEquatable<DepthStateDescription>
{
    private ComparisonMode? comparisonMode;

    private bool? writeEnabled;

    public ComparisonMode ComparisonMode
    {
        readonly get { return this.comparisonMode ?? ComparisonMode.Less; }
        set { this.comparisonMode = value; }
    }

    public bool ReadEnabled { get; set; }

    public bool WriteEnabled
    {
        readonly get { return this.writeEnabled ?? true; }
        set { this.writeEnabled = value; }
    }

    public static bool operator !=(DepthStateDescription left, DepthStateDescription right)
    {
        return !(left == right);
    }

    public static bool operator ==(DepthStateDescription left, DepthStateDescription right)
    {
        return left.Equals(right);
    }

    public readonly bool Equals(DepthStateDescription other)
    {
        return this.ComparisonMode == other.ComparisonMode &&
               this.ReadEnabled == other.ReadEnabled &&
               this.WriteEnabled == other.WriteEnabled;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is DepthStateDescription description && this.Equals(description);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return (this.ComparisonMode.GetHashCode() * accumulator) +
               (this.ReadEnabled.GetHashCode() * accumulator) +
               (this.WriteEnabled.GetHashCode() * accumulator);
    }
}
