// <copyright file="WinFormsWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FinalEngine.Platform.Desktop.Invocation;
using FinalEngine.Platform.Desktop.Invocation.Native;
using FinalEngine.Platform.Desktop.Native;

/// <summary>
///   Provides a Windows Forms implementation of the <see cref="IWindow"/> interface, enabling window management and customization for desktop applications.
/// </summary>
/// <remarks>
///   This class inherits from <see cref="Form"/> and implements the <see cref="IWindow"/> interface, providing properties and methods to manage the window's state, style, visibility, and title. It uses <see cref="IMapper"/> for converting between enumeration types related to window states and styles.
/// </remarks>
/// <seealso cref="Form"/>
/// <seealso cref="IWindow"/>
internal sealed class WinFormsWindow : IWindow
{
    /// <summary>
    ///   The mapper instance used for converting between enumeration types.
    /// </summary>
    private readonly IMapper mapper;

    /// <summary>
    ///   The form invoker instance used for invoking form-related operations.
    /// </summary>
    private IFormAdapter? form;

    /// <summary>
    ///   Indicates whether this instance has been disposed of and its resources released.
    /// </summary>
    private bool isDisposed;

    private IPInvokeAdapter nativeAdapter;

    /// <summary> Initializes a new instance of the <see cref="WinFormsWindow"/> class. </summary> <param name="form"> The form invoker instance used for invoking form-related operations. </param> <param name="nativeAdapter"> The native adapter instance used for invoking native operations, such as posting quit messages. </param> <param name="mapper"> The mapper instance used for converting between enumeration types. </param> <exception cref="ArgumentNullException"> Thrown when the <paramref name="form"/>, <paramref name="nativeAdapter"/> or <paramref name="mapper"/> parameter is null. </exception> </exception>
    public WinFormsWindow(IFormAdapter form, IPInvokeAdapter nativeAdapter, IMapper mapper)
    {
        this.form = form ?? throw new ArgumentNullException(nameof(form));
        this.nativeAdapter = nativeAdapter ?? throw new ArgumentNullException(nameof(nativeAdapter));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        this.form.FormClosed += this.Form_FormClosed;
        ;

        this.Title = "Final Engine";

        this.form.StartPosition = FormStartPosition.CenterScreen;
        this.IsUserReSizable = false;
        this.ClientSize = new Size(1280, 720);

        this.IsVisible = true;
    }

    /// <summary>
    ///   Finalizes an instance of the <see cref="WinFormsWindow"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    ~WinFormsWindow()
    {
        this.Dispose(false);
    }

    /// <summary>
    ///   Gets or sets the size of the client (the area inside the window excluding borders and title bar).
    /// </summary>
    /// <value>
    ///   The size of the client (the area inside the window excluding the borders and title bar).
    /// </value>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown when this <see cref="WinFormsWindow"/> has already been disposed of and its resources have been released.
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

    /// <summary>
    ///   Gets or sets a value indicating whether this <see cref="WinFormsWindow"/> can be resized manually by the user.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="WinFormsWindow"/> is user re-sizable; otherwise, <c>false</c>.
    /// </value>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown when this <see cref="WinFormsWindow"/> has already been disposed of and its resources have been released.
    /// </exception>
    /// <remarks>
    ///   The value of this property determines whether the user can change the size of the window by dragging its edges or corners. If set to <c>false</c>, the window will maintain a fixed size and cannot be resized by the user.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    /// <summary>
    ///   Gets or sets a value indicating whether this <see cref="WinFormsWindow"/> is visible.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="WinFormsWindow"/> is visible; otherwise, <c>false</c>.
    /// </value>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown when this <see cref="WinFormsWindow"/> has already been disposed of and its resources have been released.
    /// </exception>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    /// <summary>
    ///   Gets or sets the state of the window (e.g., normal, minimized, maximized, full-screen).
    /// </summary>
    /// <value>
    ///   The state of the window (e.g., normal, minimized, maximized, full-screen).
    /// </value>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown when this <see cref="WinFormsWindow"/> has already been disposed of and its resources have been released.
    /// </exception>
    /// <remarks>
    ///   When set to <see cref="WindowState.Fullscreen"/>, the window should occupy the entire screen, hiding the task-bar and other windows. When set to <see cref="WindowState.Maximized"/>, the window should fill the screen except for the task-bar. The <see cref="WindowState.Normal"/> state indicates that the window is in its default size and position, while <see cref="WindowState.Minimized"/> indicates that the window is minimized to an icon in the task-bar. When the user changes the window from <see cref="WindowState.Fullscreen"/> to <see cref="WindowState.Normal"/> the desired behavior is not to resize the client area of the display. This functionality is not outside the scope of <see cref="WinFormsWindow"/>.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

            // Windows has an issue where the task bar will remain shown when switching to full-screen mode if the FormBorderStyle is not set to border-less first.
            if (value == WindowState.Fullscreen)
            {
                this.Style = WindowStyle.Borderless;
            }

            this.form!.WindowState = this.mapper.Map<FormWindowState>(value);
        }
    }

    /// <summary>
    ///   Gets or sets the style of the window (e.g., fixed, re-sizable, border-less).
    /// </summary>
    /// <value>
    ///   The style of the window (e.g., fixed, re-sizable, border-less).
    /// </value>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown when this <see cref="WinFormsWindow"/> has already been disposed of and its resources have been released.
    /// </exception>
    /// <remarks>
    ///   When set to <see cref="WindowStyle.Fixed"/>, the window cannot be resized by the user, and should include a title bar and system buttons. When set to <see cref="WindowStyle.Resizable"/>, the user can change the size of the window by dragging its edges or corners. The <see cref="WindowStyle.Borderless"/> style indicates that the window has no borders or title bar, allowing for a clean, unobtrusive appearance. This style is often used for applications that require a custom border interface or when the window's appearance should not distract from the content.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
            this.form!.FormBorderStyle = this.mapper.Map<FormBorderStyle>(value);
        }
    }

    /// <summary>
    ///   Gets or sets the title of the <see cref="WinFormsWindow"/>.
    /// </summary>
    /// <value>
    ///   The title of the <see cref="WinFormsWindow"/>.
    /// </value>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown when this <see cref="WinFormsWindow"/> has already been disposed of and its resources have been released.
    /// </exception>
    /// <remarks>
    ///   The title is typically displayed in the title bar of the window. If developing for other platforms such as mobile this would be the name of the application as it appears in the application switcher or task manager.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    /// <summary>
    ///   Closes this <see cref="WinFormsWindow"/> and releases any resources associated with it.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    ///   Thrown when this <see cref="WinFormsWindow"/> has already been disposed of and its resources have been released.
    /// </exception>
    public void Close()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, nameof(WinFormsWindow));
        this.form!.Close();
    }

    /// <summary>
    ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing">
    ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (this.form != null)
            {
                this.form.FormClosed -= this.Form_FormClosed;
                this.form.Dispose();
                this.form = null;
            }
        }

        this.isDisposed = true;
    }

    private void Form_FormClosed(object? sender, FormClosedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
        this.nativeAdapter.PostQuitMessage(0);
    }
}
