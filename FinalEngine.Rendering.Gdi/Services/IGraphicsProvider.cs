// <copyright file="IGraphicsProvider.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Services;

using FinalEngine.Rendering.Adapters.Drawing;

internal interface IGraphicsProvider
{
    IGraphicsAdapter? GetCurrentGraphics();
}
