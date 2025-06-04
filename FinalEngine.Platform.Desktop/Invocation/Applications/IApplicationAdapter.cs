// <copyright file="IApplicationAdapter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Invocation.Applications;

/// <summary>
/// Defines an interface that represents an <see cref="Application"/> adapter.
/// </summary>
internal interface IApplicationAdapter
{
    /// <inheritdoc cref="Application.FilterMessage(ref Message)"/>
    bool FilterMessage(ref Message message);
}
