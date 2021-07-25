// <copyright file="MainView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Views
{
    using System;
    using FinalEngine.Editor.ViewModels;
    using MahApps.Metro.Controls;

    /// <summary>
    ///   Interaction logic for MainView.xaml.
    /// </summary>
    public partial class MainView : MetroWindow
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView(IMainViewModel viewModel)
        {
            this.DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel), $"The specified {nameof(viewModel)} parameter cannot be null.");

            this.InitializeComponent();
        }
    }
}