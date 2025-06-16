// <copyright file="IInputLayout.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Buffers;

using System.Collections.Generic;

public interface IInputLayout
{
    IEnumerable<InputElement> Elements { get; }
}
