// <copyright file="BufferedGraphicsAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Adapters.Drawing;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal sealed class BufferedGraphicsAdapter : IBufferedGraphicsAdapter
{
    private BufferedGraphics? bufferedGraphics;

    private bool isDisposed;

    public BufferedGraphicsAdapter(BufferedGraphics bufferedGraphics)
    {
        this.bufferedGraphics = bufferedGraphics ?? throw new ArgumentNullException(nameof(bufferedGraphics));
    }

    ~BufferedGraphicsAdapter()
    {
        this.Dispose(false);
    }

    public IGraphicsAdapter Graphics
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BufferedGraphicsAdapter));
            return new GraphicsAdapter(this.bufferedGraphics!.Graphics);
        }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Render()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BufferedGraphicsAdapter));
        this.bufferedGraphics!.Render();
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.bufferedGraphics != null)
        {
            this.bufferedGraphics.Dispose();
            this.bufferedGraphics = null;
        }

        this.isDisposed = true;
    }
}
