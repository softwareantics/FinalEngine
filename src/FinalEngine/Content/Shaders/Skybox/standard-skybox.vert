#version 460 core

layout (location = 0) in vec3 in_position;

layout (location = 0) out vec3 out_texCoords;

uniform mat4 u_projection;
uniform mat4 u_view;

void main()
{
    out_texCoords = in_position;
    gl_Position = vec4(u_projection * u_view * vec4(in_position, 1.0)).xyww;
}
