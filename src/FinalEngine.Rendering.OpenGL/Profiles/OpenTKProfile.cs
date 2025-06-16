// <copyright file="OpenTKProfile.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Profiles;

using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using FinalEngine.Rendering.Buffers;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.States;
using FinalEngine.Rendering.Textures;
using TKBlendEquationMode = OpenTK.Graphics.OpenGL4.BlendEquationMode;
using TKBlendingFactor = OpenTK.Graphics.OpenGL4.BlendingFactor;
using TKBufferUsageHint = OpenTK.Graphics.OpenGL4.BufferUsageHint;
using TKCullFaceMode = OpenTK.Graphics.OpenGL4.CullFaceMode;
using TKDepthFunction = OpenTK.Graphics.OpenGL4.DepthFunction;
using TKFrontFaceDirection = OpenTK.Graphics.OpenGL4.FrontFaceDirection;
using TKPixelForamt = OpenTK.Graphics.OpenGL4.PixelFormat;
using TKPixelType = OpenTK.Graphics.OpenGL4.PixelType;
using TKPolygonMode = OpenTK.Graphics.OpenGL4.PolygonMode;
using TKPrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;
using TKShaderType = OpenTK.Graphics.OpenGL4.ShaderType;
using TKSizedInternalFormat = OpenTK.Graphics.OpenGL4.SizedInternalFormat;
using TKStencilFunction = OpenTK.Graphics.OpenGL4.StencilFunction;
using TKStencilOp = OpenTK.Graphics.OpenGL4.StencilOp;
using TKTextureMagFilter = OpenTK.Graphics.OpenGL4.TextureMagFilter;
using TKTextureMinFilter = OpenTK.Graphics.OpenGL4.TextureMinFilter;
using TKTextureWrapMode = OpenTK.Graphics.OpenGL4.TextureWrapMode;
using TKVertexAttribType = OpenTK.Graphics.OpenGL4.VertexAttribType;

internal sealed class OpenTKProfile : Profile
{
    public OpenTKProfile()
    {
        this.CreateMap<PrimitiveTopology, TKPrimitiveType>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(PrimitiveTopology.Triangle, TKPrimitiveType.Triangles);
                x.MapValue(PrimitiveTopology.TriangleStrip, TKPrimitiveType.TriangleStrip);
                x.MapValue(PrimitiveTopology.Line, TKPrimitiveType.Lines);
                x.MapValue(PrimitiveTopology.LineStrip, TKPrimitiveType.LineStrip);
            }).ReverseMap();

        this.CreateMap<FaceCullMode, TKCullFaceMode>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(FaceCullMode.Back, TKCullFaceMode.Back);
                x.MapValue(FaceCullMode.Front, TKCullFaceMode.Front);
            }).ReverseMap();

        this.CreateMap<WindingDirection, TKFrontFaceDirection>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(WindingDirection.Clockwise, TKFrontFaceDirection.Cw);
                x.MapValue(WindingDirection.CounterClockwise, TKFrontFaceDirection.Ccw);
            }).ReverseMap();

        this.CreateMap<RasterMode, TKPolygonMode>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(RasterMode.Wireframe, TKPolygonMode.Line);
                x.MapValue(RasterMode.Solid, TKPolygonMode.Fill);
            }).ReverseMap();

        this.CreateMap<InputElementType, TKVertexAttribType>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(InputElementType.Byte, TKVertexAttribType.Byte);
                x.MapValue(InputElementType.Int, TKVertexAttribType.Int);
                x.MapValue(InputElementType.Float, TKVertexAttribType.Float);
                x.MapValue(InputElementType.Double, TKVertexAttribType.Double);
                x.MapValue(InputElementType.Short, TKVertexAttribType.Short);
            }).ReverseMap();

        this.CreateMap<PipelineTarget, TKShaderType>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(PipelineTarget.Vertex, TKShaderType.VertexShader);
                x.MapValue(PipelineTarget.Fragment, TKShaderType.FragmentShader);
            }).ReverseMap();

        this.CreateMap<BlendEquationMode, TKBlendEquationMode>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(BlendEquationMode.Min, TKBlendEquationMode.Min);
                x.MapValue(BlendEquationMode.Max, TKBlendEquationMode.Max);
                x.MapValue(BlendEquationMode.Add, TKBlendEquationMode.FuncAdd);
                x.MapValue(BlendEquationMode.Subtract, TKBlendEquationMode.FuncSubtract);
                x.MapValue(BlendEquationMode.ReverseSubtract, TKBlendEquationMode.FuncReverseSubtract);
            }).ReverseMap();

        this.CreateMap<BlendMode, TKBlendingFactor>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(BlendMode.One, TKBlendingFactor.One);
                x.MapValue(BlendMode.Zero, TKBlendingFactor.Zero);
                x.MapValue(BlendMode.DestinationAlpha, TKBlendingFactor.DstAlpha);
                x.MapValue(BlendMode.SourceAlpha, TKBlendingFactor.SrcAlpha);
                x.MapValue(BlendMode.DestinationColor, TKBlendingFactor.DstColor);
                x.MapValue(BlendMode.SourceColor, TKBlendingFactor.SrcColor);
                x.MapValue(BlendMode.OneMinusDestinationAlpha, TKBlendingFactor.OneMinusDstAlpha);
                x.MapValue(BlendMode.OneMinusSourceAlpha, TKBlendingFactor.OneMinusSrcAlpha);
                x.MapValue(BlendMode.OneMinusDestinationColor, TKBlendingFactor.OneMinusDstColor);
                x.MapValue(BlendMode.OneMinusSourceColor, TKBlendingFactor.OneMinusSrcColor);
            }).ReverseMap();

        this.CreateMap<ComparisonMode, TKDepthFunction>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(ComparisonMode.Greater, TKDepthFunction.Greater);
                x.MapValue(ComparisonMode.GreaterEqual, TKDepthFunction.Gequal);
                x.MapValue(ComparisonMode.Less, TKDepthFunction.Less);
                x.MapValue(ComparisonMode.LessEqual, TKDepthFunction.Lequal);
                x.MapValue(ComparisonMode.Equal, TKDepthFunction.Equal);
                x.MapValue(ComparisonMode.NotEqual, TKDepthFunction.Notequal);
                x.MapValue(ComparisonMode.Never, TKDepthFunction.Never);
                x.MapValue(ComparisonMode.Always, TKDepthFunction.Always);
            }).ReverseMap();

        this.CreateMap<ComparisonMode, TKStencilFunction>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(ComparisonMode.Greater, TKStencilFunction.Greater);
                x.MapValue(ComparisonMode.GreaterEqual, TKStencilFunction.Gequal);
                x.MapValue(ComparisonMode.Less, TKStencilFunction.Less);
                x.MapValue(ComparisonMode.LessEqual, TKStencilFunction.Lequal);
                x.MapValue(ComparisonMode.Equal, TKStencilFunction.Equal);
                x.MapValue(ComparisonMode.NotEqual, TKStencilFunction.Notequal);
                x.MapValue(ComparisonMode.Never, TKStencilFunction.Never);
                x.MapValue(ComparisonMode.Always, TKStencilFunction.Always);
            }).ReverseMap();

        this.CreateMap<StencilOperation, TKStencilOp>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(StencilOperation.Keep, TKStencilOp.Keep);
                x.MapValue(StencilOperation.Zero, TKStencilOp.Zero);
                x.MapValue(StencilOperation.Replace, TKStencilOp.Replace);
                x.MapValue(StencilOperation.Increment, TKStencilOp.Incr);
                x.MapValue(StencilOperation.IncrementWrap, TKStencilOp.IncrWrap);
                x.MapValue(StencilOperation.Decrement, TKStencilOp.Decr);
                x.MapValue(StencilOperation.DecrementWrap, TKStencilOp.DecrWrap);
                x.MapValue(StencilOperation.Invert, TKStencilOp.Invert);
            }).ReverseMap();

        this.CreateMap<TextureFilterMode, TKTextureMinFilter>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(TextureFilterMode.Linear, TKTextureMinFilter.Linear);
                x.MapValue(TextureFilterMode.Nearest, TKTextureMinFilter.Nearest);
                x.MapValue(TextureFilterMode.LinearMipmapLinear, TKTextureMinFilter.LinearMipmapLinear);
            }).ReverseMap();

        this.CreateMap<TextureFilterMode, TKTextureMagFilter>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(TextureFilterMode.Linear, TKTextureMagFilter.Linear);
                x.MapValue(TextureFilterMode.Nearest, TKTextureMagFilter.Nearest);
            }).ReverseMap();

        this.CreateMap<TextureWrapMode, TKTextureWrapMode>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(TextureWrapMode.Repeat, TKTextureWrapMode.Repeat);
                x.MapValue(TextureWrapMode.Clamp, TKTextureWrapMode.ClampToEdge);
            }).ReverseMap();

        this.CreateMap<PixelType, TKPixelType>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(PixelType.Byte, TKPixelType.UnsignedByte);
                x.MapValue(PixelType.Float, TKPixelType.Float);
                x.MapValue(PixelType.Int, TKPixelType.UnsignedInt);
                x.MapValue(PixelType.Short, TKPixelType.UnsignedShort);
            }).ReverseMap();

        this.CreateMap<PixelFormat, TKPixelForamt>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(PixelFormat.R, TKPixelForamt.Red);
                x.MapValue(PixelFormat.Rg, TKPixelForamt.Rg);
                x.MapValue(PixelFormat.Rgb, TKPixelForamt.Rgb);
                x.MapValue(PixelFormat.Rgba, TKPixelForamt.Rgba);
            }).ReverseMap();

        this.CreateMap<SizedFormat, TKSizedInternalFormat>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(SizedFormat.R8, TKSizedInternalFormat.R8);
                x.MapValue(SizedFormat.Rg8, TKSizedInternalFormat.Rg8);
                x.MapValue(SizedFormat.Rgb8, TKSizedInternalFormat.Rgb8);
                x.MapValue(SizedFormat.Rgba8, TKSizedInternalFormat.Rgba8);
            }).ReverseMap();

        this.CreateMap<BufferUsageType, TKBufferUsageHint>()
            .ConvertUsingEnumMapping(x =>
            {
                x.MapValue(BufferUsageType.Static, TKBufferUsageHint.StaticDraw);
                x.MapValue(BufferUsageType.Dynamic, TKBufferUsageHint.DynamicDraw);
            }).ReverseMap();
    }
}
