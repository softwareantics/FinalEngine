// <copyright file="TextureQualitySettings.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

public enum TextureFilterType
{
    NearestNeighbour,

    Bilinear,

    Trilinear,
}

public readonly record struct TextureQualitySettings(
    TextureFilterType FilterType)
{
    public TextureQualitySettings()
        : this(TextureFilterType.Trilinear)
    {
    }

    public readonly TextureFilterMode MagFilter
    {
        get
        {
            return this.FilterType switch
            {
                TextureFilterType.NearestNeighbour => TextureFilterMode.Nearest,
                TextureFilterType.Bilinear => TextureFilterMode.Linear,
                TextureFilterType.Trilinear => TextureFilterMode.Linear,
                _ => TextureFilterMode.Linear,
            };
        }
    }

    public readonly TextureFilterMode MinFilter
    {
        get
        {
            return this.FilterType switch
            {
                TextureFilterType.NearestNeighbour => TextureFilterMode.Nearest,
                TextureFilterType.Bilinear => TextureFilterMode.Linear,
                TextureFilterType.Trilinear => TextureFilterMode.LinearMipmapLinear,
                _ => TextureFilterMode.LinearMipmapLinear,
            };
        }
    }
}
