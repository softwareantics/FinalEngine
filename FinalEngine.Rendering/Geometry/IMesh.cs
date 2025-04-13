// <copyright file="IMesh.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Geometry;

public interface IMesh
{
    void Bind(IInputAssembler inputAssembler);

    void Draw(IRenderDevice renderDevice);
}
