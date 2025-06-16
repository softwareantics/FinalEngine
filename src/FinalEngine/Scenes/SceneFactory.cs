// <copyright file="SceneFactory.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes;

internal sealed class SceneFactory : ISceneFactory
{
    private readonly Func<IScene> createScene;

    public SceneFactory(Func<IScene> createScene)
    {
        this.createScene = createScene ?? throw new ArgumentNullException(nameof(createScene));
    }

    public IScene CreateScene()
    {
        return this.createScene();
    }
}
