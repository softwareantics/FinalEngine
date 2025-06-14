// <copyright file="IRenderDevice.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Drawing;

public interface IRenderDevice
{
    void Clear(Color color);
}
