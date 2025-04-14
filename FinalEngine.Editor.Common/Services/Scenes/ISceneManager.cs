// <copyright file="ISceneManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System.Drawing;
using FinalEngine.ECS;

public interface ISceneManager
{
    IEntityWorld ActiveScene { get; }

    void Initialize();

    void Render();

    void SetViewport(Rectangle viewport);

    void Update();
}
