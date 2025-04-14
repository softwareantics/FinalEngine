// <copyright file="RuntimeInformationInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities.Invocation;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

[ExcludeFromCodeCoverage(Justification = "Invocation")]
internal sealed class RuntimeInformationInvoker : IRuntimeInformationInvoker
{
    public bool IsOSPlatform(OSPlatform platform)
    {
        return RuntimeInformation.IsOSPlatform(platform);
    }
}
