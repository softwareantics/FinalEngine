// <copyright file="DockViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking;

using System;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Inspectors;
using FinalEngine.Editor.ViewModels.Projects;
using FinalEngine.Editor.ViewModels.Scenes;
using FinalEngine.Editor.ViewModels.Services.Layout;
using Microsoft.Extensions.Logging;

public sealed class DockViewModel : ObservableObject, IDockViewModel
{
    private const string StartupLayoutName = "startup";

    private readonly ILayoutManager layoutManager;

    private readonly ILogger<DockViewModel> logger;

    private ICommand? loadCommand;

    private ICommand? unloadCommand;

    public DockViewModel(
        ILogger<DockViewModel> logger,
        ILayoutManager layoutManager,
        IFactory<IProjectExplorerToolViewModel> projectExplorerFactory,
        IFactory<ISceneHierarchyToolViewModel> sceneHierarchyFactory,
        IFactory<IPropertiesToolViewModel> propertiesFactory,
        IFactory<IConsoleToolViewModel> consoleFactory,
        IFactory<IEntitySystemsToolViewModel> entitySystemsFactory,
        IFactory<ISceneViewPaneViewModel> sceneViewFactory)
    {
        ArgumentNullException.ThrowIfNull(projectExplorerFactory, nameof(projectExplorerFactory));
        ArgumentNullException.ThrowIfNull(sceneHierarchyFactory, nameof(sceneHierarchyFactory));
        ArgumentNullException.ThrowIfNull(propertiesFactory, nameof(propertiesFactory));
        ArgumentNullException.ThrowIfNull(consoleFactory, nameof(consoleFactory));
        ArgumentNullException.ThrowIfNull(entitySystemsFactory, nameof(entitySystemsFactory));
        ArgumentNullException.ThrowIfNull(sceneViewFactory, nameof(sceneViewFactory));

        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.layoutManager = layoutManager ?? throw new ArgumentNullException(nameof(layoutManager));

        this.logger.LogInformation("Creating tool views...");

        this.Tools =
        [
            projectExplorerFactory.Create(),
            sceneHierarchyFactory.Create(),
            propertiesFactory.Create(),
            consoleFactory.Create(),
            entitySystemsFactory.Create(),
        ];

        this.logger.LogInformation("Creating pane views...");

        this.Panes =
        [
            sceneViewFactory.Create(),
        ];
    }

    public ICommand LoadCommand
    {
        get { return this.loadCommand ??= new RelayCommand(this.Load); }
    }

    public IEnumerable<IPaneViewModel> Panes { get; }

    public IEnumerable<IToolViewModel> Tools { get; }

    public ICommand UnloadCommand
    {
        get { return this.unloadCommand ??= new RelayCommand(this.Unload); }
    }

    private void Load()
    {
        this.logger.LogInformation("Loading the window layout...");

        if (!this.layoutManager.ContainsLayout(StartupLayoutName))
        {
            this.logger.LogInformation("No startup window layout was found, resolving to default layout...");
            this.layoutManager.ResetLayout();

            return;
        }

        this.layoutManager.LoadLayout(StartupLayoutName);
    }

    private void Unload()
    {
        this.logger.LogInformation("Saving the startup window layout...");
        this.layoutManager.SaveLayout(StartupLayoutName);
    }
}
