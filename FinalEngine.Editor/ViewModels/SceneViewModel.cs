// <copyright file="SceneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

using System;
using System.Drawing;
using System.Windows.Input;
using FinalEngine.Editor.Commands;
using FinalEngine.Rendering;

namespace FinalEngine.Editor.ViewModels
{
    public class SceneViewModel : PaneViewModelBase, ISceneViewModel
    {
        private readonly IRenderDevice renderDevice;

        public SceneViewModel(IRenderDevice renderDevice)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice), $"The specified {nameof(renderDevice)} parameter cannot be null.");

            this.InitializeCommand = new RelayCommand(o => this.Initialize());
            this.RenderCommand = new RelayCommand(o => this.Render());
        }

        public ICommand InitializeCommand { get; }

        public ICommand RenderCommand { get; }

        private void Initialize()
        {
            this.renderDevice.Initialize();
        }

        private void Render()
        {
            this.renderDevice.Clear(Color.CornflowerBlue);
        }
    }
}