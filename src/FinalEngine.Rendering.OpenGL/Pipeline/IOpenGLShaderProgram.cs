// <copyright file="IOpenGLShaderProgram.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Pipeline;
internal interface IOpenGLShaderProgram : IShaderProgram
{
    void Bind();

    bool TryGetUniformLocation(string name, out int location);
}
