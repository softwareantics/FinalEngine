// <copyright file="FormInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.Invocation;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage(Justification = "Adapter class for Windows Forms.")]
internal sealed class FormInvoker : Form, IFormInvoker
{
    public FormInvoker()
    {
    }
}
