// <copyright file="GdiRenderResourceFactory.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Diagnostics.CodeAnalysis;
using FinalEngine.Rendering.Adapters.Drawing;

internal sealed class GdiRenderContext : IRenderContext
{
    private IBufferedGraphicsAdapter? bufferedGraphics;

    private IBufferedGraphicsContextAdapter? context;

    private bool isDisposed;

    [ExcludeFromCodeCoverage]
    public GdiRenderContext(nint handle, Size size)
        : this(new BufferedGraphicsContextAdapter(), handle, size)
    {
    }

    public GdiRenderContext(IBufferedGraphicsContextAdapter context, nint handle, Size size)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));

        this.context.MaximumBuffer = new Size()
        {
            Width = size.Width + 1,
            Height = size.Height + 1,
        };

        this.bufferedGraphics = this.context.Allocate(handle, new Rectangle(Point.Empty, size));
        this.bufferedGraphics.Graphics.EnableLowestQuality();
    }

    ~GdiRenderContext()
    {
        this.Dispose(false);
    }

    public static IGraphicsAdapter? CurrentGraphics { get; private set; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void MakeCurrent()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(GdiRenderContext));
        CurrentGraphics = this.bufferedGraphics!.Graphics;
    }

    public void SwapBuffers()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(GdiRenderContext));
        this.bufferedGraphics!.Render();
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.bufferedGraphics != null)
            {
                this.bufferedGraphics.Dispose();
                this.bufferedGraphics = null;
            }

            if (this.context != null)
            {
                this.context.Dispose();
                this.context = null;
            }
        }

        this.isDisposed = true;
    }
}
