#version 460

layout (location = 0) in vec4 in_color;
layout (location = 1) in vec2 in_texCoord;
layout (location = 2) in float in_textureID;

out vec4 out_color;

uniform sampler2D u_textures[32];

void main()
{
    vec4 color = in_color;

    switch(int(in_textureID))
    {
        case 0: color *= texture(u_textures[0], in_texCoord); break;
        case 1: color *= texture(u_textures[1], in_texCoord); break;
        case 2: color *= texture(u_textures[2], in_texCoord); break;
        case 3: color *= texture(u_textures[3], in_texCoord); break;
        case 4: color *= texture(u_textures[4], in_texCoord); break;
        case 5: color *= texture(u_textures[5], in_texCoord); break;
        case 6: color *= texture(u_textures[6], in_texCoord); break;
        case 7: color *= texture(u_textures[7], in_texCoord); break;
        case 8: color *= texture(u_textures[8], in_texCoord); break;
        case 9: color *= texture(u_textures[9], in_texCoord); break;
        case 10: color *= texture(u_textures[10], in_texCoord); break;
        case 11: color *= texture(u_textures[11], in_texCoord); break;
        case 12: color *= texture(u_textures[12], in_texCoord); break;
        case 13: color *= texture(u_textures[13], in_texCoord); break;
        case 14: color *= texture(u_textures[14], in_texCoord); break;
        case 15: color *= texture(u_textures[15], in_texCoord); break;
        case 16: color *= texture(u_textures[16], in_texCoord); break;
        case 17: color *= texture(u_textures[17], in_texCoord); break;
        case 18: color *= texture(u_textures[18], in_texCoord); break;
        case 19: color *= texture(u_textures[19], in_texCoord); break;
        case 20: color *= texture(u_textures[20], in_texCoord); break;
        case 21: color *= texture(u_textures[21], in_texCoord); break;
        case 22: color *= texture(u_textures[22], in_texCoord); break;
        case 23: color *= texture(u_textures[23], in_texCoord); break;
        case 24: color *= texture(u_textures[24], in_texCoord); break;
        case 25: color *= texture(u_textures[25], in_texCoord); break;
        case 26: color *= texture(u_textures[26], in_texCoord); break;
        case 27: color *= texture(u_textures[27], in_texCoord); break;
        case 28: color *= texture(u_textures[28], in_texCoord); break;
        case 29: color *= texture(u_textures[29], in_texCoord); break;
        case 30: color *= texture(u_textures[30], in_texCoord); break;
        case 31: color *= texture(u_textures[31], in_texCoord); break;
    }

    out_color = color;
}
