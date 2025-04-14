// <copyright file="GameTimeTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Runtime;

using System;
using FinalEngine.Runtime.Invocation;
using FinalEngine.Utilities;
using Moq;
using NUnit.Framework;

public class GameTimeTests
{
    private const double FrameCap = 120.0d;

    private GameTime gameTime;

    private Mock<IStopwatchInvoker> watch;

    [Test]
    public void CanProcessNextFrameShouldReturnFalseWhenCurrentTimeIsNotEqualToLastTimePlusWaitTime()
    {
        // Act
        bool actual = this.gameTime.CanProcessNextFrame();

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void CanProcessNextFrameShouldReturnTrueWhenCurrentTimeIsGreaterThanLastTimePlusWaitTime()
    {
        // Arrange
        this.watch.SetupGet(x => x.Elapsed).Returns(TimeSpan.FromMilliseconds(8.4d));

        // Act
        bool actual = this.gameTime.CanProcessNextFrame();

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void ConstructorShouldNotThrowExceptionWhenInvoked()
    {
        // Act and assert
        Assert.DoesNotThrow(() =>
        {
            new GameTime(120.0d);
        });
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenWatchIsNull()
    {
        // Arrange, act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            new GameTime(null, FrameCap);
        });
    }

    [Test]
    public void ConstructorShouldThrowDivideByZeroExceptionWhenFrameCapIsEqualToZero()
    {
        // Arrange, act and assert
        Assert.Throws<DivideByZeroException>(() =>
        {
            new GameTime(this.watch.Object, 0.0d);
        });
    }

    [Test]
    public void ConstructorShouldThrowDivideByZeroExceptionWhenFrameCapIsLessThanZero()
    {
        // Arrange, act and assert
        Assert.Throws<DivideByZeroException>(() =>
        {
            new GameTime(this.watch.Object, -0.1d);
        });
    }

    [SetUp]
    public void Setup()
    {
        this.watch = new Mock<IStopwatchInvoker>();
        this.gameTime = new GameTime(this.watch.Object, FrameCap);
    }
}
