// <copyright file="SceneHierarchyToolViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Commands.Entities;
using FinalEngine.Editor.ViewModels.Docking.Tools;
using FinalEngine.Editor.ViewModels.Messages.Entities;
using Microsoft.Extensions.Logging;

public sealed class SceneHierarchyToolViewModel : ToolViewModelBase, ISceneHierarchyToolViewModel
{
    private readonly ObservableCollection<Entity> entities;

    private readonly ILogger<SceneHierarchyToolViewModel> logger;

    private readonly IMessenger messenger;

    private readonly ISceneManager sceneManager;

    private IRelayCommand? deleteEntityCommand;

    private Entity? selectedEntity;

    public SceneHierarchyToolViewModel(
        ILogger<SceneHierarchyToolViewModel> logger,
        ISceneManager sceneManager,
        IMessenger messenger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.entities = [];

        this.Title = "Scene Hierarchy";
        this.ContentID = "SceneHierarchy";

        this.logger.LogInformation($"Initializing {this.Title}...");

        this.messenger.Register<EntityCreatedMessage>(this, this.HandlEntityCreated);
        this.messenger.Register<EntityDeletedMessage>(this, this.HandleEntityDeleted);
    }

    public IRelayCommand DeleteEntityCommand
    {
        get { return this.deleteEntityCommand ??= new RelayCommand(this.DeleteEntity, this.CanDeleteEntity); }
    }

    public IReadOnlyCollection<Entity> Entities
    {
        get { return this.entities; }
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

        var command = new DeleteEntityCommand(
            this.sceneManager,
            this.messenger,
            this.SelectedEntity);

        command.Execute();
    }

    private void HandleEntityDeleted(object recipient, EntityDeletedMessage message)
    {
        this.entities.Remove(message.Entity);
    }

    private void HandlEntityCreated(object recipient, EntityCreatedMessage message)
    {
        var entity = message.Entity;

        this.entities.Add(entity);
        this.SelectedEntity = entity;
    }
}
