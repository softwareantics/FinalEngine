﻿// <copyright file="IMainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System.Windows.Input;
    using FinalEngine.Editor.ViewModels.Docking;

    /// <summary>
    ///   Defines an interface that represents a main view.
    /// </summary>
    public interface IMainViewModel
    {
        /// <summary>
        ///   Gets the dock view model.
        /// </summary>
        /// <value>
        ///   The dock view model.
        /// </value>
        IDockViewModel DockViewModel { get; }

        /// <summary>
        ///   Gets the exit command.
        /// </summary>
        /// <value>
        ///   The exit command.
        /// </value>
        ICommand ExitCommand { get; }

        /// <summary>
        ///   Gets the new project command.
        /// </summary>
        /// <value>
        ///   The new project command.
        /// </value>
        ICommand NewProjectCommand { get; }

        /// <summary>
        ///   Gets the open project command.
        /// </summary>
        /// <value>
        ///   The open project command.
        /// </value>
        ICommand OpenProjectCommand { get; }

        /// <summary>
        ///   Gets the name of the project that is currently open.
        /// </summary>
        /// <value>
        ///   The name of the project that is currently open.
        /// </value>
        string ProjectName { get; }

        /// <summary>
        ///   Gets the toggle tool window command.
        /// </summary>
        /// <value>
        ///   The toggle tool window command.
        /// </value>
        ICommand ToggleToolWindowCommand { get; }
    }
}