// <copyright file="OpenTKWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK;

using System;
using System.Drawing;
using FinalEngine.Platform.Desktop.OpenTK.Invocation;
using global::OpenTK.Mathematics;

internal sealed class OpenTKWindow : IWindow, IEventsProcessor
{
    private readonly INativeWindowInvoker nativeWindow;

    private bool isDisposed;

    public OpenTKWindow(INativeWindowInvoker nativeWindow)
    {
        this.nativeWindow = nativeWindow ?? throw new ArgumentNullException(nameof(nativeWindow));
    }

    ~OpenTKWindow()
    {
        this.Dispose(false);
    }

    public bool CanProcessEvents
    {
        get { return !this.IsExiting; }
    }

    public Rectangle ClientBounds
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            return new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
        }
    }

    public Size ClientSize
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            return new Size(this.nativeWindow.ClientSize.X, this.nativeWindow.ClientSize.Y);
        }
    }

    public bool IsExiting
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            return this.nativeWindow.IsExiting;
        }
    }

    public bool IsFocused
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            return this.nativeWindow.IsFocused;
        }
    }

    public Size Size
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            return new Size(this.nativeWindow.Size.X, this.nativeWindow.Size.Y);
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            this.nativeWindow.Size = new Vector2i(value.Width, value.Height);
        }
    }

    public string Title
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            return this.nativeWindow.Title;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            this.nativeWindow.Title = value;
        }
    }

    public bool Visible
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            return this.nativeWindow.IsVisible;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, this);
            this.nativeWindow.IsVisible = value;
        }
    }

    public void Close()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        this.nativeWindow.Close();
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void ProcessEvents()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, this);
        this.nativeWindow.ProcessEvents();
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && !this.nativeWindow.IsDisposed)
        {
            this.nativeWindow.Dispose();
        }

        this.isDisposed = true;
    }
}
