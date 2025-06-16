// <copyright file="IPipeline.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering;

using System.Numerics;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;

public interface IPipeline
{
    int MaxTextureSlots { get; }

    void SetShaderProgram(IShaderProgram program);

    void SetTexture(ITexture2D texture, int slot = 0);

    void SetUniform(string name, int value);

    void SetUniform(string name, float value);

    void SetUniform(string name, double value);

    void SetUniform(string name, bool value);

    void SetUniform(string name, Vector2 value);

    void SetUniform(string name, Vector3 value);

    void SetUniform(string name, Vector4 value);

    void SetUniform(string name, Matrix4x4 value);
}
