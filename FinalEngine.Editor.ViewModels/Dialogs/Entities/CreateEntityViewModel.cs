// <copyright file="CreateEntityViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using Microsoft.Extensions.Logging;

public sealed class CreateEntityViewModel : ObservableValidator, ICreateEntityViewModel
{
    private readonly ILogger<CreateEntityViewModel> logger;

    private readonly ISceneManager sceneManager;

    private IRelayCommand? createCommand;

    private string? entityName;

    public CreateEntityViewModel(
        ILogger<CreateEntityViewModel> logger,
        ISceneManager sceneManager)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));

        this.EntityName = "Entity";
        this.EntityGuid = Guid.NewGuid();
    }

    public IRelayCommand CreateCommand
    {
        get
        {
            return this.createCommand ??= new RelayCommand<ICloseable>(this.Create, x =>
            {
                return !this.HasErrors;
            });
        }
    }

    public Guid EntityGuid { get; }

    [Required(ErrorMessage = "You must provide an entity name.")]
    public string EntityName
    {
        get
        {
            return this.entityName ?? string.Empty;
        }

        set
        {
            this.SetProperty(ref this.entityName, value, true);
            this.CreateCommand.NotifyCanExecuteChanged();
        }
    }

    public string Title
    {
        get { return "Create New Entity"; }
    }

    private void Create(ICloseable? closeable)
    {
        ArgumentNullException.ThrowIfNull(closeable, nameof(closeable));

        this.logger.LogInformation($"Creating new entity...");

        var scene = this.sceneManager.ActiveScene;
        var entity = new Entity();

        entity.AddComponent(new TagComponent()
        {
            Name = this.EntityName,
        });

        entity.AddComponent<TransformComponent>();

        scene.AddEntity(entity);

        this.logger.LogInformation($"Entity with ID: '{this.EntityGuid}' created!");

        closeable.Close();
    }
}
