// <copyright file="NativeWindowAdapter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Adapters;

using System.Diagnostics.CodeAnalysis;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

[ExcludeFromCodeCoverage]
internal sealed class NativeWindowAdapter : NativeWindow, INativeWindowAdapter
{
    public NativeWindowAdapter()
        : base(new NativeWindowSettings()
        {
            API = ContextAPI.OpenGL,
            APIVersion = new Version(4, 6),
            Flags = ContextFlags.ForwardCompatible,
            Profile = ContextProfile.Core,
            AutoLoadBindings = false,

            ClientSize = new OpenTK.Mathematics.Vector2i(1280, 720),
            WindowBorder = WindowBorder.Resizable,
            WindowState = WindowState.Normal,

            Title = "Final Engine",
            StartVisible = true,
            Vsync = VSyncMode.Off,
        })
    {
    }
}
