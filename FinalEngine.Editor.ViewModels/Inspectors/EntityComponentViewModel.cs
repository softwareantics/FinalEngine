// <copyright file="EntityComponentViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Editor.ViewModels.Editing.DataTypes;
using FinalEngine.Editor.ViewModels.Messages.Entities;

public sealed class EntityComponentViewModel : ObservableObject, IEntityComponentViewModel
{
    private readonly IEntityComponent component;

    private readonly Entity entity;

    private readonly IMessenger messenger;

    private readonly ObservableCollection<ObservableObject> propertyViewModels;

    private bool isVisible;

    private ICommand? removeCommand;

    private ICommand? toggleCommand;

    public EntityComponentViewModel(IMessenger messenger, Entity entity, IEntityComponent component)
    {
        this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        this.entity = entity ?? throw new ArgumentNullException(nameof(entity));
        this.component = component ?? throw new ArgumentNullException(nameof(component));

        this.propertyViewModels = [];

        this.Name = component.GetType().Name;
        this.IsVisible = true;

        foreach (var property in component.GetType().GetProperties().OrderBy(x =>
        {
            return x.Name;
        }))
        {
            if (property.GetSetMethod() == null || property.GetGetMethod() == null)
            {
                continue;
            }

            var type = property.PropertyType;
            var browsable = property.GetCustomAttribute<BrowsableAttribute>();

            if (browsable != null && !browsable.Browsable)
            {
                continue;
            }

            switch (type.Name.ToUpperInvariant())
            {
                case "STRING":
                    this.propertyViewModels.Add(new StringPropertyViewModel(component, property));
                    break;

                case "BOOLEAN":
                    this.propertyViewModels.Add(new BoolPropertyViewModel(component, property));
                    break;

                case "INT32":
                    this.propertyViewModels.Add(new IntPropertyViewModel(component, property));
                    break;

                case "DOUBLE":
                    this.propertyViewModels.Add(new DoublePropertyViewModel(component, property));
                    break;

                case "SINGLE":
                    this.propertyViewModels.Add(new FloatPropertyViewModel(component, property));
                    break;

                case "VECTOR2":
                    this.propertyViewModels.Add(new Vector2PropertyViewModel(component, property));
                    break;

                case "VECTOR3":
                    this.propertyViewModels.Add(new Vector3PropertyViewModel(component, property));
                    break;

                case "VECTOR4":
                    this.propertyViewModels.Add(new Vector4PropertyViewModel(component, property));
                    break;

                case "QUATERNION":
                    this.propertyViewModels.Add(new QuaternionPropertyViewModel(component, property));
                    break;

                default:
                    break;
                    //throw new PropertyTypeNotFoundException(type.Name);
            }
        }
    }

    public bool IsVisible
    {
        get { return this.isVisible; }
        private set { this.SetProperty(ref this.isVisible, value); }
    }

    public string Name { get; }

    public ICollection<ObservableObject> PropertyViewModels
    {
        get { return this.propertyViewModels; }
    }

    public ICommand RemoveCommand
    {
        get { return this.removeCommand ??= new RelayCommand(this.Remove, this.CanRemove); }
    }

    public ICommand ToggleCommand
    {
        get { return this.toggleCommand ??= new RelayCommand(this.Toggle); }
    }

    private bool CanRemove()
    {
        return this.component.GetType() != typeof(TagComponent);
    }

    private void Remove()
    {
        if (!this.entity.ContainsComponent(this.component))
        {
            throw new InvalidOperationException($"The {nameof(Entity)} provided to this instance does not contain an {nameof(IEntityComponent)} of type: '{this.component.GetType()}'");
        }

        this.entity.RemoveComponent(this.component);
        this.messenger.Send(new EntityModifiedMessage(this.entity));
    }

    private void Toggle()
    {
        this.IsVisible = !this.IsVisible;
    }
}
