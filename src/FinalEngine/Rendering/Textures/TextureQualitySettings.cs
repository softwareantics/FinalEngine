// <copyright file="TextureQualitySettings.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using System;

public enum TextureFilterType
{
    NearestNeighbour,

    Bilinear,

    Trilinear,
}

public struct TextureQualitySettings : IEquatable<TextureQualitySettings>
{
    private TextureFilterType? filterType;

    public TextureFilterType FilterType
    {
        readonly get { return this.filterType ?? TextureFilterType.Trilinear; }
        set { this.filterType = value; }
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

    public static bool operator !=(TextureQualitySettings left, TextureQualitySettings right)
    {
        return !(left == right);
    }

    public static bool operator ==(TextureQualitySettings left, TextureQualitySettings right)
    {
        return left.Equals(right);
    }

    public readonly bool Equals(TextureQualitySettings other)
    {
        return this.FilterType == other.FilterType;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is TextureQualitySettings settings && this.Equals(settings);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return this.FilterType.GetHashCode() * accumulator;
    }
}
