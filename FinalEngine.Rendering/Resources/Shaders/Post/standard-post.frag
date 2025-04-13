#version 460

#include "effects"
  
layout (location = 0) in vec2 in_texCoord;

layout (location = 0) out vec4 out_color;

uniform sampler2D u_screenTexture;

uniform ToneMappingRenderEffect u_toneMapping;
uniform InversionRenderEffect u_inversion;

void main()
{
    vec3 color = texture(u_screenTexture, in_texCoord).rgb;

    color = CalculateInversion(u_inversion, color, in_texCoord);
    color = CalculateToneMapping(u_toneMapping, color, in_texCoord);

    out_color = vec4(color, 1.0);
}
