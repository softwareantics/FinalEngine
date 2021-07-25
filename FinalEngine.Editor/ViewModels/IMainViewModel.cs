// <copyright file="IMainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    public interface IMainViewModel
    {
        IDockViewModel DockViewModel { get; }

        IMainMenuViewModel MainMenuViewModel { get; }
    }
}