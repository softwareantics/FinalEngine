#version 460

#include "effects"

layout (location = 0) in vec2 in_texCoord;

layout (location = 0) out vec4 out_color;

uniform sampler2D u_screenTexture;
uniform sampler2D u_depthTexture;

uniform mat4 u_inverseProjection;

uniform GammaCorrectionRenderEffect u_gamma;
uniform ToneMappingRenderEffect u_toneMapping;
uniform InversionRenderEffect u_inversion;
uniform FogRenderEffect u_fog;

void main()
{
    vec3 color = texture(u_screenTexture, in_texCoord).rgb;
    vec3 viewPosition = ReconstructViewPosition(u_depthTexture, in_texCoord, u_inverseProjection);

    color = CalculateInversion(u_inversion, color, in_texCoord);
    color = CalculateFog(u_fog, viewPosition.z, color);
    color = CalculateToneMapping(u_toneMapping, color, in_texCoord);
    color = CalculateGammaCorrection(u_gamma, color);

    out_color = vec4(color, 1.0);
}
