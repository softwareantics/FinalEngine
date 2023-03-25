// <copyright file="IToolViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools;

using FinalEngine.Editor.ViewModels.Docking.Panes;

/// <summary>
///   Defines an interface that represents a tool window view model.
/// </summary>
/// <remarks>
///   A tool view is a view which is used as part of a dockable layout system. It represents any element that can be docked to the tool section of a dockable layout.
/// </remarks>
/// <seealso cref="IPaneViewModel"/>
public interface IToolViewModel : IPaneViewModel
{
    public bool IsVisible { get; set; }
}