// <copyright file="Texture2DResourceLoaderTests.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Resources.Extensions.ImageSharp;

using System.IO.Abstractions;
using FinalEngine.Rendering;
using FinalEngine.Resources.Extensions;
using FinalEngine.Resources.Extensions.Adapters;
using NSubstitute;

[TestFixture]
internal sealed class Texture2DResourceLoaderTests
{
    private IFileSystem fileSystem;

    private IImageAdapter imageSharp;

    private IRenderResourceFactory resourceFactory;

    private Texture2DResourceLoader resourceLoader;

    [Test]
    public void ConstructorShouldNotThrowExceptionWhenInvoked()
    {
        // Act and assert
        Assert.DoesNotThrow(() => new Texture2DResourceLoader(this.fileSystem, this.imageSharp, this.resourceFactory));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFileSystemIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() => new Texture2DResourceLoader(null, this.imageSharp, this.resourceFactory));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenImageSharpIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() => new Texture2DResourceLoader(this.fileSystem, null, this.resourceFactory));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenResourceFactoryIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() => new Texture2DResourceLoader(this.fileSystem, this.imageSharp, null));
    }

    [SetUp]
    public void Setup()
    {
        this.fileSystem = Substitute.For<IFileSystem>();
        this.imageSharp = Substitute.For<IImageAdapter>();
        this.resourceFactory = Substitute.For<IRenderResourceFactory>();

        this.resourceLoader = new Texture2DResourceLoader(this.fileSystem, this.imageSharp, this.resourceFactory);
    }
}
