﻿// <copyright file="IRenderContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    /// <summary>
    ///   Defines an interface that represents a rendering context.
    /// </summary>
    public interface IRenderContext
    {
        /// <summary>
        ///   Swaps the front and back buffers, displaying the rendered scene to the user.
        /// </summary>
        void SwapBuffers();
    }
}