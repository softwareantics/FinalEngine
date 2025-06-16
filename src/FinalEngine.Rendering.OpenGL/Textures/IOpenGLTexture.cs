// <copyright file="IOpenGLTexture.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

internal interface IOpenGLTexture : ITexture2D
{
    void Bind(int unit);
}
