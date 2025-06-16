#version 460

#include "lighting"

layout (location = 0) in vec2 in_texCoord;

layout (location = 0) out vec4 out_color;

uniform Material u_material;
uniform AmbientLight u_light;

void main()
{
    out_color = vec4(CalculateAmbientLight(u_light, u_material, in_texCoord), 1.0);
}
