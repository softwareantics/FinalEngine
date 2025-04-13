// <copyright file="Material.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Geometry;

using System;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

public sealed class Material : IMaterial
{
    private static readonly ITexture2D DefaultDiffuseTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_diffuse.png");

    private static readonly ITexture2D DefaultEmissionTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_emission.png");

    private static readonly ITexture2D DefaultNormalTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_normal.png");

    private static readonly ITexture2D DefaultSpecularTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_specular.png");

    private ITexture2D? diffuseTexture;

    private ITexture2D? emissionTexture;

    private ITexture2D? normalTexture;

    private ITexture2D? specularTexture;

    public Material()
    {
        this.Shininess = 32.0f;
    }

    public ITexture2D DiffuseTexture
    {
        get { return this.diffuseTexture ??= DefaultDiffuseTexture; }
        set { this.diffuseTexture = value; }
    }

    public ITexture2D EmissionTexture
    {
        get { return this.emissionTexture ??= DefaultEmissionTexture; }
        set { this.emissionTexture = value; }
    }

    public ITexture2D NormalTexture
    {
        get { return this.normalTexture ??= DefaultNormalTexture; }
        set { this.normalTexture = value; }
    }

    public float Shininess { get; set; }

    public ITexture2D SpecularTexture
    {
        get { return this.specularTexture ??= DefaultSpecularTexture; }
        set { this.specularTexture = value; }
    }

    public void Bind(IPipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline, nameof(pipeline));

        pipeline.SetUniform("u_material.diffuseTexture", 0);
        pipeline.SetUniform("u_material.specularTexture", 1);
        pipeline.SetUniform("u_material.normalTexture", 2);
        pipeline.SetUniform("u_material.emissionTexture", 3);
        pipeline.SetUniform("u_material.shininess", this.Shininess);

        pipeline.SetTexture(this.DiffuseTexture, 0);
        pipeline.SetTexture(this.SpecularTexture, 1);
        pipeline.SetTexture(this.NormalTexture, 2);
        pipeline.SetTexture(this.EmissionTexture, 3);
    }

    public bool Equals(Material? other)
    {
        if (other == null)
        {
            return false;
        }

        return ReferenceEquals(this.DiffuseTexture, other.DiffuseTexture) &&
               ReferenceEquals(this.SpecularTexture, other.SpecularTexture) &&
               ReferenceEquals(this.NormalTexture, other.NormalTexture) &&
               ReferenceEquals(this.EmissionTexture, other.EmissionTexture) &&
               this.Shininess == other.Shininess;
    }

    public override bool Equals(object? obj)
    {
        return obj is Material material && this.Equals(material);
    }

    public override int GetHashCode()
    {
        const int accumulator = 17;

        return (this.DiffuseTexture.GetHashCode() * accumulator) +
               (this.SpecularTexture.GetHashCode() * accumulator) +
               (this.NormalTexture.GetHashCode() * accumulator) +
               (this.EmissionTexture.GetHashCode() * accumulator) +
               (this.Shininess.GetHashCode() * accumulator);
    }
}
