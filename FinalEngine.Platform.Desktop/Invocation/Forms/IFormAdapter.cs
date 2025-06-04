// <copyright file="IFormAdapter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Invocation.Forms;

/// <summary>
/// Defines an interface that represents an adapter for a <see cref="Form"/>.
/// </summary>
/// <seealso cref="IDisposable"/>
internal interface IFormAdapter : IDisposable
{
    /// <inheritdoc cref="Form.FormClosed"/>
    event FormClosedEventHandler? FormClosed;

    /// <inheritdoc cref="Form.ClientSize"/>
    Size ClientSize { get; set; }

    /// <inheritdoc cref="Form.FormBorderStyle"/>
    FormBorderStyle FormBorderStyle { get; set; }

    /// <inheritdoc cref="Form.MaximizeBox"/>
    bool MaximizeBox { get; set; }

    /// <inheritdoc cref="Form.StartPosition"/>
    FormStartPosition StartPosition { get; set; }

    /// <inheritdoc cref="Form.Text"/>
    string Text { get; set; }

    /// <inheritdoc cref="Control.Visible"/>
    bool Visible { get; set; }

    /// <inheritdoc cref="Form.WindowState"/>
    FormWindowState WindowState { get; set; }

    /// <inheritdoc cref="Form.Close"/>
    void Close();
}
