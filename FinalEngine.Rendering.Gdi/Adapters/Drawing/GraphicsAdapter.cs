// <copyright file="GraphicsAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Adapters.Drawing;

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

[ExcludeFromCodeCoverage]
internal sealed class GraphicsAdapter : IGraphicsAdapter
{
    private readonly bool isDisposed;

    private Graphics? graphics;

    public GraphicsAdapter(Graphics graphics)
    {
        this.graphics = graphics ?? throw new ArgumentNullException(nameof(graphics));
    }

    ~GraphicsAdapter()
    {
        this.Dispose(false);
    }

    public void Clear(Color color)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(GraphicsAdapter));
        this.graphics!.Clear(color);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void DrawImageUnscaled(IBitmapAdapter image, int x, int y)
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(GraphicsAdapter));
        ArgumentNullException.ThrowIfNull(image);

        this.graphics!.DrawImageUnscaled(image.GetImplementation(), x, y);
    }

    public void EnableLowestQuality()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(GraphicsAdapter));

        this.graphics!.CompositingMode = CompositingMode.SourceOver;
        this.graphics.CompositingQuality = CompositingQuality.HighSpeed;
        this.graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        this.graphics.PixelOffsetMode = PixelOffsetMode.Half;
        this.graphics.SmoothingMode = SmoothingMode.HighSpeed;
        this.graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.graphics != null)
        {
            this.graphics.Dispose();
            this.graphics = null;
        }

        this.graphics = null;
    }
}
