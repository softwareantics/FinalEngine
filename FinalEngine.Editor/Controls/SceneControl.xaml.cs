// <copyright file="SceneControl.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Controls
{
    using System.Windows.Controls;
    using OpenTK.Windowing.Common;
    using OpenTK.Wpf;

    /// <summary>
    ///   Interaction logic for SceneControl.xaml.
    /// </summary>
    public partial class SceneControl : UserControl
    {
        public SceneControl()
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

            this.openTKControl.Start(settings);
        }
    }
}