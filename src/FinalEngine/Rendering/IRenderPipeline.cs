// <copyright file="IRenderPipeline.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System;

public interface IRenderPipeline : IDisposable
{
    void Initialize();
}
