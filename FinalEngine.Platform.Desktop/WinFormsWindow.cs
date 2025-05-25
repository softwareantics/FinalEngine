// <copyright file="WinFormsWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop;

using System.ComponentModel;
using AutoMapper;

/// <summary>
///   Provides a Windows Forms implementation of the <see cref="IWindow"/> interface, enabling window management and customization for desktop applications.
/// </summary>
/// <remarks>
///   This class inherits from <see cref="Form"/> and implements the <see cref="IWindow"/> interface, providing properties and methods to manage the window's state, style, visibility, and title. It uses <see cref="IMapper"/> for converting between enumeration types related to window states and styles.
/// </remarks>
/// <seealso cref="Form"/>
/// <seealso cref="IWindow"/>
internal sealed class WinFormsWindow : Form, IWindow
{
    /// <summary>
    ///   The mapper instance used for converting between enumeration types.
    /// </summary>
    private readonly IMapper mapper;

    /// <summary>
    ///   Initializes a new instance of the <see cref="WinFormsWindow"/> class.
    /// </summary>
    /// <param name="mapper">
    ///   The mapper instance used for converting between enumeration types.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   Thrown when the <paramref name="mapper"/> parameter is null.
    /// </exception>
    public WinFormsWindow(IMapper mapper)
    {
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        this.StartPosition = FormStartPosition.CenterScreen;
        this.IsUserResizable = false;

        this.ClientSize = new Size(1280, 720);
        this.Visible = true;

        this.Title = "Final Engine";
    }

    /// <summary>
    ///   Gets a value indicating whether this <see cref="WinFormsWindow"/> is closing.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="WinFormsWindow"/> is closing; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsClosing { get; private set; }

    /// <summary>
    ///   Gets or sets a value indicating whether this <see cref="WinFormsWindow"/> can be resized manually by the user.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="WinFormsWindow"/> is user resizable; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    ///   The value of this property determines whether the user can change the size of the window by dragging its edges or corners. If set to <c>false</c>, the window will maintain a fixed size and cannot be resized by the user.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsUserResizable
    {
        get { return this.MaximizeBox; }
        set { this.MaximizeBox = value; }
    }

    /// <summary>
    ///   Gets or sets a value indicating whether this <see cref="WinFormsWindow"/> is visible.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this <see cref="WinFormsWindow"/> is visible; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsVisible
    {
        get { return this.Visible; }
        set { this.Visible = value; }
    }

    /// <summary>
    ///   Gets or sets the state of the window (e.g., normal, minimized, maximized, fullscreen).
    /// </summary>
    /// <value>
    ///   The state of the window (e.g., normal, minimized, maximized, fullscreen).
    /// </value>
    /// <remarks>
    ///   When set to <see cref="WindowState.Fullscreen"/>, the window should occupy the entire screen, hiding the taskbar and other windows. When set to <see cref="WindowState.Maximized"/>, the window should fill the screen except for the taskbar. The <see cref="WindowState.Normal"/> state indicates that the window is in its default size and position, while <see cref="WindowState.Minimized"/> indicates that the window is minimized to an icon in the taskbar. When the user changes the window from <see cref="WindowState.Fullscreen"/> to <see cref="WindowState.Normal"/> the desired behaviour is not to resize the client area of the display. This functionality is not outside the scope of <see cref="WinFormsWindow"/>.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public WindowState State
    {
        get
        {
            return this.mapper.Map<WindowState>(this.WindowState);
        }

        set
        {
            // Windows has an issue where the task bar will remain shown when switching to fullscreen mode if the FormBorderStyle is not set to borderless first.
            if (value == Platform.WindowState.Fullscreen)
            {
                this.Style = WindowStyle.Borderless;
            }

            this.WindowState = this.mapper.Map<FormWindowState>(value);
        }
    }

    /// <summary>
    ///   Gets or sets the style of the window (e.g., fixed, resizable, borderless).
    /// </summary>
    /// <value>
    ///   The style of the window (e.g., fixed, resizable, borderless).
    /// </value>
    /// <remarks>
    ///   When set to <see cref="WindowStyle.Fixed"/>, the window cannot be resized by the user, and should include a title bar and system buttons. When set to <see cref="WindowStyle.Resizable"/>, the user can change the size of the window by dragging its edges or corners. The <see cref="WindowStyle.Borderless"/> style indicates that the window has no borders or title bar, allowing for a clean, unobtrusive appearance. This style is often used for applications that require a custom border interface or when the window's appearance should not distract from the content.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public WindowStyle Style
    {
        get { return this.mapper.Map<WindowStyle>(this.FormBorderStyle); }
        set { this.FormBorderStyle = this.mapper.Map<FormBorderStyle>(value); }
    }

    /// <summary>
    ///   Gets or sets the title of the <see cref="WinFormsWindow"/>.
    /// </summary>
    /// <value>
    ///   The title of the <see cref="WinFormsWindow"/>.
    /// </value>
    /// <remarks>
    ///   The title is typically displayed in the title bar of the window. If developing for other platforms such as mobile this would be the name of the application as it appears in the app switcher or task manager.
    /// </remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Title
    {
        get { return this.Text; }
        set { this.Text = value; }
    }

    /// <summary>
    ///   Called when the form is closing. This method sets the <see cref="IsClosing"/> property to indicate whether the form is closing or not.
    /// </summary>
    /// <param name="e">
    ///   A <see cref="FormClosingEventArgs"/> that contains the event data.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///   Thrown when the <paramref name="e"/> parameter is null.
    /// </exception>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);

        this.IsClosing = !e.Cancel;
        base.OnFormClosing(e);
    }
}
