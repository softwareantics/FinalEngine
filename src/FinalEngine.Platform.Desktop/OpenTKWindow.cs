// <copyright file="OpenTKWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System;
using System.Drawing;
using FinalEngine.Platform.Adapters;

internal sealed class OpenTKWindow : IWindow, IEventsProcessor
{
    private bool isDisposed;

    private INativeWindowAdapter? nativeWindow;

    public OpenTKWindow(INativeWindowAdapter nativeWindow)
    {
        this.nativeWindow = nativeWindow ?? throw new ArgumentNullException(nameof(nativeWindow));
    }

    ~OpenTKWindow()
    {
        this.Dispose(false);
    }

    public bool CanProcessEvents
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenTKWindow));
            return !this.nativeWindow!.IsExiting;
        }
    }

    public Size ClientSize
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenTKWindow));
            return new Size(this.nativeWindow!.ClientSize.X, this.nativeWindow.ClientSize.Y);
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenTKWindow));
            this.nativeWindow!.ClientSize = new OpenTK.Mathematics.Vector2i(value.Width, value.Height);
        }
    }

    public bool IsVisible
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenTKWindow));
            return this.nativeWindow!.IsVisible;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenTKWindow));
            this.nativeWindow!.IsVisible = value;
        }
    }

    public WindowState State
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public WindowStyle Style
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public string Title
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenTKWindow));
            return this.nativeWindow!.Title;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenTKWindow));
            this.nativeWindow!.Title = value;
        }
    }

    public void Close()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenTKWindow));
        this.nativeWindow!.Close();
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void ProcessEvents()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(OpenTKWindow));
        this.nativeWindow!.ProcessEvents(0);
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.nativeWindow != null)
        {
            this.nativeWindow.Dispose();
            this.nativeWindow = null;
        }

        this.isDisposed = true;
    }
}
