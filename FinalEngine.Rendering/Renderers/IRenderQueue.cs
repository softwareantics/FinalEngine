// <copyright file="IRenderQueue.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

public interface IRenderQueue<T>
{
    bool CanRender { get; }

    void Clear();

    void Enqueue(T renderable);
}
