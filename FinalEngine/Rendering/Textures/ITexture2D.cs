// <copyright file="ITexture2D.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using FinalEngine.Resources;

public interface ITexture2D : IResource
{
    int Height { get; }

    int Width { get; }
}
