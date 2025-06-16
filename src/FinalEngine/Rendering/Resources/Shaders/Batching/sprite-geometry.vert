#version 460

layout(location = 0) in vec2 in_position;
layout(location = 1) in vec4 in_color;
layout(location = 2) in vec2 in_texCoord;
layout(location = 3) in float in_textureID;

layout(location = 0) out vec4 out_color;
layout(location = 1) out vec2 out_texCoord;
layout(location = 2) out float out_textureID;

uniform mat4 u_projection;
uniform mat4 u_transform;

void main()
{
    out_color = in_color;
    out_texCoord = vec2(in_texCoord.x, 1.0 - in_texCoord.y);;
    out_textureID = in_textureID;

    gl_Position = u_projection * u_transform * vec4(in_position, 0.0, 1.0);
}
