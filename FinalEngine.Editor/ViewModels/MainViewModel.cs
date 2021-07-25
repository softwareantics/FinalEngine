// <copyright file="MainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;

    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public MainViewModel(IMainMenuViewModel mainMenuViewModel, IDockViewModel dockViewModel)
        {
            this.MainMenuViewModel = mainMenuViewModel ?? throw new ArgumentNullException(nameof(mainMenuViewModel), $"The specified {nameof(mainMenuViewModel)} parameter cannot be null.");
            this.DockViewModel = dockViewModel ?? throw new ArgumentNullException(nameof(dockViewModel), $"The specified {nameof(dockViewModel)} parameter cannot be null.");
        }

        public IDockViewModel DockViewModel { get; }

        public IMainMenuViewModel MainMenuViewModel { get; }
    }
}