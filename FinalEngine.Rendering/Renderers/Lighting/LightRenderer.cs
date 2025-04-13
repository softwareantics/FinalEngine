// <copyright file="LightRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers.Lighting;

using System;
using System.Collections.Generic;
using System.Numerics;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;

internal sealed class LightRenderer : ILightRenderer
{
    private readonly Light ambientLight;

    private readonly Dictionary<LightType, IList<Light>> lightTypeToLightMap;

    private readonly IRenderDevice renderDevice;

    private IShaderProgram? ambientProgram;

    private IShaderProgram? directionalProgram;

    private IShaderProgram? pointProgram;

    private IShaderProgram? spotProgram;

    public LightRenderer(IRenderDevice renderDevice)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

        this.lightTypeToLightMap = [];

        this.ambientLight = new Light()
        {
            Type = LightType.Ambient,
            Intensity = 0.1f,
        };
    }

    public bool CanRender
    {
        get { return this.lightTypeToLightMap.Count != 0; }
    }

    private IShaderProgram AmbientProgram
    {
        get { return this.ambientProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Lighting\\lighting-ambient.fesp"); }
    }

    private IShaderProgram DirectionalProgram
    {
        get { return this.directionalProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Lighting\\lighting-directional.fesp"); }
    }

    private IShaderProgram PointProgram
    {
        get { return this.pointProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Lighting\\lighting-point.fesp"); }
    }

    private IShaderProgram SpotProgram
    {
        get { return this.spotProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\Lighting\\lighting-spot.fesp"); }
    }

    public void Clear()
    {
        this.lightTypeToLightMap.Clear();
    }

    public void Enqueue(Light renderable)
    {
        ArgumentNullException.ThrowIfNull(renderable, nameof(renderable));

        if (!this.lightTypeToLightMap.TryGetValue(renderable.Type, out var batch))
        {
            batch = [];
            this.lightTypeToLightMap.Add(renderable.Type, batch);
        }

        batch.Add(renderable);
    }

    public void Render(Action renderScene)
    {
        ArgumentNullException.ThrowIfNull(renderScene, nameof(renderScene));

        this.UpdateUniforms(this.ambientLight);
        renderScene();

        foreach (var kvp in this.lightTypeToLightMap)
        {
            var type = kvp.Key;
            var batch = kvp.Value;

            foreach (var light in batch)
            {
                if (light.Type == LightType.Ambient)
                {
                    continue;
                }

                this.Prepare();

                this.UpdateUniforms(light);
                renderScene();

                this.Conclude();
            }
        }
    }

    public void SetAmbientLight(Vector3 color, float intensity)
    {
        this.ambientLight.Color = color;
        this.ambientLight.Intensity = intensity;
    }

    private void Conclude()
    {
        this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription()
        {
            Enabled = false,
        });

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            ReadEnabled = true,
            ComparisonMode = ComparisonMode.Less,
            WriteEnabled = true,
        });
    }

    private void Prepare()
    {
        this.renderDevice.OutputMerger.SetBlendState(new BlendStateDescription()
        {
            Enabled = true,
            SourceMode = BlendMode.One,
            DestinationMode = BlendMode.One,
        });

        this.renderDevice.OutputMerger.SetDepthState(new DepthStateDescription()
        {
            ReadEnabled = true,
            WriteEnabled = false,
            ComparisonMode = ComparisonMode.Equal,
        });
    }

    private void RenderAmbientLight()
    {
        this.renderDevice.Pipeline.SetShaderProgram(this.AmbientProgram);
    }

    private void RenderAttenuation(Light light)
    {
        this.renderDevice.Pipeline.SetUniform("u_light.attenuation.constant", light.Attenuation.Constant);
        this.renderDevice.Pipeline.SetUniform("u_light.attenuation.linear", light.Attenuation.Linear);
        this.renderDevice.Pipeline.SetUniform("u_light.attenuation.quadratic", light.Attenuation.Quadratic);
    }

    private void RenderDirectionalLight(Light light)
    {
        this.renderDevice.Pipeline.SetShaderProgram(this.DirectionalProgram);
        this.renderDevice.Pipeline.SetUniform("u_light.direction", light.Direction);
    }

    private void RenderPointLight(Light light)
    {
        this.renderDevice.Pipeline.SetShaderProgram(this.PointProgram);

        this.RenderAttenuation(light);
        this.renderDevice.Pipeline.SetUniform("u_light.position", light.Position);
    }

    private void RenderSpotLight(Light light)
    {
        this.renderDevice.Pipeline.SetShaderProgram(this.SpotProgram);

        this.RenderAttenuation(light);

        this.renderDevice.Pipeline.SetUniform("u_light.position", light.Position);
        this.renderDevice.Pipeline.SetUniform("u_light.direction", light.Direction);
        this.renderDevice.Pipeline.SetUniform("u_light.radius", light.Radius);
        this.renderDevice.Pipeline.SetUniform("u_light.outerRadius", light.OuterRadius);
    }

    private void UpdateUniforms(Light light)
    {
        ArgumentNullException.ThrowIfNull(light, nameof(light));

        switch (light.Type)
        {
            case LightType.Directional:
                this.RenderDirectionalLight(light);
                break;

            case LightType.Point:
                this.RenderPointLight(light);
                break;

            case LightType.Spot:
                this.RenderSpotLight(light);
                break;

            case LightType.Ambient:
                this.RenderAmbientLight();
                break;

            default:
                throw new NotSupportedException($"The specified {nameof(light)} is not supported by the {nameof(LightRenderer)}.");
        }

        this.renderDevice.Pipeline.SetUniform("u_light.base.color", light.Color);
        this.renderDevice.Pipeline.SetUniform("u_light.base.intensity", light.Intensity);
    }
}
