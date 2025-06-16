#ifndef EFFECTS_GLSL
#define EFFECTS_GLSL

#define HDR_TYPE_REINHARD 0
#define HDR_TYPE_EXPOSURE 1

#define FOG_TYPE_LINEAR 0
#define FOG_TYPE_EXP 1
#define FOG_TYPE_EXP2 2

struct RenderEffectBase
{
    bool enabled;
};

struct ToneMappingRenderEffect
{
    float exposure;
    int type;
    RenderEffectBase base;
};

struct InversionRenderEffect
{
    RenderEffectBase base;
};

struct FogRenderEffect
{
    vec3 color;
    float start;
    float end;
    float density;
    int type;

    RenderEffectBase base;
};

struct GammaCorrectionRenderEffect
{
    float amount;
    RenderEffectBase base;
};

vec3 ReconstructViewPosition(sampler2D depthTexture, vec2 texCoord, mat4 inverseProjection)
{
    // Get the depth in normalized device coordinates.
    float ndc_z = texture(depthTexture, texCoord).r * 2.0 - 1.0;

    // Get the texture coordinate in normalized device coordinates.
    vec2 ndc_xy = texCoord * 2.0 - 1.0;

    // Build a clip-space position (NDC x/y from uv, z from depth)
    vec4 clip = vec4(ndc_xy, ndc_z, 1.0);

    // Unproject to view space
    vec4 view = inverseProjection * clip;
    view /= view.w;

    return view.xyz;
}

vec3 CalculateGammaCorrection(GammaCorrectionRenderEffect gamma, vec3 color)
{
    if (!gamma.base.enabled)
    {
        return color;
    }

    return pow(color.rgb, vec3(1.0 / gamma.amount));
}

vec3 CalculateExposureToneMapping(float exposure, vec3 color, vec2 texCoord)
{
    return vec3(1.0) - exp(-color * exposure);
}

vec3 CalculateReinhardToneMapping(vec3 color, vec2 texCoord)
{
    return color / (color + vec3(1.0));
}

vec3 CalculateToneMapping(ToneMappingRenderEffect effect, vec3 color, vec2 texCoord)
{
    vec3 result = color;

    if (!effect.base.enabled)
    {
        return result;
    }
    else
    {
        switch (effect.type)
        {
            case HDR_TYPE_REINHARD:
                result = CalculateReinhardToneMapping(color, texCoord);
                break;

            case HDR_TYPE_EXPOSURE:
                result = CalculateExposureToneMapping(effect.exposure, color, texCoord);
                break;
        }
    }

    return result;
}

vec3 CalculateInversion(InversionRenderEffect effect, vec3 color, vec2 texCoord)
{
    if (!effect.base.enabled)
    {
        return  color;
    }

    return vec3(1.0 - color);
}

float CalculateLinearFog(float start, float end, float coord)
{
    float fogLength = end - start;
	return (end - coord) / fogLength;
}

float CalculateExponentialFog(float density, float coord)
{
    return exp(-density * coord);
}

float CalculateExponentialSquaredFog(float density, float coord)
{
    return exp(-pow(density * coord, 2.0));
}

vec3 CalculateFog(FogRenderEffect fog, float coord, vec3 color)
{
    if (!fog.base.enabled)
    {
        return color;
    }

    float fogFactor = 0.0;
    float fogCoord = abs(coord);

    switch (fog.type)
    {
        case FOG_TYPE_LINEAR:
            fogFactor = CalculateLinearFog(fog.start, fog.end, fogCoord);
            break;

        case FOG_TYPE_EXP:
            fogFactor = CalculateExponentialFog(fog.density, fogCoord);
            break;

        case FOG_TYPE_EXP2:
            fogFactor = CalculateExponentialSquaredFog(fog.density, fogCoord);
            break;
    }

	fogFactor = 1.0 - clamp(fogFactor, 0.0, 1.0);
    return mix(color, fog.color.rgb, fogFactor);
}

#endif // EFFECTS_GLSL
