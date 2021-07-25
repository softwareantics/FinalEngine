// <copyright file="DockViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class DockViewModel : ViewModelBase, IDockViewModel
    {
        public DockViewModel(IConsoleViewModel consoleViewModel, IProjectExplorerViewModel projectExplorerViewModel, ISceneHierarchyViewModel sceneHierarchyViewModel, IPropertiesViewModel propertiesViewModel, ISceneViewModel sceneViewModel)
        {
            this.ConsoleViewModel = consoleViewModel ?? throw new ArgumentNullException(nameof(consoleViewModel), $"The specified {nameof(consoleViewModel)} parameter cannot be null.");
            this.ProjectExplorerViewModel = projectExplorerViewModel ?? throw new ArgumentNullException(nameof(projectExplorerViewModel), $"The specified {nameof(projectExplorerViewModel)} parameter cannot be null.");
            this.SceneHierarchyViewModel = sceneHierarchyViewModel ?? throw new ArgumentNullException(nameof(sceneHierarchyViewModel), $"The specified {nameof(sceneHierarchyViewModel)} parameter cannot be null.");
            this.PropertiesViewModel = propertiesViewModel ?? throw new ArgumentNullException(nameof(propertiesViewModel), $"The specified {nameof(propertiesViewModel)} parameter cannot be null.");
            this.SceneViewModel = sceneViewModel ?? throw new ArgumentNullException(nameof(sceneViewModel), $"The specified {nameof(sceneViewModel)} parameter cannot be null.");

            this.Anchorables = new IToolViewModel[]
            {
                this.ConsoleViewModel,
                this.ProjectExplorerViewModel,
                this.SceneHierarchyViewModel,
                this.PropertiesViewModel,
            };

            this.Documents = new IPaneViewModel[]
            {
                this.SceneViewModel,
            };
        }

        public IEnumerable<IToolViewModel> Anchorables { get; }

        public IConsoleViewModel ConsoleViewModel { get; }

        public IEnumerable<IPaneViewModel> Documents { get; }

        public IProjectExplorerViewModel ProjectExplorerViewModel { get; }

        public IPropertiesViewModel PropertiesViewModel { get; }

        public ISceneHierarchyViewModel SceneHierarchyViewModel { get; }

        public ISceneViewModel SceneViewModel { get; }
    }
}