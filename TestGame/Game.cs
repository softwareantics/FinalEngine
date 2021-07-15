// <copyright file="Game.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System.Drawing;
    using FinalEngine.ECS;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Launching;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Textures;
    using TestGame.Components;
    using TestGame.Systems;

    public class Game : GameContainer
    {
        private readonly ISpriteDrawer drawer;

        private readonly IEntityWorld world;

        public Game()
        {
            var batcher = new SpriteBatcher(this.RenderDevice.InputAssembler);
            var binder = new TextureBinder(this.RenderDevice.Pipeline);

            this.drawer = new SpriteDrawer(
                this.RenderDevice,
                batcher,
                binder,
                this.Window!.ClientSize.Width,
                this.Window!.ClientSize.Height);

            this.world = new EntityWorld();
            this.world.AddSystem(new SpriteRenderSystem(this.drawer));

            var entity = new Entity();

            entity.AddComponent<TransformComponent>();
            entity.AddComponent(new SpriteComponent()
            {
                Texture = this.ResourceManager!.GetResource<ITexture2D>("jedi.jpg"),
            });

            this.world.AddEntity(entity);
        }

        protected override void Render()
        {
            this.RenderDevice.Clear(Color.CornflowerBlue);

            this.world.ProcessAll(GameLoopType.Render);

            base.Render();
        }

        protected override void Update()
        {
            if (this.Keyboard.IsKeyReleased(Key.Escape))
            {
                this.Exit();
            }

            this.world.ProcessAll(GameLoopType.Update);

            base.Update();
        }
    }
}