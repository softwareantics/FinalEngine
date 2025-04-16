// <copyright file="CreateEntityViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Commands.Entities;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using Microsoft.Extensions.Logging;

public sealed class CreateEntityViewModel : ObservableValidator, ICreateEntityViewModel
{
    private readonly ILogger<CreateEntityViewModel> logger;

    private readonly IMessenger messenger;

    private readonly ISceneManager sceneManager;

    private IRelayCommand? createCommand;

    private string? entityName;

    public CreateEntityViewModel(
        ILogger<CreateEntityViewModel> logger,
        ISceneManager sceneManager,
        IMessenger messenger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

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

        var command = new CreateEntityCommand(
            this.sceneManager,
            this.messenger,
            this.EntityName,
            this.EntityGuid);

        command.Execute();

        this.logger.LogInformation($"Entity with ID: '{this.EntityGuid}' created!");

        closeable.Close();
    }
}
