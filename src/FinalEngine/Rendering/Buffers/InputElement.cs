// <copyright file="InputElement.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers;

public enum InputElementType
{
    Int,

    Byte,

    Short,

    Float,

    Double,
}

public readonly record struct InputElement(
    int Index,
    int Size,
    InputElementType Type,
    int RelativeOffset);
