// <copyright file="InputElement.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers;

using System;

public enum InputElementType
{
    Int,

    Byte,

    Short,

    Float,

    Double,
}

public struct InputElement : IEquatable<InputElement>
{
    public InputElement(int index, int size, InputElementType type, int relativeOffset)
    {
        this.Index = index;
        this.Size = size;
        this.Type = type;
        this.RelativeOffset = relativeOffset;
    }

    public int Index { get; set; }

    public int RelativeOffset { get; set; }

    public int Size { get; set; }

    public InputElementType Type { get; set; }

    public static bool operator !=(InputElement left, InputElement right)
    {
        return !(left == right);
    }

    public static bool operator ==(InputElement left, InputElement right)
    {
        return left.Equals(right);
    }

    public readonly bool Equals(InputElement other)
    {
        return this.Index == other.Index &&
               this.RelativeOffset == other.RelativeOffset &&
               this.Size == other.Size &&
               this.Type == other.Type;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is InputElement description && this.Equals(description);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return (this.Index.GetHashCode() * accumulator) +
               (this.RelativeOffset.GetHashCode() * accumulator) +
               (this.Size.GetHashCode() * accumulator) +
               (this.Type.GetHashCode() * accumulator);
    }
}
