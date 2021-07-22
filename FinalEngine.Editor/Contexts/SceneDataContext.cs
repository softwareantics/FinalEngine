// <copyright file="SceneDataContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Contexts
{
    using System;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Invocation;
    using FinalEngine.Rendering.Textures;

    public class SceneDataContext : DataContextBase, ISceneDataContext
    {
        private readonly IRenderDevice renderDevice;

        private ISpriteDrawer drawer;

        private ITexture2D texture;

        public SceneDataContext(IRenderDevice renderDevice)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice), $"The specified {nameof(renderDevice)} parameter cannot be null.");
        }

        public void Initialize()
        {
            this.renderDevice.Initialize();

            var binder = new TextureBinder(this.renderDevice.Pipeline);
            var batcher = new SpriteBatcher(this.renderDevice.InputAssembler);
            this.drawer = new SpriteDrawer(this.renderDevice, batcher, binder, 800, 450);

            var loader = new Texture2DResourceLoader(new FileSystem(new FileInvoker(), new DirectoryInvoker()), this.renderDevice.Factory, new ImageInvoker());

            this.texture = loader.LoadResource("Resources\\Textures\\jedi.jpg");
        }

        public void Render()
        {
            this.renderDevice.Clear(Color.CornflowerBlue);

            this.drawer.Begin();

            this.drawer.Draw(
                this.texture,
                Color.White,
                Vector2.Zero,
                new Vector2(100, 100),
                0,
                new Vector2(256, 256));

            this.drawer.End();
        }
    }
}