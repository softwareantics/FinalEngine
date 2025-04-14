// <copyright file="IBlackboardResource.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Blackboard;

public interface IBlackboardResource
{
}

public interface IBlackboardResource<T> : IBlackboardResource
{
    T Resource { get; set; }
}
