// <copyright file="EngineDriverTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Runtime;

using System;
using NSubstitute;
using NUnit.Framework;
using FinalEngine.Platform;
using FinalEngine.Runtime;

[TestFixture]
internal sealed class EngineDriverTests
{
    private EngineDriver engineDriver;

    private IEventsProcessor eventsProcessor;

    private IWindow window;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenEventsProcessorIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new EngineDriver(this.window, null);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenWindowIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new EngineDriver(null, this.eventsProcessor);
        });
    }

    [Test]
    public void DisposeShouldBeIdempotent()
    {
        // Act
        this.engineDriver.Dispose();
        this.engineDriver.Dispose();

        // Assert
        this.window.Received(1).Dispose();
    }

    [Test]
    public void DisposeShouldDisposeWindow()
    {
        // Act
        this.engineDriver.Dispose();

        // Assert
        this.window.Received(1).Dispose();
    }

    [Test]
    public void DisposeShouldDisposeWindowAndSetWindowToNull()
    {
        // Arrange
        var windowField = typeof(EngineDriver).GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        this.engineDriver.Dispose();

        // Assert
        this.window.Received(1).Dispose();
        Assert.That(windowField.GetValue(this.engineDriver), Is.Null);
    }

    [Test]
    public void DisposeShouldNotDisposeWindowWhenDisposingIsFalse()
    {
        // Arrange
        var windowField = typeof(EngineDriver).GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Act
        typeof(EngineDriver)
            .GetMethod("Dispose", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(this.engineDriver, [false]);

        // Assert
        this.window.DidNotReceive().Dispose();
        Assert.That(windowField.GetValue(this.engineDriver), Is.Not.Null);
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.window = Substitute.For<IWindow>();
        this.eventsProcessor = Substitute.For<IEventsProcessor>();
        this.engineDriver = new EngineDriver(this.window, this.eventsProcessor);
    }

    [Test]
    public void StartShouldReturnWhenAlreadyRunning()
    {
        // Arrange
        var isRunningField = typeof(EngineDriver).GetField("isRunning", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
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
        var isRunningField = typeof(EngineDriver).GetField("isRunning", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
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
        this.engineDriver.Dispose();
    }
}
