// <copyright file="IMainDataContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Contexts
{
    public interface IMainDataContext
    {
        ISceneDataContext SceneDataContext { get; }
    }
}