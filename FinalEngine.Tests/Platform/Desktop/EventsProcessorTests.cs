// <copyright file="EventsProcessorTests.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Platform.Desktop;

using System;
using System.Windows.Forms;
using FinalEngine.Platform;
using FinalEngine.Platform.Adapters.Applications;
using FinalEngine.Platform.Adapters.Native;
using FinalEngine.Platform.Native.Messaging;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

[TestFixture]
internal sealed class EventsProcessorTests
{
    private IApplicationAdapter applicationAdapter;

    private WinFormsEventsProcessor eventsProcessor;

    private ILogger<WinFormsEventsProcessor> logger;

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
        var ex = Assert.Throws<ArgumentNullException>(() => new WinFormsEventsProcessor(this.logger, this.nativeAdapter, null));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("application"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new WinFormsEventsProcessor(null, this.nativeAdapter, this.applicationAdapter));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("logger"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenNativeAdapterIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new WinFormsEventsProcessor(this.logger, null, this.applicationAdapter));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("native"));
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
        this.logger = Substitute.For<ILogger<WinFormsEventsProcessor>>();
        this.eventsProcessor = new WinFormsEventsProcessor(this.logger, this.nativeAdapter, this.applicationAdapter);
    }
}
