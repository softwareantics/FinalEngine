// <copyright file="ViewportBlackboardResource.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Blackboard;

using System.Drawing;
using FinalEngine.ECS.Blackboard;

internal sealed class ViewportBlackboardResource : IBlackboardResource<Rectangle>
{
    public Rectangle Resource { get; set; } = new Rectangle(0, 0, 1, 1);
}
