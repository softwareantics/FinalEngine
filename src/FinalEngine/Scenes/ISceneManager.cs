// <copyright file="ISceneManager.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes;

public interface ISceneManager : IDisposable
{
    IScene? CurrentScene { get; }

    void LoadScene(IScene scene);
}
