// <copyright file="ITexture.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures
{
    using FinalEngine.Resources;

    /// <summary>
    ///   Defines an interface that represents a texture.
    /// </summary>
    /// <seealso cref="System.IDisposable"/>
    public interface ITexture : IResource
    {
        /// <summary>
        ///   Gets the format of this <see cref="ITexture"/>.
        /// </summary>
        /// <value>
        ///   The format of this <see cref="ITexture"/>.
        /// </value>
        PixelFormat Format { get; }

        /// <summary>
        ///   Gets the internal format of this <see cref="ITexture"/>.
        /// </summary>
        /// <value>
        ///   The internal format of this <see cref="ITexture"/>.
        /// </value>
        SizedFormat InternalFormat { get; }
    }
}