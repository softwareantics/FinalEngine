// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

using FinalEngine.Platform;
using FinalEngine.Runtime.Desktop.Extensions;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddWindowsRuntime();

var window = services.BuildServiceProvider().GetRequiredService<IWindow>();

// Swap these two lines around to see the visual artifact dissapear.
window.IsVisible = true;
window.State = WindowState.Fullscreen;

while (!window.IsClosing)
{
    Application.DoEvents();
}

window.Dispose();
