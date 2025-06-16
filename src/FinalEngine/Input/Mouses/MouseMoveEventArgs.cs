// <copyright file="MouseMoveEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System;
using System.Drawing;

public sealed class MouseMoveEventArgs : EventArgs
{
    public PointF Location { get; init; }
}
