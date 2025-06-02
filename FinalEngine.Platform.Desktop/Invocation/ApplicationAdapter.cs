// <copyright file="ApplicationAdapter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Invocation;

using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

/// <summary>
///   Provides a standard implementation of an <see cref="IApplicationAdapter"/>.
/// </summary>
/// <seealso cref="IApplicationAdapter"/>
[ExcludeFromCodeCoverage]
internal sealed class ApplicationAdapter : IApplicationAdapter
{
    /// <inheritdoc/>
    public bool FilterMessage(ref Message message)
    {
        return Application.FilterMessage(ref message);
    }
}
