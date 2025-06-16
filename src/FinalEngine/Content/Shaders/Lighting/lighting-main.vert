#version 460

layout (location = 0) in vec3 in_position;
layout (location = 1) in vec2 in_texCoord;
layout (location = 2) in vec3 in_normal;
layout (location = 3) in vec3 in_tangent;

layout (location = 0) out vec2 out_texCoord;
layout (location = 1) out vec3 out_fragPos;
layout (location = 2) out mat3 out_tbnMatrix;

uniform mat4 u_projection;
uniform mat4 u_view;
uniform mat4 u_transform;

void main()
{
	out_texCoord = in_texCoord;
    out_fragPos = vec3(u_transform * vec4(in_position, 1.0));

    vec3 t = normalize(vec3(u_transform * vec4(in_tangent, 0.0)));
    vec3 n = normalize(vec3(u_transform * vec4(in_normal, 0.0)));

    // TBN vectors could end up perpendicular, so re-orthogonalize to be safe.
    t = normalize(t - dot(t, n) * n);
    vec3 b = cross(n, t);

    out_tbnMatrix = mat3(t, b, n);

	gl_Position = u_projection * u_view * vec4(out_fragPos, 1.0);
}
