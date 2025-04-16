// <copyright file="CreateEntityCommand.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Commands.Entities;

using System;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Messages.Entities;

internal sealed class CreateEntityCommand
{
    private readonly IMessenger messenger;

    private readonly string name;

    private readonly ISceneManager sceneManager;

    private readonly Guid? uniqueID;

    public CreateEntityCommand(ISceneManager sceneManager, IMessenger messenger, string name, Guid? uniqueID = null)
    {
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.name = name;
        this.uniqueID = uniqueID;
    }

    public void Execute()
    {
        var entity = new Entity(this.uniqueID);

        entity.AddComponent(new TagComponent(this.name));
        entity.AddComponent(new TransformComponent());

        this.sceneManager.Scene.AddEntity(entity);
        this.messenger.Send(new EntityCreatedMessage(entity));
    }
}
