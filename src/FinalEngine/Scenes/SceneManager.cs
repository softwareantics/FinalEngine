// <copyright file="SceneManager.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Scenes;

public sealed class SceneManager : ISceneManager
{
    private static ISceneManager? instance;

    private bool isDisposed;

    private SceneManager()
    {
    }

    ~SceneManager()
    {
        this.Dispose(false);
    }

    public static ISceneManager Instance
    {
        get { return instance ??= new SceneManager(); }
    }

    public IScene? CurrentScene { get; private set; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void LoadScene(IScene scene)
    {
        ArgumentNullException.ThrowIfNull(scene);

        if (this.CurrentScene != null)
        {
            this.CurrentScene.UnloadContent();
            this.CurrentScene.Dispose();
        }

        scene.Initialize();
        scene.LoadContent();

        this.CurrentScene = scene;
    }

    private void Dispose(bool disposing)
    {
        if (this.isDisposed)
        {
            return;
        }

        if (disposing && this.CurrentScene != null)
        {
            this.CurrentScene.UnloadContent();
            this.CurrentScene.Dispose();
        }

        this.isDisposed = true;
    }
}
