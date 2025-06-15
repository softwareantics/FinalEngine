// <copyright file="BufferedGraphicsContextAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Adapters.Drawing;

using System.Diagnostics.CodeAnalysis;
using System.Drawing;

[ExcludeFromCodeCoverage]
internal sealed class BufferedGraphicsContextAdapter : IBufferedGraphicsContextAdapter
{
    private BufferedGraphicsContext? context;

    private bool isDisposed;

    public BufferedGraphicsContextAdapter(BufferedGraphicsContext? bufferedGraphics = null)
    {
        this.context ??= new BufferedGraphicsContext();
    }

    ~BufferedGraphicsContextAdapter()
    {
        this.Dispose(false);
    }

    public Size MaximumBuffer
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BufferedGraphicsContextAdapter));
            return this.context!.MaximumBuffer;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BufferedGraphicsContextAdapter));
            this.context!.MaximumBuffer = value;
        }
    }

    public IBufferedGraphicsAdapter Allocate(nint handle, Rectangle targetRectangle)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(BufferedGraphicsContextAdapter));
        return new BufferedGraphicsAdapter(this.context!.Allocate(Graphics.FromHwnd(handle), targetRectangle));
    }

    public IBufferedGraphicsAdapter Allocate(nint handle, Size size)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }
        if (disposing && this.context != null)
        {
            this.context.Dispose();
            this.context = null;
        }

        this.isDisposed = true;
    }
}
