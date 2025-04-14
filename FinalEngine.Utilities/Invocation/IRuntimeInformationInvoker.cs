// <copyright file="IRuntimeInformationInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Utilities.Invocation;

using System.Runtime.InteropServices;

internal interface IRuntimeInformationInvoker
{
    bool IsOSPlatform(OSPlatform platform);
}
