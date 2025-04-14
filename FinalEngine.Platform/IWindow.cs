// <copyright file="IWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System;
using System.Drawing;

public interface IWindow : IDisposable
{
    Rectangle ClientBounds { get; }

    Size ClientSize { get; }

    bool IsExiting { get; }

    bool IsFocused { get; }

    Size Size { get; set; }

    string Title { get; set; }

    bool Visible { get; set; }

    void Close();
}
