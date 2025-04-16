// <copyright file="DeleteEntityCommand.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Commands.Entities;

using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Messages.Entities;

internal sealed class DeleteEntityCommand
{
    private readonly Entity entity;

    private readonly IMessenger messenger;

    private readonly ISceneManager sceneManager;

    public DeleteEntityCommand(ISceneManager sceneManager, IMessenger messenger, Entity entity)
    {
        this.sceneManager = sceneManager ?? throw new System.ArgumentNullException(nameof(sceneManager));
        this.messenger = messenger ?? throw new System.ArgumentNullException(nameof(messenger));
        this.entity = entity ?? throw new System.ArgumentNullException(nameof(entity));
    }

    public void Execute()
    {
        this.sceneManager.Scene.RemoveEntity(this.entity);
        this.messenger.Send(new EntityDeletedMessage(this.entity));
    }
}
