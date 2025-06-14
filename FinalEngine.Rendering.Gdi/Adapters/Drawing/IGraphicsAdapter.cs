// <copyright file="IGraphicsAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Adapters.Drawing;

using System.Drawing;

internal interface IGraphicsAdapter : IDisposable
{
    void Clear(Color color);
}
