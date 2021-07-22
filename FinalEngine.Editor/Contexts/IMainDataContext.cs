// <copyright file="IMainDataContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Contexts
{
    using System.Windows.Input;

    public interface IMainDataContext
    {
        ICommand CloseCommand { get; }

        bool IsClosed { get; }

        ISceneDataContext SceneDataContext { get; }
    }
}