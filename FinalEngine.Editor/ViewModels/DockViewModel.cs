// <copyright file="DockViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels
{
    using System.Collections.Generic;

    public class DockViewModel : ViewModelBase, IDockViewModel
    {
        public DockViewModel(
            IConsoleViewModel consoleViewModel,
            IProjectExplorerViewModel projectExplorerViewModel,
            ISceneHierarchyViewModel sceneHierarchyViewModel,
            IPropertiesViewModel propertiesViewModel,
            ISceneViewModel sceneViewModel)
        {
            if (consoleViewModel == null)
            {
                throw new System.ArgumentNullException(nameof(consoleViewModel), $"The specified {nameof(consoleViewModel)} parameter cannot be null.");
            }

            if (projectExplorerViewModel == null)
            {
                throw new System.ArgumentNullException(nameof(projectExplorerViewModel), $"The specified {nameof(projectExplorerViewModel)} parameter cannot be null.");
            }

            if (sceneHierarchyViewModel == null)
            {
                throw new System.ArgumentNullException(nameof(sceneHierarchyViewModel), $"The specified {nameof(sceneHierarchyViewModel)} parameter cannot be null.");
            }

            if (propertiesViewModel == null)
            {
                throw new System.ArgumentNullException(nameof(propertiesViewModel), $"The specified {nameof(propertiesViewModel)} parameter cannot be null.");
            }

            if (sceneViewModel == null)
            {
                throw new System.ArgumentNullException(nameof(sceneViewModel), $"The specified {nameof(sceneViewModel)} parameter cannot be null.");
            }

            this.Anchorables = new IToolViewModel[]
            {
                consoleViewModel,
                projectExplorerViewModel,
                sceneHierarchyViewModel,
                propertiesViewModel,
            };

            this.Documents = new IPaneViewModel[]
            {
                sceneViewModel,
            };
        }

        public IEnumerable<IToolViewModel> Anchorables { get; }

        public IEnumerable<IPaneViewModel> Documents { get; }
    }
}