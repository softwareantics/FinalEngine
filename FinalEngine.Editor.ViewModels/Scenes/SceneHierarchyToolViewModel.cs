// <copyright file="SceneHierarchyToolViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Messages.Entities;
using Microsoft.Extensions.Logging;

public sealed class SceneHierarchyToolViewModel : ToolViewModelBase, ISceneHierarchyToolViewModel
{
    private readonly ILogger<SceneHierarchyToolViewModel> logger;

    private readonly IMessenger messenger;

    private readonly ISceneManager sceneManager;

    private IRelayCommand? deleteEntityCommand;

    private Entity? selectedEntity;

    public SceneHierarchyToolViewModel(
        ILogger<SceneHierarchyToolViewModel> logger,
        IMessenger messenger,
        ISceneManager sceneManager)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));

        this.Title = "Scene Hierarchy";
        this.ContentID = "SceneHierarchy";

        this.logger.LogInformation($"Initializing {this.Title}...");
    }

    public IRelayCommand DeleteEntityCommand
    {
        get { return this.deleteEntityCommand ??= new RelayCommand(this.DeleteEntity, this.CanDeleteEntity); }
    }

    public IReadOnlyCollection<Entity> Entities
    {
        get { return this.sceneManager.ActiveScene.Entities; }
    }

    public Entity? SelectedEntity
    {
        get
        {
            return this.selectedEntity;
        }

        set
        {
            this.SetProperty(ref this.selectedEntity, value);
            this.DeleteEntityCommand.NotifyCanExecuteChanged();

            if (this.SelectedEntity != null)
            {
                this.messenger.Send(new EntitySelectedMessage(this.SelectedEntity));
            }
        }
    }

    private bool CanDeleteEntity()
    {
        return this.SelectedEntity != null;
    }

    private void DeleteEntity()
    {
        if (this.SelectedEntity == null)
        {
            return;
        }

        this.sceneManager.ActiveScene.RemoveEntity(this.SelectedEntity);
        this.messenger.Send(new EntityDeletedMessage());
    }
}
