// <copyright file="ISceneViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Panes;

using System.Drawing;
using System.Windows.Input;

public interface ISceneViewModel
{
    ICommand RenderCommand { get; }

    Size ProjectionSize { get; }
}