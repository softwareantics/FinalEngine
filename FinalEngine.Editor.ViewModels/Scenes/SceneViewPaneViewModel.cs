// <copyright file="SceneViewPaneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System;
using System.Drawing;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Rendering;
using Microsoft.Extensions.Logging;

public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    private static bool isInitialized;

    private readonly IPipeline pipeline;

    private readonly ISceneManager sceneManager;

    private ICommand? renderCommand;

    private IRelayCommand<Rectangle>? updateViewCommand;

    public SceneViewPaneViewModel(
        ILogger<SceneViewPaneViewModel> logger,
        ISceneManager sceneManager,
        IPipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
        this.pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));

        this.Title = "Scene View";
        this.ContentID = "SceneView";

        logger.LogInformation($"Initializing {this.Title}...");
    }

    public ICommand RenderCommand
    {
        get { return this.renderCommand ??= new RelayCommand<int>(this.Render); }
    }

    public IRelayCommand<Rectangle> UpdateViewCommand
    {
        get { return this.updateViewCommand ??= new RelayCommand<Rectangle>(this.UpdateView); }
    }

    private void Render(int defaultFrameBuffer)
    {
        if (!isInitialized)
        {
            this.pipeline.SetDefaultFrameBufferTarget(defaultFrameBuffer);

            this.sceneManager.Initialize();
            isInitialized = true;
        }

        this.sceneManager.Update();
        this.sceneManager.Render();
    }

    private void UpdateView(Rectangle viewport)
    {
        this.sceneManager.SetViewport(viewport);
    }
}
