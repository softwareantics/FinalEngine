// <copyright file="Texture2DDescription.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

using System;

public struct Texture2DDescription : IEquatable<Texture2DDescription>
{
    private TextureFilterMode? magFilter;

    private TextureFilterMode? minFilter;

    private PixelType? pixelType;

    private TextureWrapMode? wrapS;

    private TextureWrapMode? wrapT;

    public bool GenerateMipmaps { get; set; }

    public int Height { get; set; }

    public TextureFilterMode MagFilter
    {
        readonly get { return this.magFilter ?? TextureFilterMode.Nearest; }
        set { this.magFilter = value; }
    }

    public TextureFilterMode MinFilter
    {
        readonly get { return this.minFilter ?? TextureFilterMode.Nearest; }
        set { this.minFilter = value; }
    }

    public PixelType PixelType
    {
        readonly get { return this.pixelType ?? PixelType.Byte; }
        set { this.pixelType = value; }
    }

    public int Width { get; set; }

    public TextureWrapMode WrapS
    {
        readonly get { return this.wrapS ?? TextureWrapMode.Repeat; }
        set { this.wrapS = value; }
    }

    public TextureWrapMode WrapT
    {
        readonly get { return this.wrapT ?? TextureWrapMode.Repeat; }
        set { this.wrapT = value; }
    }

    public static bool operator !=(Texture2DDescription left, Texture2DDescription right)
    {
        return !(left == right);
    }

    public static bool operator ==(Texture2DDescription left, Texture2DDescription right)
    {
        return left.Equals(right);
    }

    public readonly bool Equals(Texture2DDescription other)
    {
        return this.MinFilter == other.MinFilter &&
               this.MagFilter == other.magFilter &&
               this.WrapS == other.WrapS &&
               this.WrapT == other.WrapT &&
               this.PixelType == other.PixelType &&
               this.Width == other.Width &&
               this.Height == other.Height &&
               this.GenerateMipmaps == other.GenerateMipmaps;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is Texture2DDescription description && this.Equals(description);
    }

    public override readonly int GetHashCode()
    {
        const int accumulator = 17;

        return (this.MinFilter.GetHashCode() * accumulator) +
               (this.MagFilter.GetHashCode() * accumulator) +
               (this.WrapS.GetHashCode() * accumulator) +
               (this.WrapT.GetHashCode() * accumulator) +
               (this.PixelType.GetHashCode() * accumulator) +
               (this.Width.GetHashCode() * accumulator) +
               (this.Height.GetHashCode() * accumulator) +
               (this.GenerateMipmaps.GetHashCode() * accumulator);
    }
}
