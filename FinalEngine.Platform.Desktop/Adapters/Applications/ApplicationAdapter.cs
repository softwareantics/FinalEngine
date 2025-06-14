// <copyright file="ApplicationAdapter.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Adapters.Applications;

using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

[ExcludeFromCodeCoverage]
internal sealed class ApplicationAdapter : IApplicationAdapter
{
    public bool FilterMessage(ref Message message)
    {
        return Application.FilterMessage(ref message);
    }
}
