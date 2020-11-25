#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec4 incolor;

uniform mat4 mvp;

out vec4 color;

void main() {
  gl_Position = vec4(aPosition, 1.0) * mvp;
  color = incolor;
}

