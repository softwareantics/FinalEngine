// <copyright file="MouseButtonEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System;

public sealed class MouseButtonEventArgs : EventArgs
{
    public MouseButton Button { get; init; }
}
