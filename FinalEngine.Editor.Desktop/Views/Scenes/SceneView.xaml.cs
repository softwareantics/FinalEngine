// <copyright file="SceneView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Scenes;

using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FinalEngine.Editor.Desktop.Framework.Input;
using FinalEngine.Editor.ViewModels.Scenes;
using FinalEngine.Utilities;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;

public partial class SceneView : UserControl
{
    private readonly IGameTime gameTime;

    static SceneView()
    {
        FocusableProperty.OverrideMetadata(typeof(GLWpfControl), new FrameworkPropertyMetadata(true));
    }

    public SceneView()
    {
        this.InitializeComponent();

        this.glWpfControl.CanInvokeOnHandledEvents = false;
        this.glWpfControl.RegisterToEventsDirectly = false;

        this.glWpfControl.Focusable = true;

        this.glWpfControl.SizeChanged += this.GlWpfControl_SizeChanged;
        this.glWpfControl.Render += this.GlWpfControl_Render;

        this.glWpfControl.MouseDown += this.GlWpfControl_MouseDown;
        this.glWpfControl.MouseEnter += this.GlWpfControl_MouseEnter;
        this.glWpfControl.MouseLeave += this.GlWpfControl_MouseLeave;

        this.glWpfControl.Start(new GLWpfControlSettings()
        {
            MajorVersion = 4,
            MinorVersion = 6,
            GraphicsProfile = ContextProfile.Core,
            GraphicsContextFlags = ContextFlags.ForwardCompatible,
            RenderContinuously = true,
            UseDeviceDpi = true,
        });

        this.gameTime = new GameTime(120.0d);

        KeyboardDevice.Initialize(this.glWpfControl);
        MouseDevice.Initialize(this.glWpfControl);
    }

    internal static WPFKeyboardDevice KeyboardDevice { get; } = new WPFKeyboardDevice();

    internal static WPFMouseDevice MouseDevice { get; } = new WPFMouseDevice();

    private void GlWpfControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        this.glWpfControl.Focus();
    }

    private void GlWpfControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        this.glWpfControl.Focus();
    }

    private void GlWpfControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        var scope = FocusManager.GetFocusScope(this.glWpfControl);
        FocusManager.SetFocusedElement(scope, null);
        Keyboard.ClearFocus();
    }

    private void GlWpfControl_Render(System.TimeSpan obj)
    {
        if (!this.gameTime.CanProcessNextFrame())
        {
            return;
        }

        if (this.DataContext is ISceneViewPaneViewModel vm)
        {
            vm.RenderCommand.Execute(this.glWpfControl.Framebuffer);
        }
    }

    private void GlWpfControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (!this.gameTime.CanProcessNextFrame())
        {
            return;
        }

        if (this.DataContext is ISceneViewPaneViewModel vm)
        {
            int w = (int)e.NewSize.Width;
            int h = (int)e.NewSize.Height;

            vm.UpdateViewCommand.Execute(new Rectangle(0, 0, w, h));
        }
    }
}
