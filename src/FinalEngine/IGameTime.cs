// <copyright file="IGameTime.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine;

internal interface IGameTime
{
    float Delta { get; }

    float FrameRate { get; }

    internal bool CanProcessNextFrame();
}
