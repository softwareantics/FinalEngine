#ifndef SAMPLING_GLSL
#define SAMPLING_GLSL

vec3 CalculateNormal(mat3 tbnMatrix, sampler2D normalTexture, vec2 texCoord)
{
    // Take the TBN matrix and transform the normal to world space.
    return normalize(tbnMatrix * (2.0 * texture(normalTexture, texCoord).rgb - 1.0));
}

vec3 CalculateViewDirection(mat3 tbnMatrix, vec3 viewPosition, vec3 fragPosition)
{
    vec3 tangentViewPosition = tbnMatrix * viewPosition;
    vec3 tangentFragPosition = tbnMatrix * fragPosition;

    return normalize(tangentViewPosition - tangentFragPosition);
}

#endif // SAMPLING_GLSL
