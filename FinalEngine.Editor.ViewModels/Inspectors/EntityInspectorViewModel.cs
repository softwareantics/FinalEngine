// <copyright file="EntityInspectorViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Editor.ViewModels.Messages.Entities;
using FinalEngine.Editor.ViewModels.Services.Entities;
using FinalEngine.Physics.Components;
using FinalEngine.Rendering.Components;

public sealed class EntityInspectorViewModel : ObservableObject, IEntityInspectorViewModel
{
    private readonly ObservableCollection<IEntityComponentCategoryViewModel> categorizedComponentTypes;

    private readonly ObservableCollection<IEntityComponentViewModel> componentViewModels;

    private readonly Entity entity;

    private readonly IMessenger messenger;

    private readonly IEntityComponentTypeResolver typeResolver;

    public EntityInspectorViewModel(IMessenger messenger, IEntityComponentTypeResolver typeResolver, Entity entity)
    {
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.typeResolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
        this.entity = entity ?? throw new ArgumentNullException(nameof(entity));

        this.componentViewModels = [];
        this.categorizedComponentTypes = [];

        this.messenger.Register<EntityModifiedMessage>(this, this.HandleEntityModified);

        this.InitializeEntityComponents();
        this.IntializeComponentTypes();
    }

    public ICollection<IEntityComponentCategoryViewModel> CategorizedComponentTypes
    {
        get { return this.categorizedComponentTypes; }
    }

    public ICollection<IEntityComponentViewModel> ComponentViewModels
    {
        get { return this.componentViewModels; }
    }

    private void HandleEntityModified(object recipient, EntityModifiedMessage message)
    {
        if (!ReferenceEquals(this.entity, message.Entity))
        {
            return;
        }

        this.InitializeEntityComponents();
        this.IntializeComponentTypes();
    }

    private void InitializeComponentTypesFromAssembly<T>()
        where T : IEntityComponent
    {
        var assembly = Assembly.GetAssembly(typeof(T)) ?? throw new TypeAccessException("Failed to initialize core engine components.");

        var categoryToTypeMap = this.typeResolver.GetCategorizedTypes(assembly);

        foreach (var kvp in categoryToTypeMap)
        {
            var typeViewModels = kvp.Value.Select(x =>
            {
                return new EntityComponentTypeViewModel(this.messenger, this.entity, x);
            });

            this.categorizedComponentTypes.Add(new EntityComponentCategoryViewModel(kvp.Key, typeViewModels));
        }
    }

    private void InitializeEntityComponents()
    {
        this.componentViewModels.Clear();

        foreach (var component in this.entity.Components)
        {
            this.componentViewModels.Add(new EntityComponentViewModel(this.messenger, this.entity, component));
        }
    }

    private void IntializeComponentTypes()
    {
        this.categorizedComponentTypes.Clear();

        this.InitializeComponentTypesFromAssembly<TagComponent>();
        this.InitializeComponentTypesFromAssembly<MeshComponent>();
        this.InitializeComponentTypesFromAssembly<VelocityComponent>();
    }
}
