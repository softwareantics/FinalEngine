// <copyright file="DesktopGamePlatformFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching.Factories
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Input.Mouse;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
    using FinalEngine.Platform;
    using FinalEngine.Platform.Desktop.OpenTK;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Invocation;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.Textures;
    using FinalEngine.Resources;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    ///   Provides a standard desktop implementation of an <see cref="IGamePlatformFactory"/> that runs on Windows, Macintosh and Linux operating systems.
    /// </summary>
    /// <seealso cref="FinalEngine.Launching.IGamePlatformFactory"/>
    [ExcludeFromCodeCoverage]
    public class DesktopGamePlatformFactory : IGamePlatformFactory
    {
        /// <summary>
        ///   Initializes the platform.
        /// </summary>
        /// <param name="width">
        ///   The width of the window (in pixels).
        /// </param>
        /// <param name="height">
        ///   The height of the window (in pixels).
        /// </param>
        /// <param name="title">
        ///   The title of the window.
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
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Handled by Game Container.")]
        public void InitializePlatform(
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
            out IResourceManager resourceManager)
        {
            var settings = new NativeWindowSettings()
            {
                API = ContextAPI.OpenGL,
                APIVersion = new Version(4, 5),

                Flags = ContextFlags.ForwardCompatible,
                Profile = ContextProfile.Core,

                AutoLoadBindings = false,

                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,

                Size = new OpenTK.Mathematics.Vector2i(1280, 720),

                StartVisible = true,
            };

            var nativeWindow = new NativeWindowInvoker(settings);

            window = new OpenTKWindow(nativeWindow);
            eventsProcessor = (IEventsProcessor)window;

            var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
            keyboard = new Keyboard(keyboardDevice);

            var mouseDevice = new OpenTKMouseDevice(nativeWindow);
            mouse = new Mouse(mouseDevice);

            var file = new FileInvoker();
            var directory = new DirectoryInvoker();
            fileSystem = new FileSystem(file, directory);

            var opengl = new OpenGLInvoker();
            var bindings = new GLFWBindingsContext();

            renderContext = new OpenGLRenderContext(opengl, bindings, nativeWindow.Context);
            renderDevice = new OpenGLRenderDevice(opengl);

            var image = new ImageInvoker();
            var textureLoader = new Texture2DResourceLoader(fileSystem, renderDevice.Factory, image);

            resourceManager = new ResourceManager();
            resourceManager.RegisterLoader(textureLoader);
        }
    }
}