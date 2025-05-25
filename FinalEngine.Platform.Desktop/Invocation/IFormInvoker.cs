// <copyright file="IFormInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Invocation;

internal interface IFormInvoker : IDisposable
{
    event FormClosingEventHandler? FormClosing;

    Size ClientSize { get; set; }

    FormBorderStyle FormBorderStyle { get; set; }

    bool MaximizeBox { get; set; }

    FormStartPosition StartPosition { get; set; }

    string Text { get; set; }

    bool Visible { get; set; }

    FormWindowState WindowState { get; set; }

    void Close();
}
