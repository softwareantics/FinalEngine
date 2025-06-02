// <copyright file="WinFormsEventsProcessorTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Platform.Desktop;

using System;
using System.Windows.Forms;
using FinalEngine.Platform.Desktop;
using FinalEngine.Platform.Desktop.Invocation;
using FinalEngine.Platform.Desktop.Invocation.Native;
using FinalEngine.Platform.Desktop.Native.Messaging;
using NSubstitute;
using NUnit.Framework;

[TestFixture]
internal sealed class WinFormsEventsProcessorTests
{
    private IApplicationAdapter applicationAdapter;

    private WinFormsEventsProcessor eventsProcessor;

    private INativeAdapter nativeAdapter;

    [Test]
    public void CanProcessEventsShouldReturnCurrentState()
    {
        // Assert
        Assert.That(this.eventsProcessor.CanProcessEvents, Is.True);

        // Act
        typeof(WinFormsEventsProcessor)
            .GetProperty(nameof(WinFormsEventsProcessor.CanProcessEvents))
            .SetValue(this.eventsProcessor, false);

        // Assert
        Assert.That(this.eventsProcessor.CanProcessEvents, Is.False);
    }

    [Test]
    public void ConstructorShouldSetCanProcessEventsToTrue()
    {
        // Act & Assert
        Assert.That(this.eventsProcessor.CanProcessEvents, Is.True);
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenApplicationAdapterIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
        {
            new WinFormsEventsProcessor(this.nativeAdapter, null);
        });

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("applicationAdapter"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenNativeAdapterIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
        {
            new WinFormsEventsProcessor(null, this.applicationAdapter);
        });

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("nativeAdapter"));
    }

    [Test]
    public void ProcessEventsShouldNotProcessWhenPeekMessageReturnsZero()
    {
        // Arrange
        this.nativeAdapter.PeekMessage(out Arg.Any<NativeMessage>(), IntPtr.Zero, 0, 0, 0).Returns(0);

        // Act
        this.eventsProcessor.ProcessEvents();

        // Assert
        this.nativeAdapter.DidNotReceiveWithAnyArgs().GetMessage(out Arg.Any<NativeMessage>(), IntPtr.Zero, 0, 0);
    }

    [Test]
    public void ProcessEventsShouldNotTranslateOrDispatchWhenFilterMessageReturnsTrue()
    {
        // Arrange
        this.nativeAdapter.PeekMessage(out Arg.Any<NativeMessage>(), IntPtr.Zero, 0, 0, 0).Returns(1, 0);
        this.nativeAdapter.GetMessage(out Arg.Any<NativeMessage>(), IntPtr.Zero, 0, 0).Returns(ci =>
        {
            ci[0] = default(NativeMessage);
            return 1;
        });

        this.applicationAdapter.FilterMessage(ref Arg.Any<Message>()).Returns(true);

        // Act
        this.eventsProcessor.ProcessEvents();

        // Assert
        this.nativeAdapter.DidNotReceiveWithAnyArgs().TranslateMessage(ref Arg.Any<NativeMessage>());
        this.nativeAdapter.DidNotReceiveWithAnyArgs().DispatchMessage(ref Arg.Any<NativeMessage>());
    }

    [Test]
    public void ProcessEventsShouldReturnImmediatelyWhenCanProcessEventsIsFalse()
    {
        // Arrange
        typeof(WinFormsEventsProcessor)
            .GetProperty(nameof(WinFormsEventsProcessor.CanProcessEvents))
            .SetValue(this.eventsProcessor, false);

        // Act
        this.eventsProcessor.ProcessEvents();

        // Assert
        this.nativeAdapter.DidNotReceiveWithAnyArgs().PeekMessage(out Arg.Any<NativeMessage>(), default, 0, 0, 0);
    }

    [Test]
    public void ProcessEventsShouldSetCanProcessEventsToFalseWhenGetMessageReturnsZero()
    {
        // Arrange
        this.nativeAdapter.PeekMessage(out Arg.Any<NativeMessage>(), IntPtr.Zero, 0, 0, 0).Returns(1, 0);
        this.nativeAdapter.GetMessage(out Arg.Any<NativeMessage>(), IntPtr.Zero, 0, 0).Returns(0);

        // Act
        this.eventsProcessor.ProcessEvents();

        // Assert
        Assert.That(this.eventsProcessor.CanProcessEvents, Is.False);
    }

    [Test]
    public void ProcessEventsShouldThrowInvalidOperationExceptionWhenGetMessageReturnsMinusOne()
    {
        // Arrange
        this.nativeAdapter.PeekMessage(out Arg.Any<NativeMessage>(), IntPtr.Zero, 0, 0, 0).Returns(1, 0);
        this.nativeAdapter.GetMessage(out Arg.Any<NativeMessage>(), IntPtr.Zero, 0, 0).Returns(-1);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(this.eventsProcessor.ProcessEvents);
    }

    [Test]
    public void ProcessEventsShouldTranslateAndDispatchWhenFilterMessageReturnsFalse()
    {
        // Arrange
        this.nativeAdapter.PeekMessage(out Arg.Any<NativeMessage>(), IntPtr.Zero, 0, 0, 0).Returns(1, 0);
        this.nativeAdapter.GetMessage(out Arg.Any<NativeMessage>(), IntPtr.Zero, 0, 0).Returns(ci =>
        {
            ci[0] = default(NativeMessage);
            return 1;
        });

        // Act
        this.eventsProcessor.ProcessEvents();

        // Assert
        this.nativeAdapter.Received(1).TranslateMessage(ref Arg.Any<NativeMessage>());
        this.nativeAdapter.Received(1).DispatchMessage(ref Arg.Any<NativeMessage>());
    }

    [SetUp]
    public void SetUp()
    {
        this.nativeAdapter = Substitute.For<INativeAdapter>();
        this.applicationAdapter = Substitute.For<IApplicationAdapter>();
        this.eventsProcessor = new WinFormsEventsProcessor(this.nativeAdapter, this.applicationAdapter);
    }
}
