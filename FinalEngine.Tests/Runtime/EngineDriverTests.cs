// <copyright file="EngineDriverTests.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Runtime;

using System;
using System.Drawing;
using System.Reflection;
using FinalEngine.Hosting;
using FinalEngine.Platform;
using FinalEngine.Rendering;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

[TestFixture]
internal sealed class EngineDriverTests
{
    private EngineDriver engineDriver;

    private IEventsProcessor eventsProcessor;

    private ILogger<EngineDriver> logger;

    private IRenderContext renderContext;

    private IRenderContext.RenderContextFactory renderContextFactory;

    private IWindow window;

    [Test]
    public void ConstructorShouldInvokeRenderContextFactoryWhenInvoked()
    {
        // Act and assert
        this.renderContextFactory.Received(1).Invoke(Arg.Is<nint>(n => n == this.window.Handle), Arg.Is<Size>(s => s == this.window.ClientSize));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenEventsProcessorIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new EngineDriver(this.logger, this.window, null, this.renderContextFactory));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("eventsProcessor"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new EngineDriver(null, this.window, this.eventsProcessor, this.renderContextFactory));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("logger"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenRenderContextFactoryIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new EngineDriver(this.logger, this.window, this.eventsProcessor, null));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("renderContextFactory"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenWindowIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new EngineDriver(this.logger, null, this.eventsProcessor, this.renderContextFactory));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("window"));
    }

    [Test]
    public void DisposeShouldBeIdempotent()
    {
        // Act
        this.engineDriver.Dispose();
        this.engineDriver.Dispose();

        // Assert
        this.window.Received(1).Dispose();
        this.renderContext.Received(1).Dispose();
    }

    [Test]
    public void DisposeShouldDisposeRenderContextAndSetRenderContextToNull()
    {
        // Arrange
        var windowField = typeof(EngineDriver).GetField("renderContext", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        this.engineDriver.Dispose();

        // Assert
        this.renderContext.Received(1).Dispose();
        Assert.That(windowField.GetValue(this.engineDriver), Is.Null);
    }

    [Test]
    public void DisposeShouldDisposeWindowAndSetWindowToNull()
    {
        // Arrange
        var windowField = typeof(EngineDriver).GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        this.engineDriver.Dispose();

        // Assert
        this.window.Received(1).Dispose();
        Assert.That(windowField.GetValue(this.engineDriver), Is.Null);
    }

    [SetUp]
    public void Setup()
    {
        this.logger = Substitute.For<ILogger<EngineDriver>>();
        this.window = Substitute.For<IWindow>();
        this.eventsProcessor = Substitute.For<IEventsProcessor>();
        this.renderContext = Substitute.For<IRenderContext>();

        this.window.ClientSize.Returns(new Size(800, 600));
        this.window.Handle.Returns(123);

        this.renderContextFactory = Substitute.For<IRenderContext.RenderContextFactory>();
        this.renderContextFactory.Invoke(Arg.Any<nint>(), Arg.Any<Size>()).Returns(this.renderContext);

        this.engineDriver = new EngineDriver(this.logger, this.window, this.eventsProcessor, this.renderContextFactory);
    }

    [Test]
    public void StartShouldReturnWhenAlreadyRunning()
    {
        // Arrange
        var isRunningField = typeof(EngineDriver).GetField("isRunning", BindingFlags.NonPublic | BindingFlags.Instance);
        isRunningField.SetValue(this.engineDriver, true);

        // Act
        this.engineDriver.Start();

        // Assert
        this.eventsProcessor.DidNotReceive().ProcessEvents();
    }

    [Test]
    public void StartShouldRunGameLoopAndProcessEvents()
    {
        // Arrange
        this.eventsProcessor.CanProcessEvents.Returns(true, true, false);

        // Act
        this.engineDriver.Start();

        // Assert
        this.eventsProcessor.Received(2).ProcessEvents();
    }

    [Test]
    public void StartShouldRunGameLoopAndRenderContextShouldBeCurrent()
    {
        // Arrange
        this.eventsProcessor.CanProcessEvents.Returns(true, true, false);

        // Act
        this.engineDriver.Start();

        // Assert
        this.renderContext.Received(1).MakeCurrent();
    }

    [Test]
    public void StartShouldRunGameLoopAndRenderContextSwapBuffersShouldBeCalled()
    {
        // Arrange
        this.eventsProcessor.CanProcessEvents.Returns(true, true, false);

        // Act
        this.engineDriver.Start();

        // Assert
        this.renderContext.Received(2).SwapBuffers();
    }

    [Test]
    public void StartShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.engineDriver.Dispose();

        // Act & Assert
        Assert.Throws<ObjectDisposedException>(this.engineDriver.Start);
    }

    [Test]
    public void StopShouldSetIsRunningToFalseWhenCalled()
    {
        // Arrange
        var isRunningField = typeof(EngineDriver).GetField("isRunning", BindingFlags.NonPublic | BindingFlags.Instance);
        this.engineDriver.Start();

        // Act
        this.engineDriver.Stop();

        // Assert
        Assert.That((bool)isRunningField.GetValue(this.engineDriver), Is.False);
    }

    [Test]
    public void StopShouldThrowObjectDisposedExceptionWhenAlreadyDisposed()
    {
        // Arrange
        this.engineDriver.Dispose();

        // Act & Assert
        Assert.Throws<ObjectDisposedException>(this.engineDriver.Stop);
    }

    [TearDown]
    public void TearDown()
    {
        this.window.Dispose();
        this.renderContext.Dispose();
        this.engineDriver.Dispose();
    }
}
