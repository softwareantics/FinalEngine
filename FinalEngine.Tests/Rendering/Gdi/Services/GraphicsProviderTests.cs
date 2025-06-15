// <copyright file="GraphicsProviderTests.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.Gdi.Services;

using FinalEngine.Rendering.Services;

[TestFixture]
internal sealed class GraphicsProviderTests
{
    private GraphicsProvider graphicsProvider;

    [Test]
    public void ConstructorShouldNotThrowExceptionWhenInvoked()
    {
        // Act and assert
        Assert.DoesNotThrow(() => new GraphicsProvider());
    }

    [SetUp]
    public void Setup()
    {
        this.graphicsProvider = new GraphicsProvider();
    }
}
