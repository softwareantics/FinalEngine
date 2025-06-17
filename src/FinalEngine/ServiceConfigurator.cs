// <copyright file="ServiceConfigurator.cs" company="Software Antics">
//   Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine;

using System.IO.Abstractions;
using FinalEngine.Hosting;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Rendering.Batching;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;
using FinalEngine.Resources.Extensions;
using FinalEngine.Resources.Loaders;
using FinalEngine.Scenes;
using FinalEngine.Scenes.Entities;
using FinalEngine.Scenes.Systems;
using FinalEngine.Scenes.World;
using Microsoft.Extensions.DependencyInjection;

internal sealed class ServiceConfigurator : IServiceConfigurator
{
    public void ConfigureServices(IEngineBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IFileSystem, FileSystem>();

        //// TODO: Move all resource loaders and their registration to separate extension projects
        //// or at least any resource loaders that add dependencies to Core. Use IServiceConfigurator(s)
        builder.Services.AddResourceLoader<IShader, ShaderResourceLoader>();
        builder.Services.AddResourceLoader<IShaderProgram, ShaderProgramResourceLoader>();
        builder.Services.AddResourceLoader<ITexture2D, Texture2DResourceLoader>();
        builder.Services.AddSingleton<IResourceManager, ResourceManager>();

        builder.Services.AddSingleton<IKeyboard, Keyboard>();
        builder.Services.AddSingleton<IMouse, Mouse>();

        builder.Services.AddSingleton<ISpriteBatcher, SpriteBatcher>();
        builder.Services.AddSingleton<ITextureBinder, TextureBinder>();
        builder.Services.AddSingleton<ISpriteDrawer, SpriteDrawer>();

        builder.Services.AddSingleton<IEntityFactoryResolver>(provider =>
        {
            return new EntityFactoryResolver(type =>
            {
                if (provider.GetService(type) is not IEntityFactory factory)
                {
                    throw new InvalidOperationException($"No entity factory of type '{type.FullName}' is registered.");
                }

                return factory;
            });
        });

        builder.Services.AddSingleton<IEntitySystemResolver>(provider =>
        {
            return new EntitySystemResolver(type =>
            {
                if (provider.GetService(type) is not EntitySystemBase system)
                {
                    throw new InvalidOperationException($"No entity system of type '{type.FullName}' is registered.");
                }

                return system;
            });
        });

        builder.Services.AddTransient<IEntityWorld, EntityWorld>();

        builder.Services.AddTransient<SpriteRenderEntitySystem>();

        builder.Services.AddSingleton<ISceneFactory>(provider =>
        {
            IScene createScene()
            {
                return provider.GetRequiredService<IScene>();
            }

            return new SceneFactory(createScene);
        });

        builder.Services.AddTransient<IScene, Scene>();
        builder.Services.AddSingleton<ISceneManager>(SceneManager.Instance);

        builder.Services.AddSingleton<IEngineDriver, EngineDriver>();
    }
}
