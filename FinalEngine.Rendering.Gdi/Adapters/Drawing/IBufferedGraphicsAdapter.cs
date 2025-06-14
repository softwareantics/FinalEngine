// <copyright file="IBufferedGraphicsAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Adapters.Drawing;

internal interface IBufferedGraphicsAdapter : IDisposable
{
    IGraphicsAdapter Graphics { get; }

    void Render();
}
