#version 460

#include "lighting"

layout (location = 0) in vec2 in_texCoord;

layout (location = 0) out vec4 out_color;

uniform Material u_material;

void main()
{
    out_color = texture(u_material.diffuseTexture, in_texCoord).rgba;
}
