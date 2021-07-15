// <copyright file="IGamePlatformFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Input.Mouse;
    using FinalEngine.IO;
    using FinalEngine.Platform;
    using FinalEngine.Rendering;
    using FinalEngine.Resources;

    /// <summary>
    ///   Defines an interface that provides a method for creating all the resources required for running a game on any platform.
    /// </summary>
    public interface IGamePlatformFactory
    {
        /// <summary>
        ///   Initializes the platform.
        /// </summary>
        /// <param name="width">
        ///   The width of the window or surface.
        /// </param>
        /// <param name="height">
        ///   The height of the window or surface.
        /// </param>
        /// <param name="title">
        ///   The title of the window or surface.
        /// </param>
        /// <param name="window">
        ///   The window.
        /// </param>
        /// <param name="eventsProcessor">
        ///   The events processor.
        /// </param>
        /// <param name="fileSystem">
        ///   The file system.
        /// </param>
        /// <param name="keyboard">
        ///   The keyboard.
        /// </param>
        /// <param name="mouse">
        ///   The mouse.
        /// </param>
        /// <param name="renderContext">
        ///   The render context.
        /// </param>
        /// <param name="renderDevice">
        ///   The render device.
        /// </param>
        /// <param name="textureLoader">
        ///   The texture loader.
        /// </param>
        void InitializePlatform(
            int width,
            int height,
            string title,
            out IWindow window,
            out IEventsProcessor eventsProcessor,
            out IFileSystem fileSystem,
            out IKeyboard keyboard,
            out IMouse mouse,
            out IRenderContext renderContext,
            out IRenderDevice renderDevice,
            out IResourceManager resourceManager);
    }
}