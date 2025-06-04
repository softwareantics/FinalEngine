// <copyright file="WinFormsWindow.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop;

using AutoMapper;
using FinalEngine.Platform.Desktop.Invocation.Forms;
using FinalEngine.Platform.Desktop.Invocation.Native;
using FinalEngine.Platform;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides a Windows Forms implementation of the <see cref="IWindow"/> interface for managing a window on desktop platforms.
/// </summary>
///
/// <remarks>
/// This class wraps a Windows Forms <see cref="IFormAdapter"/> to implement the <see cref="IWindow"/> contract, allowing control over window properties such as size, style, state, visibility, and title. It handles platform-specific behaviors such as switching to fullscreen by changing the window style to borderless to prevent taskbar display issues. The class also manages resource cleanup and disposal of the underlying form.
/// </remarks>
/// <inheritdoc cref="IWindow" />
internal sealed class WinFormsWindow : IWindow
{
    /// <summary>
    /// Specifies an <see cref="ILogger{TCategoryName}"/> that is used for logging purposes.
    /// </summary>
    private readonly ILogger<WinFormsWindow> logger;

    /// <summary>
    /// Specifies an <see cref="IMapper"/> that is used to translate between platform-specific and engine-specific types.
    /// </summary>
    private readonly IMapper mapper;

    /// <summary>
    /// Specifies an <see cref="INativeAdapter"/> that is used to perform Windows API calls related to window management.
    /// </summary>
    private readonly INativeAdapter nativeAdapter;

    /// <summary>
    /// Specifies an <see cref="IFormAdapter"/> that represents the underlying Windows Forms window.
    /// </summary>
    private IFormAdapter? form;

    /// <summary>
    /// Indicates whether the <see cref="WinFormsWindow"/> instance has been disposed.
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="WinFormsWindow"/> class.
    /// </summary>
    ///
    /// <param name="logger">
    /// Specifies an <see cref="ILogger{TCategoryName}"/> that is used for logging purposes.
    /// </param>
    ///
    /// <param name="form">
    /// Specifies an <see cref="IFormAdapter"/> that represents the underlying window.
    /// </param>
    ///
    /// <param name="nativeAdapter">
    /// Specifies an <see cref="INativeAdapter"/> that represents an adapter for Windows API calls.
    /// </param>
    ///
    /// <param name="mapper">
    /// Specifies an <see cref="IMapper"/> that represents the mapper used to translate between platform and engine types.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown when the one of the following parameters are null:
    /// <list type="bullet">
    ///     <item>
    ///         <paramref name="logger"/>
    ///     </item>
    ///     <item>
    ///         <paramref name="form"/>
    ///     </item>
    ///     <item>
    ///         <paramref name="nativeAdapter"/>
    ///     </item>
    ///     <item>
    ///         <paramref name="mapper"/>
    ///     </item>
    /// </list>
    /// </exception>
    public WinFormsWindow(ILogger<WinFormsWindow> logger, IFormAdapter form, INativeAdapter nativeAdapter, IMapper mapper)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.form = form ?? throw new ArgumentNullException(nameof(form));
        this.nativeAdapter = nativeAdapter ?? throw new ArgumentNullException(nameof(nativeAdapter));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        this.form.FormClosed += this.Form_FormClosed;

        this.Title = "Final Engine";

        this.form.StartPosition = FormStartPosition.CenterScreen;
        this.IsUserReSizable = false;
        this.ClientSize = new Size(1280, 720);

        this.IsVisible = true;

        this.logger.LogInformation("Window initialized: Size={Size}, Title='{Title}'", this.ClientSize, this.Title);
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="WinFormsWindow"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    ~WinFormsWindow()
    {
        this.Dispose(false);
    }

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="WinFormsWindow"/> instance has been disposed.
    /// </exception>
    public Size ClientSize
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            return this.form!.ClientSize;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            this.form!.ClientSize = value;
        }
    }

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="WinFormsWindow"/> instance has been disposed.
    /// </exception>
    public bool IsUserReSizable
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            return this.form!.MaximizeBox;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            this.form!.MaximizeBox = value;
        }
    }

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="WinFormsWindow"/> instance has been disposed.
    /// </exception>
    public bool IsVisible
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            return this.form!.Visible;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            this.form!.Visible = value;
        }
    }

    /// <inheritdoc />
    /// <remarks>
    /// When switching to <see cref="WindowState.Fullscreen"/>, this implementation sets the window style to <see cref="WindowStyle.Borderless"/> first to address an issue where the Windows taskbar remains visible otherwise.
    /// </remarks>
    ///
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="WinFormsWindow"/> instance has been disposed.
    /// </exception>
    public WindowState State
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            return this.mapper.Map<WindowState>(this.form!.WindowState);
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));

            this.logger.LogDebug("Setting window state to {State}.", value);

            if (value == WindowState.Fullscreen)
            {
                this.logger.LogInformation("Switching to Fullscreen mode, applying Borderless style.");
                this.Style = WindowStyle.Borderless;
            }
            else if (value == WindowState.Normal)
            {
                this.logger.LogInformation("Switching to Normal mode, applying Fixed style.");
                this.Style = WindowStyle.Fixed;
            }

            this.form!.WindowState = this.mapper.Map<FormWindowState>(value);
            this.logger.LogDebug("{WindowState} changed to {State}", nameof(WindowState), value);
        }
    }

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="WinFormsWindow"/> instance has been disposed.
    /// </exception>
    public WindowStyle Style
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            return this.mapper.Map<WindowStyle>(this.form!.FormBorderStyle);
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            this.logger.LogDebug("Changing window style to {Style}.", value);
            this.form!.FormBorderStyle = this.mapper.Map<FormBorderStyle>(value);
        }
    }

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="WinFormsWindow"/> instance has been disposed.
    /// </exception>
    public string Title
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            return this.form!.Text;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
            this.form!.Text = value;
        }
    }

    /// <inheritdoc />
    /// <exception cref="ObjectDisposedException">
    /// Thrown when the <see cref="WinFormsWindow"/> instance has been disposed.
    /// </exception>
    public void Close()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));

        this.logger.LogInformation("Closing window...");
        this.form!.Close();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    ///
    /// <param name="disposing">
    /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
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

    /// <summary>
    /// Occurs when the form has closed.
    /// </summary>
    ///
    /// <param name="sender">
    /// The sender.
    /// </param>
    ///
    /// <param name="e">
    /// Specifies a <see cref="FormClosedEventArgs"/> instance containing the event data.
    /// </param>
    ///
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified <paramref name="e"/> parameter is null.
    /// </exception>
    private void Form_FormClosed(object? sender, FormClosedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
        this.logger.LogDebug("Form closed. Posting quit message to end message loop.");
        this.nativeAdapter.PostQuitMessage(0);
    }
}
