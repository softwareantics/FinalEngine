﻿// <copyright file="IViewModelFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction
{
    public interface IViewModelFactory
    {
        INewProjectViewModel CreateNewProjectViewModel();
    }
}