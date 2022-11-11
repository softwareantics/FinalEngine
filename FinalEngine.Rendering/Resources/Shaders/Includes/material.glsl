#ifndef MATERIAL_GLSL
#define MATERIAL_GLSL

struct Material
{
    sampler2D diffuseTexture;
    sampler2D specularTexture;
    float shininess;
};

#endif // MATERIAL_GLSL
