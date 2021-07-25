// <copyright file="IConsoleViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

using System.Windows.Input;

namespace FinalEngine.Editor.ViewModels
{
    public interface IConsoleViewModel : IToolViewModel
    {
        ICommand Clicked { get; }
    }
}