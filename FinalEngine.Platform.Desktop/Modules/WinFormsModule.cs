// <copyright file="WinFormsModule.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Modules;

using System.Composition;
using FinalEngine.Platform.Desktop.Extensions;
using FinalEngine.Utilities.Modules;

[Export(typeof(IEngineModule))]
internal sealed class WinFormsModule : IEngineModule
{
    public void Load(IEngineOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        options.Services.AddWindows();
    }
}
