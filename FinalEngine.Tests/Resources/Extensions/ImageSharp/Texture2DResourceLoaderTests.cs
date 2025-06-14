// <copyright file="Texture2DResourceLoaderTests.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Resources.Extensions.ImageSharp;

using System;
using System.IO;
using System.IO.Abstractions;
using FinalEngine.Rendering;
using FinalEngine.Resources.Extensions;
using FinalEngine.Resources.Extensions.Adapters;
using NSubstitute;
using NUnit.Framework;

[TestFixture]
internal sealed class Texture2DResourceLoaderTests
{
    private const string ValidFilePath = "C:\\textures\\test.png";

    private IFileSystem fileSystem;

    private IImageAdapter imageAdapter;

    private Texture2DResourceLoader loader;

    private IRenderResourceFactory resourceFactory;

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFileSystemIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            new Texture2DResourceLoader(null, this.imageAdapter, this.resourceFactory));
        Assert.That(ex.ParamName, Is.EqualTo("fileSystem"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenImageAdapterIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            new Texture2DResourceLoader(this.fileSystem, null, this.resourceFactory));
        Assert.That(ex.ParamName, Is.EqualTo("imageSharp"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenResourceFactoryIsNull()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() =>
            new Texture2DResourceLoader(this.fileSystem, this.imageAdapter, null));
        Assert.That(ex.ParamName, Is.EqualTo("resourceFactory"));
    }

    [TestCase("")]
    [TestCase("   ")]
    public void LoadResourceShouldThrowArgumentExceptionWhenFilePathIsNullOrWhiteSpace(string filePath)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => this.loader.LoadResource(filePath));
    }

    [Test]
    public void LoadResourceShouldThrowArgumentNullExceptionWhenFilePathIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => this.loader.LoadResource(null));
    }

    [Test]
    public void LoadResourceShouldThrowFileNotFoundExceptionWhenFileDoesNotExist()
    {
        // Arrange
        this.fileSystem.File.Exists(ValidFilePath).Returns(false);

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => this.loader.LoadResource(ValidFilePath));
    }

    [SetUp]
    public void SetUp()
    {
        this.fileSystem = Substitute.For<IFileSystem>();
        this.imageAdapter = Substitute.For<IImageAdapter>();
        this.resourceFactory = Substitute.For<IRenderResourceFactory>();
        this.loader = new Texture2DResourceLoader(this.fileSystem, this.imageAdapter, this.resourceFactory);
    }

    [TearDown]
    public void TearDown()
    {
        this.fileSystem = null;
        this.imageAdapter = null;
        this.resourceFactory = null;
        this.loader = null;
    }
}
