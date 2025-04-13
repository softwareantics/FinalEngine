// <copyright file="IRenderEffect.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Effects;

internal interface IRenderEffect
{
    bool Enabled { get; set; }

    void Bind(IPipeline pipeline);
}
