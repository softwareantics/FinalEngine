// <copyright file="GameContainer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Input.Mouse;
    using FinalEngine.IO;
    using FinalEngine.Platform;
    using FinalEngine.Rendering;
    using FinalEngine.Resources;

    [ExcludeFromCodeCoverage]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Class is constantly changing, not 100% certain if I'm going to keep it or not.")]
    public abstract class GameContainer : IDisposable
    {
        private const int InitialWindowHeight = 720;

        private const string InitialWindowTitle = "Final Engine";

        private const int InitialWindowWidth = 1280;

        private bool isRunning;

        protected GameContainer(IPlatformResolver resolver)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException(nameof(resolver), $"The specified {nameof(resolver)} parameter cannot be null.");
            }

            IGamePlatformFactory factory = resolver.Resolve();

            factory.InitializePlatform(
                InitialWindowWidth,
                InitialWindowHeight,
                InitialWindowTitle,
                out IWindow window,
                out IEventsProcessor eventsProcessor,
                out IFileSystem fileSystem,
                out IKeyboard keyboard,
                out IMouse mouse,
                out IRenderContext renderContext,
                out IRenderDevice renderDevice,
                out IResourceManager resourceManager);

            this.Window = window;
            this.EventsProcessor = eventsProcessor;

            this.FileSystem = fileSystem;

            this.Keyboard = keyboard;
            this.Mouse = mouse;

            this.RenderContext = renderContext;
            this.RenderDevice = renderDevice;

            this.ResourceManager = resourceManager;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="GameContainer"/> class.
        /// </summary>
        protected GameContainer()
            : this(PlatformResolver.Instance)
        {
        }

        /// <summary>
        ///   Finalizes an instance of the <see cref="GameContainer"/> class.
        /// </summary>
        ~GameContainer()
        {
            this.Dispose(false);
        }

        /// <summary>
        ///   Gets the file system.
        /// </summary>
        /// <value>
        ///   The file system.
        /// </value>
        protected IFileSystem FileSystem { get; }

        /// <summary>
        ///   Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        ///   Gets the keyboard.
        /// </summary>
        /// <value>
        ///   The keyboard.
        /// </value>
        protected IKeyboard Keyboard { get; }

        /// <summary>
        ///   Gets the mouse.
        /// </summary>
        /// <value>
        ///   The mouse.
        /// </value>
        protected IMouse Mouse { get; }

        /// <summary>
        ///   Gets the render device.
        /// </summary>
        /// <value>
        ///   The render device.
        /// </value>
        protected IRenderDevice RenderDevice { get; }

        protected IResourceManager? ResourceManager { get; private set; }

        /// <summary>
        ///   Gets the window.
        /// </summary>
        /// <value>
        ///   The window.
        /// </value>
        protected IWindow? Window { get; private set; }

        /// <summary>
        ///   Gets the events processor.
        /// </summary>
        /// <value>
        ///   The events processor.
        /// </value>
        private IEventsProcessor EventsProcessor { get; }

        /// <summary>
        ///   Gets or sets the render context.
        /// </summary>
        /// <value>
        ///   The render context.
        /// </value>
        private IRenderContext? RenderContext { get; set; }

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Exits the game, don't forget to call <see cref="Dispose"/>.
        /// </summary>
        public void Exit()
        {
            this.isRunning = false;
        }

        public void Launch(double frameCap)
        {
            this.Launch(new GameTime(frameCap));
        }

        public void Launch(IGameTime gameTime)
        {
            if (gameTime == null)
            {
                throw new ArgumentNullException(nameof(gameTime), $"The specified {nameof(gameTime)} parameter cannot be null.");
            }

            if (this.isRunning)
            {
                return;
            }

            this.isRunning = true;

            while (this.isRunning && !(this.Window?.IsExiting ?? false))
            {
                if (gameTime.CanProcessNextFrame())
                {
                    this.Update();

                    this.Keyboard.Update();
                    this.Mouse.Update();

                    this.Render();

                    this.RenderContext?.SwapBuffers();
                    this.EventsProcessor.ProcessEvents();
                }
            }
        }

        /// <summary>
        ///   Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.ResourceManager != null)
                {
                    this.ResourceManager.Dispose();
                    this.ResourceManager = null;
                }

                if (this.RenderContext != null)
                {
                    this.RenderContext.Dispose();
                    this.RenderContext = null;
                }

                if (this.Window != null)
                {
                    this.Window.Dispose();
                    this.Window = null;
                }
            }

            this.IsDisposed = true;
        }

        protected virtual void Render()
        {
        }

        protected virtual void Update()
        {
        }
    }
}