// <copyright file="IShader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Pipeline;

using FinalEngine.Resources;

public enum PipelineTarget
{
    Vertex,

    Fragment,
}

public interface IShader : IResource
{
    PipelineTarget EntryPoint { get; }
}
