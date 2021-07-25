// <copyright file="ISceneView.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

using System.Windows.Input;

namespace FinalEngine.Editor.ViewModels
{
    public interface ISceneViewModel : IPaneViewModel
    {
        ICommand InitializeCommand { get; }

        ICommand RenderCommand { get; }
    }
}