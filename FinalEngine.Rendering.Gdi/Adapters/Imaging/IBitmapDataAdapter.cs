// <copyright file="IBitmapDataAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Adapters.Imaging;

using System.Drawing.Imaging;

internal interface IBitmapDataAdapter : IDisposable
{
    nint Scan0 { get; }

    int Stride { get; }

    BitmapData GetImplementation();
}
