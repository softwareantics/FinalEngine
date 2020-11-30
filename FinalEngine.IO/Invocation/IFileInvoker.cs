﻿// <copyright file="IFileInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation
{
    using System.IO;

    /// <summary>
    ///   Defines an interface that provides methods for invocation of the <see cref="File"/> class.
    /// </summary>
    public interface IFileInvoker
    {
        /// <summary>
        ///   Creates or overwrites a file in the specified path.
        /// </summary>
        /// <param name="path">
        ///   The path and name of the file to create.
        /// </param>
        /// <returns>
        ///   A <see cref="FileStream"/> that provides read/write access to the file specified in <paramref name="path"/>.
        /// </returns>
        FileStream Create(string path);

        /// <summary>
        ///   Deletes the specified file.
        /// </summary>
        /// <param name="path">
        ///   The name of the file to be deleted. Wildcard characters are not supported.
        /// </param>
        void Delete(string path);

        /// <summary>
        ///   Determines whether the specified file exists.
        /// </summary>
        /// <param name="path">
        ///   The file to check.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the caller has the required permissions and <paramref name="path"/> contains the name of an existing file; otherwise, <c>false</c>. This method also returns <c>false</c> if <paramref name="path"/> is <c>null</c>, an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns <c>false</c> regardless of the existence of <paramref name="path"/>.
        /// </returns>
        bool Exists(string path);

        /// <summary>
        ///   Opens a <see cref="FileStream"/> on the specified path, with the specified mode and access with no sharing.
        /// </summary>
        /// <param name="path">
        ///   The file to open.
        /// </param>
        /// <param name="mode">
        ///   A <see cref="FileMode"/> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.
        /// </param>
        /// <param name="access">
        ///   A <see cref="FileAccess"/> value that specifies the operations that can be performed on the file.
        /// </param>
        /// <returns>
        ///   An unshared <see cref="FileStream"/> that provides access to the specified file, with the specified mode and access.
        /// </returns>
        FileStream Open(string path, FileMode mode, FileAccess access);
    }
}