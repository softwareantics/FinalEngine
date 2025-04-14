// <copyright file="EnvironmentContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Environment;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage(Justification = "Invocation")]
internal sealed class EnvironmentContext : IEnvironmentContext
{
    public string GetFolderPath(Environment.SpecialFolder folder)
    {
        return Environment.GetFolderPath(folder);
    }
}
