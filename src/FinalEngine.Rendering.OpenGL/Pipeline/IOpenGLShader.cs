// <copyright file="IOpenGLShader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Pipeline;
internal interface IOpenGLShader : IShader
{
    void Attach(int program);
}
