// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Extensions;

using System;
using FinalEngine.Rendering.Batching;
using FinalEngine.Rendering.Effects;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.Loaders.Models;
using FinalEngine.Rendering.Loaders.Shaders;
using FinalEngine.Rendering.Loaders.Textures;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Renderers;
using FinalEngine.Rendering.Renderers.Effects;
using FinalEngine.Rendering.Renderers.Geometry;
using FinalEngine.Rendering.Renderers.Lighting;
using FinalEngine.Rendering.Renderers.Scenes;
using FinalEngine.Rendering.Renderers.Skyboxes;
using FinalEngine.Rendering.Systems;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources.Extensions;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRendering(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<ISpriteBatcher, SpriteBatcher>();
        services.AddSingleton<ITextureBinder, TextureBinder>();
        services.AddSingleton<ISpriteDrawer, SpriteDrawer>();

        services.AddSingleton<IGeometryRenderer, GeometryRenderer>();
        services.AddSingleton<ILightRenderer, LightRenderer>();
        services.AddSingleton<ISkyboxRenderer, SkyboxRenderer>();
        services.AddSingleton<ISceneRenderer, SceneRenderer>();
        services.AddSingleton<IPostRenderer, PostRenderer>();

        services.AddSingleton<IRenderQueue<RenderModel>>(x =>
        {
            return x.GetRequiredService<IGeometryRenderer>();
        });

        services.AddSingleton<IRenderQueue<Light>>(x =>
        {
            return x.GetRequiredService<ILightRenderer>();
        });

        services.AddSingleton<IRenderQueue<IRenderEffect>>(x =>
        {
            return x.GetRequiredService<IPostRenderer>();
        });

        services.AddSingleton<IRenderCoordinator, RenderCoordinator>();
        services.AddSingleton<IRenderingEngine, RenderingEngine>();

        services.AddResourceLoader<IShader, ShaderResourceLoader>();
        services.AddResourceLoader<IShaderProgram, ShaderProgramResourceLoader>();
        services.AddResourceLoader<ITexture2D, Texture2DResourceLoader>();
        services.AddResourceLoader<ITextureCube, TextureCubeResourceLoader>();
        services.AddResourceLoader<Model, ModelResourceLoader>();

        services.AddSingleton<SpriteRenderEntitySystem>();
        services.AddSingleton<MeshRenderEntitySystem>();
        services.AddSingleton<LightRenderEntitySystem>();
        services.AddSingleton<PerspectiveRenderEntitySystem>();

        return services;
    }
}
