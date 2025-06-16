// <copyright file="MouseScrollEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System;

public sealed class MouseScrollEventArgs : EventArgs
{
    public double Offset { get; init; }
}
