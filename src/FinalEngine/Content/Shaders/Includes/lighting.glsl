#ifndef LIGHTING_GLSL
#define LIGHTING_GLSL

#include "material"

struct LightBase
{
    float intensity;
    vec3 color;
};

struct AmbientLight
{
    LightBase base;
};

struct DirectionalLight
{
    LightBase base;
    vec3 direction;
};

struct Attenuation
{
    float constant;
    float linear;
    float quadratic;
};

struct PointLight
{
    LightBase base;
    Attenuation attenuation;
    vec3 position;
};

struct SpotLight
{
    LightBase base;
    Attenuation attenuation;
    vec3 direction;
    vec3 position;
    float radius;
    float outerRadius;
};

vec3 CalculateLight(LightBase light, Material material, vec3 direction, vec3 normal, vec3 viewPosition, vec3 fragPosition, vec2 texCoord)
{
    normal = normalize(normal);
    direction = normalize(direction);

    vec3 diffuseColor = vec3(0, 0, 0);
    vec3 specularColor = vec3(0, 0, 0);

    // As the angle widens, diffuse shading decreases.
    float diffuseShading = max(dot(direction, normal), 0.0);

    if (diffuseShading > 0)
    {
        diffuseColor = diffuseShading * light.color * light.intensity * texture(material.diffuseTexture, texCoord).rgb;

        // Now let's measure the angle between the normal and halfway point of the light and view direction.
        vec3 viewDirection = normalize(viewPosition - fragPosition);
        vec3 halfWayDirection = normalize(direction + viewDirection);

        // As the angle widens, specular shading decreases.
        // This is why we raise to the power of shininess.
        float specularShading = pow(max(dot(normal, halfWayDirection), 0.0), material.shininess);

        if (specularShading > 0)
        {
            specularColor = specularShading * light.color * light.intensity * texture(material.specularTexture, texCoord).rgb;
        }
    }

    // Calculate emission map here as it's a lighting effect.
    vec3 emissionColor = texture(material.emissionTexture, texCoord).rgb;

    return diffuseColor + specularColor + emissionColor;
}

vec3 CalculateDirectionalLight(DirectionalLight light, Material material, vec3 normal, vec3 viewPosition, vec3 fragPosition, vec2 texCoord)
{
    return CalculateLight(light.base, material, -light.direction, normal, viewPosition, fragPosition, texCoord);
}

float CalculateAttenuation(Attenuation attenuation, vec3 position, vec3 fragPosition)
{
    // The distance between the light and the fragment position.
    float dist = length(position - fragPosition);

    // Use the quadratic equation to determine the lights attenuation over the distance.
    return 1.0 / (attenuation.constant + attenuation.linear * dist + attenuation.quadratic * (dist * dist));
}

vec3 CalculatePointLight(PointLight light, Material material, vec3 normal, vec3 viewPosition, vec3 fragPosition, vec2 texCoord)
{
    float attenuation = CalculateAttenuation(light.attenuation, light.position, fragPosition);
    vec3 lightColor = CalculateLight(light.base, material, light.position - fragPosition, normal, viewPosition, fragPosition, texCoord);

    return lightColor * attenuation;
}

vec3 CalculateSpotLight(SpotLight light, Material material, vec3 normal, vec3 viewPosition, vec3 fragPosition, vec2 texCoord)
{
    vec3 lightDirection = normalize(light.position - fragPosition);

    // Calculate the cut off radius
    float theta = dot(lightDirection, normalize(-light.direction));
    float epsilon = light.radius - light.outerRadius;
    float intensity = clamp((theta - light.outerRadius) / epsilon, 0.0, 1.0);

    float attenuation = CalculateAttenuation(light.attenuation, light.position, fragPosition);
    vec3 lightColor = CalculateLight(light.base, material, lightDirection, normal, viewPosition, fragPosition, texCoord);

    return lightColor * attenuation * intensity;
}

vec3 CalculateAmbientLight(AmbientLight light, Material material, vec2 texCoord)
{
    return light.base.color * light.base.intensity * texture(material.diffuseTexture, texCoord).rgb;
}

#endif // LIGHTING_GLSL
