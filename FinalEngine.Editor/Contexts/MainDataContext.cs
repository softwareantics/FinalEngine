// <copyright file="MainDataContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Contexts
{
    using System;

    public class MainDataContext : DataContextBase, IMainDataContext
    {
        public MainDataContext(ISceneDataContext sceneDataContext)
        {
            this.SceneDataContext = sceneDataContext ?? throw new ArgumentNullException(nameof(sceneDataContext), $"The specified {nameof(sceneDataContext)} parameter cannot be null.");
        }

        public ISceneDataContext SceneDataContext { get; }
    }
}