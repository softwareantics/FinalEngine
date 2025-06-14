// <copyright file="Window.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System.Diagnostics.CodeAnalysis;

using AutoMapper;
using FinalEngine.Platform.Adapters.Forms;
using FinalEngine.Platform.Adapters.Native;
using Microsoft.Extensions.Logging;

internal sealed class Window : IWindow
{
    private const int DefaultClientHeight = 720;

    private const int DefaultClientWidth = 1280;

    private readonly ILogger<Window> logger;

    private readonly IMapper mapper;

    private readonly INativeAdapter native;

    private IFormAdapter? form;

    private bool isDisposed;

    public Window(
        ILogger<Window> logger,
        IFormAdapter form,
        INativeAdapter native,
        IMapper mapper)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.form = form ?? throw new ArgumentNullException(nameof(form));
        this.native = native ?? throw new ArgumentNullException(nameof(native));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        this.form.FormClosed += this.Form_FormClosed;

        this.Title = "Final Engine";

        this.form.StartPosition = FormStartPosition.CenterScreen;
        this.IsUserReSizable = false;
        this.ClientSize = new Size(DefaultClientWidth, DefaultClientHeight);

        // Disable double buffering as this is handled by the rendering layer.
        this.form.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

        // Ignore erase background messages to reduce flickering.
        this.form.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

        // Reduce flickering by ensuring the Paint event is not handled by the form itself.
        // Typically this style and the one above should be set to true to reduce flickering.
        // but we handle the rendering in the rendering layer and not in the form itself.
        this.form.SetStyle(ControlStyles.UserPaint, false);

        this.IsVisible = true;

        this.logger.LogInformation("Window initialized: Size={Size}, Title='{Title}'", this.ClientSize, this.Title);
    }

    [ExcludeFromCodeCoverage]
    ~Window()
    {
        this.Dispose(false);
    }

    public Size ClientSize
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            return this.form!.ClientSize;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            this.form!.ClientSize = value;
        }
    }

    public nint Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(Window));
            return this.form!.Handle;
        }
    }

    public bool IsUserReSizable
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            return this.form!.MaximizeBox;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            this.form!.MaximizeBox = value;
        }
    }

    public bool IsVisible
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            return this.form!.Visible;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            this.form!.Visible = value;
        }
    }

    public WindowState State
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            return this.mapper.Map<WindowState>(this.form!.WindowState);
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));

            this.logger.LogDebug("Setting window state to {State}.", value);

            if (value == WindowState.Fullscreen)
            {
                // Switch to Borderless style to ensure the taskbar is hidden.
                this.logger.LogInformation("Switching to Fullscreen mode, applying Borderless style.");
                this.Style = WindowStyle.Borderless;
            }
            else if (value == WindowState.Normal)
            {
                // Switch back to Fixed style when returning to Normal state.
                // At the moment we can assume that the Fixed style is the default style for normal windows.
                this.logger.LogInformation("Switching to Normal mode, applying Fixed style.");
                this.Style = WindowStyle.Fixed;
            }

            this.form!.WindowState = this.mapper.Map<FormWindowState>(value);
            this.logger.LogDebug("{WindowState} changed to {State}", nameof(WindowState), value);
        }
    }

    public WindowStyle Style
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            return this.mapper.Map<WindowStyle>(this.form!.FormBorderStyle);
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            this.logger.LogDebug("Changing window style to {Style}.", value);
            this.form!.FormBorderStyle = this.mapper.Map<FormBorderStyle>(value);
        }
    }

    public string Title
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            return this.form!.Text;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));
            this.form!.Text = value;
        }
    }

    public void Close()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, nameof(Window));

        this.logger.LogInformation("Closing window...");
        this.form!.Close();
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

        if (disposing && this.form != null)
        {
            this.logger.LogTrace("Disposing form and removing event handlers.");

            this.form.FormClosed -= this.Form_FormClosed;
            this.form.Dispose();
            this.form = null;
        }

        this.isDisposed = true;
    }

    private void Form_FormClosed(object? sender, FormClosedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
        this.logger.LogDebug("Form closed. Posting quit message to end message loop.");
        this.native.PostQuitMessage(0);
    }
}
