// <copyright file="ITextureCube.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

public interface ITextureCube : ITexture
{
    TextureCubeDescription Description { get; }
}
