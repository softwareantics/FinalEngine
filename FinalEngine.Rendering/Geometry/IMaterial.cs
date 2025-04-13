// <copyright file="IMaterial.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Geometry;

using FinalEngine.Rendering.Textures;

public interface IMaterial
{
    ITexture2D DiffuseTexture { get; set; }

    ITexture2D EmissionTexture { get; set; }

    ITexture2D NormalTexture { get; set; }

    float Shininess { get; set; }

    ITexture2D SpecularTexture { get; set; }

    void Bind(IPipeline pipeline);
}
