// <copyright file="FormAdapter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Invocation.Forms;

using System.Diagnostics.CodeAnalysis;

/// <summary>
///   Provides a standard implementation of an <see cref="IFormAdapter"/>.
/// </summary>
/// <seealso cref="Form"/>
/// <seealso cref="IFormAdapter"/>
[ExcludeFromCodeCoverage]
internal sealed class FormAdapter : Form, IFormAdapter
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="FormAdapter"/> class.
    /// </summary>
    public FormAdapter()
    {
    }
}
