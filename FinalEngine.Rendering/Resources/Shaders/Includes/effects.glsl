#ifndef EFFECTS_GLSL
#define EFFECTS_GLSL

#define HDR_TYPE_REINHARD 0
#define HDR_TYPE_EXPOSURE 1

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
    vec3 result = vec3(0);

    if (!effect.base.enabled)
    {
        result = color;
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
        return color;
    }

    return vec3(1.0 - color);
}

#endif // EFFECTS_GLSL
