// <copyright file="ConsoleViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

using System.Diagnostics;
using System.Windows.Input;
using FinalEngine.Editor.Commands;

namespace FinalEngine.Editor.ViewModels
{
    public class ConsoleViewModel : ToolViewModelBase, IConsoleViewModel
    {
        public ConsoleViewModel()
        {
            this.Title = "Console";
            this.Clicked = new RelayCommand(o => Debug.WriteLine(this.Title));
        }

        public ICommand Clicked { get; }
    }
}