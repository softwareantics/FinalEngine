// <copyright file="IRenderContext.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Drawing;

public interface IRenderContext : IDisposable
{
    delegate IRenderContext RenderContextFactory(nint handle, Size size);

    void MakeCurrent();

    void SwapBuffers();
}
