// <copyright file="IBufferedGraphicsContextAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Adapters.Drawing;

internal interface IBufferedGraphicsContextAdapter : IDisposable
{
    Size MaximumBuffer { get; set; }

    IBufferedGraphicsAdapter Allocate(nint handle, Rectangle targetRectangle);
}
