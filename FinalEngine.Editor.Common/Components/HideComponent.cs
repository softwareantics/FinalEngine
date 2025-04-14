// <copyright file="HideComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Components;

using FinalEngine.ECS;

internal sealed class HideComponent : IEntityComponent
{
    public bool Hidden { get; set; } = true;
}
