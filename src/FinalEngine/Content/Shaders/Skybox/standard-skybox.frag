#version 460 core

layout (location = 0) in vec3 in_texCoords;

layout (location = 0) out vec4 out_color;

uniform samplerCube u_skybox;

void main()
{    
    out_color = texture(u_skybox, in_texCoords);
}
