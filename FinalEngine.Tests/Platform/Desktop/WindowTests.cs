// <copyright file="WindowTests.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Platform.Desktop;

using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using AutoMapper;
using FinalEngine.Platform;
using FinalEngine.Platform.Adapters.Forms;
using FinalEngine.Platform.Adapters.Native;
using Microsoft.Extensions.Logging;
using NSubstitute;

[TestFixture]
internal sealed class WindowTests
{
    private IFormAdapter form;

    private ILogger<WinFormsWindow> logger;

    private IMapper mapper;

    private INativeAdapter nativeAdapter;

    private WinFormsWindow window;

    [Test]
    public void ClientSizeShouldReturnFormClientSize()
    {
        // Arrange
        var expected = new Size(800, 600);
        this.form.ClientSize.Returns(expected);

        // Act
        var result = this.window.ClientSize;

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ClientSizeShouldSetFormClientSize()
    {
        // Arrange
        var newSize = new Size(1024, 768);

        // Act
        this.window.ClientSize = newSize;

        // Assert
        this.form.Received(1).ClientSize = newSize;
    }

    [Test]
    public void ClientSizeShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.window.Dispose();

        // Act & Assert
        Assert.Throws<ObjectDisposedException>(() => { var _ = this.window.ClientSize; });
        Assert.Throws<ObjectDisposedException>(() => this.window.ClientSize = new Size(1, 1));
    }

    [Test]
    public void CloseShouldCallFormClose()
    {
        // Act
        this.window.Close();

        // Assert
        this.form.Received(1).Close();
    }

    [Test]
    public void CloseShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.window.Dispose();

        // Act & Assert
        Assert.Throws<ObjectDisposedException>(this.window.Close);
    }

    [Test]
    public void ConstructorShouldNotThrowArgumentNullExceptionWhenLoggerIsNull()
    {
        // Arrange & Act
        var ex = Assert.Throws<ArgumentNullException>(() => new WinFormsWindow(null, this.form, this.nativeAdapter, null));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("logger"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenFormIsNull()
    {
        // Arrange & Act
        var ex = Assert.Throws<ArgumentNullException>(() => new WinFormsWindow(this.logger, null, this.nativeAdapter, this.mapper));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("form"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenMapperIsNull()
    {
        // Arrange & Act
        var ex = Assert.Throws<ArgumentNullException>(() => new WinFormsWindow(this.logger, this.form, this.nativeAdapter, null));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("mapper"));
    }

    [Test]
    public void ConstructorShouldThrowArgumentNullExceptionWhenNativeAdapterIsNull()
    {
        // Arrange & Act
        var ex = Assert.Throws<ArgumentNullException>(() => new WinFormsWindow(this.logger, this.form, null, this.mapper));

        // Assert
        Assert.That(ex.ParamName, Is.EqualTo("native"));
    }

    [Test]
    public void DisposeShouldBeIdempotent()
    {
        // Act
        this.window.Dispose();
        this.window.Dispose();

        // Assert
        this.form.Received(1).Dispose();
    }

    [Test]
    public void DisposeShouldDisposeFormAndUnsubscribeEvent()
    {
        // Act
        this.window.Dispose();

        // Assert
        this.form.Received(1).Dispose();
    }

    [Test]
    public void FormClosedShouldThrowArgumentNullExceptionWhenArgsNull()
    {
        // Act
        var method = typeof(WinFormsWindow).GetMethod("Form_FormClosed", BindingFlags.NonPublic | BindingFlags.Instance);
        var ex = Assert.Throws<TargetInvocationException>(() => method!.Invoke(this.window, [null, null]));

        // Assert
        Assert.That(ex!.InnerException, Is.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void FormClosedShouldThrowInvokePostQuitMessageWhenInvoked()
    {
        // Arrange & Act
        this.form.FormClosed += Raise.Event<FormClosedEventHandler>(
            this.form,
            new FormClosedEventArgs(CloseReason.UserClosing));

        // Assert
        this.nativeAdapter.Received(1).PostQuitMessage(0);
    }

    [Test]
    public void IsUserReSizableShouldReturnFormMaximizeBox()
    {
        // Arrange
        this.form.MaximizeBox.Returns(true);

        // Act
        bool result = this.window.IsUserReSizable;

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsUserReSizableShouldSetFormMaximizeBox()
    {
        // Act
        this.window.IsUserReSizable = true;

        // Assert
        this.form.Received(1).MaximizeBox = true;
    }

    [Test]
    public void IsUserReSizableShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.window.Dispose();

        // Act & Assert
        Assert.Throws<ObjectDisposedException>(() => { bool _ = this.window.IsUserReSizable; });
        Assert.Throws<ObjectDisposedException>(() => this.window.IsUserReSizable = false);
    }

    [Test]
    public void IsVisibleShouldReturnFormVisible()
    {
        // Arrange
        this.form.Visible.Returns(false);

        // Act
        bool result = this.window.IsVisible;

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsVisibleShouldSetFormVisible()
    {
        // Act
        this.window.IsVisible = false;

        // Assert
        this.form.Received(1).Visible = false;
    }

    [Test]
    public void IsVisibleShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.window.Dispose();

        // Act & Assert
        Assert.Throws<ObjectDisposedException>(() => { bool _ = this.window.IsVisible; });
        Assert.Throws<ObjectDisposedException>(() => this.window.IsVisible = true);
    }

    [SetUp]
    public void SetUp()
    {
        this.logger = Substitute.For<ILogger<WinFormsWindow>>();
        this.form = Substitute.For<IFormAdapter>();
        this.nativeAdapter = Substitute.For<INativeAdapter>();
        this.mapper = Substitute.For<IMapper>();

        this.form.ClientSize.Returns(new Size(1280, 720));
        this.form.MaximizeBox.Returns(false);
        this.form.Visible.Returns(true);
        this.form.WindowState.Returns(FormWindowState.Normal);
        this.form.FormBorderStyle.Returns(FormBorderStyle.Sizable);
        this.form.Text.Returns("Final Engine");

        this.mapper.Map<WindowState>(Arg.Any<FormWindowState>()).Returns(WindowState.Normal);
        this.mapper.Map<FormWindowState>(Arg.Any<WindowState>()).Returns(FormWindowState.Normal);
        this.mapper.Map<WindowStyle>(Arg.Any<FormBorderStyle>()).Returns(WindowStyle.Resizable);
        this.mapper.Map<FormBorderStyle>(Arg.Any<WindowStyle>()).Returns(FormBorderStyle.Sizable);

        this.window = new WinFormsWindow(this.logger, this.form, this.nativeAdapter, this.mapper);
    }

    [Test]
    public void StateShouldReturnMappedWindowState()
    {
        // Arrange
        this.form.WindowState.Returns(FormWindowState.Maximized);
        this.mapper.Map<WindowState>(FormWindowState.Maximized).Returns(WindowState.Maximized);

        // Act
        var result = this.window.State;

        // Assert
        Assert.That(result, Is.EqualTo(WindowState.Maximized));
    }

    [Test]
    public void StateShouldSetFormWindowState()
    {
        // Arrange
        this.mapper.Map<FormWindowState>(WindowState.Minimized).Returns(FormWindowState.Minimized);

        // Act
        this.window.State = WindowState.Minimized;

        // Assert
        this.form.Received(1).WindowState = FormWindowState.Minimized;
    }

    [Test]
    public void StateShouldSetStyleToBorderlessWhenFullScreen()
    {
        // Arrange
        this.mapper.Map<FormWindowState>(WindowState.Fullscreen).Returns(FormWindowState.Maximized);
        this.mapper.Map<FormBorderStyle>(WindowStyle.Borderless).Returns(FormBorderStyle.None);

        // Act
        this.window.State = WindowState.Fullscreen;

        // Assert
        this.form.Received(1).FormBorderStyle = FormBorderStyle.None;
        this.form.Received(1).WindowState = FormWindowState.Maximized;
    }

    [Test]
    public void StateShouldSetStyleToFixedWhenNormal()
    {
        // Arrange
        this.mapper.Map<FormWindowState>(WindowState.Normal).Returns(FormWindowState.Normal);
        this.mapper.Map<FormBorderStyle>(WindowStyle.Fixed).Returns(FormBorderStyle.FixedDialog);

        // Act
        this.window.State = WindowState.Normal;

        // Assert
        this.form.Received(1).FormBorderStyle = FormBorderStyle.FixedDialog;
        this.form.Received(1).WindowState = FormWindowState.Normal;
    }

    [Test]
    public void StateShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.window.Dispose();

        // Act & Assert
        Assert.Throws<ObjectDisposedException>(() => { var _ = this.window.State; });
        Assert.Throws<ObjectDisposedException>(() => this.window.State = WindowState.Normal);
    }

    [Test]
    public void StyleShouldReturnMappedWindowStyle()
    {
        // Arrange
        this.form.FormBorderStyle.Returns(FormBorderStyle.FixedDialog);
        this.mapper.Map<WindowStyle>(FormBorderStyle.FixedDialog).Returns(WindowStyle.Fixed);

        // Act
        var result = this.window.Style;

        // Assert
        Assert.That(result, Is.EqualTo(WindowStyle.Fixed));
    }

    [Test]
    public void StyleShouldSetFormBorderStyle()
    {
        // Arrange
        this.mapper.Map<FormBorderStyle>(WindowStyle.Fixed).Returns(FormBorderStyle.FixedDialog);

        // Act
        this.window.Style = WindowStyle.Fixed;

        // Assert
        this.form.Received(1).FormBorderStyle = FormBorderStyle.FixedDialog;
    }

    [Test]
    public void StyleShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.window.Dispose();

        // Act & Assert
        Assert.Throws<ObjectDisposedException>(() => { var _ = this.window.Style; });
        Assert.Throws<ObjectDisposedException>(() => this.window.Style = WindowStyle.Resizable);
    }

    [TearDown]
    public void TearDown()
    {
        this.window.Dispose();
        this.form.Dispose();
    }

    [Test]
    public void TitleShouldReturnFormText()
    {
        // Arrange
        this.form.Text.Returns("Test Title");

        // Act
        string result = this.window.Title;

        // Assert
        Assert.That(result, Is.EqualTo("Test Title"));
    }

    [Test]
    public void TitleShouldSetFormText()
    {
        // Act
        this.window.Title = "New Title";

        // Assert
        this.form.Received(1).Text = "New Title";
    }

    [Test]
    public void TitleShouldThrowObjectDisposedExceptionWhenDisposed()
    {
        // Arrange
        this.window.Dispose();

        // Act & Assert
        Assert.Throws<ObjectDisposedException>(() => { string _ = this.window.Title; });
        Assert.Throws<ObjectDisposedException>(() => this.window.Title = "Should Fail");
    }
}
