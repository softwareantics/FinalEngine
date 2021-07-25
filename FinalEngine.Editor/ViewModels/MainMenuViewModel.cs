// <copyright file="MainMenuViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.Windows.Input;
    using FinalEngine.Editor.Commands;

    public class MainMenuViewModel : ViewModelBase, IMainMenuViewModel
    {
        public MainMenuViewModel()
        {
            this.ExitCommand = new RelayCommand(o => Environment.Exit(0));
        }

        public ICommand ExitCommand { get; }
    }
}