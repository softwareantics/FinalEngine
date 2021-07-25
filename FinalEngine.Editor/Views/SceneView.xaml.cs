// <copyright file="SceneView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Controls;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Windowing.Common;
    using OpenTK.Wpf;

    /// <summary>
    ///   Interaction logic for SceneView.xaml.
    /// </summary>
    public partial class SceneView : UserControl
    {
        public SceneView()
        {
            this.InitializeComponent();

            var settings = new GLWpfControlSettings()
            {
                MajorVersion = 4,
                MinorVersion = 5,
                GraphicsProfile = ContextProfile.Core,
                GraphicsContextFlags = ContextFlags.ForwardCompatible,
                RenderContinuously = true,
            };

            this.glwpfControl.Start(settings);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Debug.WriteLine(this.DataContext?.GetType().ToString());
        }

        private void glwpfControl_Render(System.TimeSpan obj)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.CadetBlue);
        }
    }
}