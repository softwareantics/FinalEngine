// <copyright file="SceneRendererTests.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Editor.Common.Services.Rendering;

using System;
using System.Drawing;
using FinalEngine.Editor.Common.Models.Scenes;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Rendering;
using Moq;
using NUnit.Framework;

[TestFixture]
public sealed class SceneRendererTests
{
    private Mock<IRenderDevice> renderDevice;

    private Mock<IScene> scene;

    private Mock<ISceneManager> sceneManager;

    private SceneRenderer sceneRenderer;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenRenderDeviceIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneRenderer(null, this.sceneManager.Object);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenSceneManagerIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new SceneRenderer(this.renderDevice.Object, null);
        });
    }

    [Test]
    public void RenderShouldInvokeActiveSceneRenderWhenInvoked()
    {
        // Act
        this.sceneRenderer.Render();

        // Assert
        this.scene.Verify(x => x.Render(), Times.Once);
    }

    [Test]
    public void RenderShouldInvokeRenderDeviceClearWhenInvoked()
    {
        // Act
        this.sceneRenderer.Render();

        // Assert
        this.renderDevice.Verify(x => x.Clear(Color.FromArgb(255, 30, 30, 30), 1, 0), Times.Once);
    }

    [SetUp]
    public void Setup()
    {
        this.sceneManager = new Mock<ISceneManager>();
        this.renderDevice = new Mock<IRenderDevice>();
        this.scene = new Mock<IScene>();

        this.sceneManager.SetupGet(x => x.ActiveScene).Returns(this.scene.Object);

        this.sceneRenderer = new SceneRenderer(this.renderDevice.Object, this.sceneManager.Object);
    }
}
