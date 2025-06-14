// <copyright file="IFormAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Adapters.Forms;

internal interface IFormAdapter : IDisposable
{
    event FormClosedEventHandler? FormClosed;

    Size ClientSize { get; set; }

    FormBorderStyle FormBorderStyle { get; set; }

    nint Handle { get; }

    bool MaximizeBox { get; set; }

    FormStartPosition StartPosition { get; set; }

    string Text { get; set; }

    bool Visible { get; set; }

    FormWindowState WindowState { get; set; }

    void Close();
}
