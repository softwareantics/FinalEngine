// <copyright file="WinFormsProfileTests.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Platform.Desktop.Mappings.Profiles;

using System.Windows.Forms;
using AutoMapper;
using FinalEngine.Platform;
using FinalEngine.Platform.Mappings.Profiles;
using NUnit.Framework;

[TestFixture]
internal sealed class WinFormsProfileTests
{
    private IMapper mapper;

    [TestCase(FormBorderStyle.FixedSingle, WindowStyle.Fixed)]
    [TestCase(FormBorderStyle.Sizable, WindowStyle.Resizable)]
    [TestCase(FormBorderStyle.None, WindowStyle.Borderless)]
    public void FormBorderStyleToWindowStyleShouldMapCorrectly(FormBorderStyle actual, WindowStyle expected)
    {
        var result = this.mapper.Map<WindowStyle>(actual);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(FormWindowState.Normal, WindowState.Normal)]
    [TestCase(FormWindowState.Minimized, WindowState.Minimized)]
    [TestCase(FormWindowState.Maximized, WindowState.Maximized)]
    public void FormWindowStateToWindowStateShouldMapCorrectly(FormWindowState actual, WindowState expected)
    {
        var result = this.mapper.Map<WindowState>(actual);
        Assert.That(result, Is.EqualTo(expected));
    }

    [SetUp]
    public void SetUp()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WinFormsProfile>();
        });

        this.mapper = config.CreateMapper();
    }

    [TestCase(WindowState.Normal, FormWindowState.Normal)]
    [TestCase(WindowState.Minimized, FormWindowState.Minimized)]
    [TestCase(WindowState.Maximized, FormWindowState.Maximized)]
    [TestCase(WindowState.Fullscreen, FormWindowState.Maximized)]
    public void WindowStateToFormWindowStateShouldMapCorrectly(WindowState actual, FormWindowState expected)
    {
        var result = this.mapper.Map<FormWindowState>(actual);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(WindowStyle.Fixed, FormBorderStyle.FixedSingle)]
    [TestCase(WindowStyle.Resizable, FormBorderStyle.Sizable)]
    [TestCase(WindowStyle.Borderless, FormBorderStyle.None)]
    public void WindowStyleToFormBorderStyleShouldMapCorrectly(WindowStyle actual, FormBorderStyle expected)
    {
        var result = this.mapper.Map<FormBorderStyle>(actual);
        Assert.That(result, Is.EqualTo(expected));
    }
}
