// <copyright file="ModelResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Loaders.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Numerics;
using Assimp;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Primitives;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;
using AssimpMesh = Assimp.Mesh;
using AssimpNode = Assimp.Node;
using AssimpScene = Assimp.Scene;
using Material = Geometry.Material;

internal sealed class ModelResourceLoader : ResourceLoaderBase<Model>
{
    private readonly IFileSystem fileSystem;

    private readonly IRenderDevice renderDevice;

    public ModelResourceLoader(IFileSystem fileSystem, IRenderDevice renderDevice)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    public override Model LoadResource(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath, nameof(filePath));

        if (!this.fileSystem.File.Exists(filePath))
        {
            throw new FileNotFoundException($"The specified {nameof(filePath)} parameter cannot be located.", filePath);
        }

        using (var context = new AssimpContext())
        {
            var flags = PostProcessSteps.Triangulate |
                        PostProcessSteps.FlipUVs |
                        PostProcessSteps.GenerateNormals |
                        PostProcessSteps.CalculateTangentSpace |
                        PostProcessSteps.OptimizeGraph |
                        PostProcessSteps.OptimizeMeshes |
                        PostProcessSteps.JoinIdenticalVertices |
                        PostProcessSteps.RemoveRedundantMaterials |
                        PostProcessSteps.SplitLargeMeshes;

            var scene = context.ImportFile(filePath, flags);

            if (scene == null || scene.SceneFlags.HasFlag(SceneFlags.Incomplete) || scene.RootNode == null)
            {
                throw new AssimpException($"Failed to load model using Assimp from path: '{filePath}'");
            }

            string? directory = this.fileSystem.Path.GetDirectoryName(filePath);
            return this.ProcessModel(scene, scene.RootNode, directory);
        }
    }

    private static Dictionary<int, IMaterial> PreLoadMaterials(AssimpScene scene, string? directory)
    {
        var indexToMaterialMap = new Dictionary<int, IMaterial>();

        if (scene.HasMaterials)
        {
            for (int i = 0; i < scene.Materials.Count; i++)
            {
                var assimpMaterial = scene.Materials[i];
                var material = new Material();

                if (assimpMaterial.HasTextureDiffuse)
                {
                    material.DiffuseTexture = ResourceManager.Instance.LoadResource<ITexture2D>($"{directory}\\{assimpMaterial.TextureDiffuse.FilePath}");
                }

                if (assimpMaterial.HasTextureSpecular)
                {
                    material.SpecularTexture = ResourceManager.Instance.LoadResource<ITexture2D>($"{directory}\\{assimpMaterial.TextureSpecular.FilePath}");
                }

                if (assimpMaterial.HasTextureHeight)
                {
                    material.NormalTexture = ResourceManager.Instance.LoadResource<ITexture2D>($"{directory}\\{assimpMaterial.TextureHeight.FilePath}");
                }

                if (assimpMaterial.HasTextureEmissive)
                {
                    material.EmissionTexture = ResourceManager.Instance.LoadResource<ITexture2D>($"{directory}\\{assimpMaterial.TextureEmissive.FilePath}");
                }

                indexToMaterialMap.Add(i, material);
            }
        }

        return indexToMaterialMap;
    }

    private Model ProcessModel(AssimpScene scene, AssimpNode node, string? directory)
    {
        var model = new Model(node.Name);

        var indexToMaterialMap = PreLoadMaterials(scene, directory);

        for (int i = 0; i < node.MeshCount; i++)
        {
            var mesh = scene.Meshes[node.MeshIndices[i]];
            var renderModel = this.ProcessRenderModel(mesh, indexToMaterialMap);

            if (i == 0)
            {
                model.RenderModel = renderModel;
                continue;
            }

            model.AddChild(new Model(mesh.Name)
            {
                RenderModel = renderModel,
            });
        }

        for (int i = 0; i < node.ChildCount; i++)
        {
            model.AddChild(this.ProcessModel(scene, node.Children[i], directory));
        }

        return model;
    }

    private RenderModel ProcessRenderModel(AssimpMesh mesh, IDictionary<int, IMaterial> indexToMaterialMap)
    {
        var vertices = new List<MeshVertex>();
        var indices = new List<int>();

        if (mesh.HasVertices)
        {
            for (int i = 0; i < mesh.VertexCount; i++)
            {
                MeshVertex vertex = default;

                vertex.Position = new Vector3(
                    mesh.Vertices[i].X,
                    mesh.Vertices[i].Y,
                    mesh.Vertices[i].Z);

                if (mesh.HasTextureCoords(0))
                {
                    vertex.TextureCoordinate = new Vector2(
                        mesh.TextureCoordinateChannels[0][i].X,
                        mesh.TextureCoordinateChannels[0][i].Y);
                }

                if (mesh.HasNormals)
                {
                    vertex.Normal = new Vector3(
                        mesh.Normals[i].X,
                        mesh.Normals[i].Y,
                        mesh.Normals[i].Z);
                }

                if (mesh.HasTangentBasis)
                {
                    vertex.Tangent = new Vector3(
                        mesh.Tangents[i].X,
                        mesh.Tangents[i].Y,
                        mesh.Tangents[i].Z);
                }

                vertices.Add(vertex);
            }
        }

        for (int i = 0; i < mesh.FaceCount; i++)
        {
            var face = mesh.Faces[i];

            for (int j = 0; j < face.IndexCount; j++)
            {
                indices.Add(face.Indices[j]);
            }
        }

        return new RenderModel()
        {
            Mesh = new Mesh<MeshVertex>(this.renderDevice.Factory, [.. vertices], [.. indices], MeshVertex.InputElements, MeshVertex.SizeInBytes),
            Material = mesh.MaterialIndex >= 0 ? indexToMaterialMap[mesh.MaterialIndex] : new Material(),
        };
    }
}
