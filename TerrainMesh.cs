using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using System.Collections.Generic;

namespace INVOX {
    class TerrainMesh {

	private static List<int> VAOs = new List<int>();
	private static List<int> VBOs = new List<int>();
	private static List<int> EBOs = new List<int>();
	
	private readonly int meshVAO;
	private readonly int meshEBO;
	private readonly int positionVBO, colorVBO, shimmerVBO;

	private readonly int vertexCount;
	
	private readonly Matrix4 modelMatrix;

	// Temp constructor
	public TerrainMesh (Matrix4 mat) {
	    meshVAO = GL.GenVertexArray();
	    VAOs.Add(meshVAO);
	    GL.BindVertexArray(meshVAO);

	    float [] vertices = {-1.0f,-1.0f,-1.0f,-1.0f,-1.0f, 1.0f,-1.0f, 1.0f, 1.0f,1.0f, 1.0f,-1.0f,-1.0f,-1.0f,-1.0f,-1.0f, 1.0f,-1.0f,1.0f,-1.0f, 1.0f,-1.0f,-1.0f,-1.0f,1.0f,-1.0f,-1.0f,1.0f, 1.0f,-1.0f,1.0f,-1.0f,-1.0f,-1.0f,-1.0f,-1.0f,-1.0f,-1.0f,-1.0f,-1.0f, 1.0f, 1.0f,-1.0f, 1.0f,-1.0f,1.0f,-1.0f, 1.0f,-1.0f,-1.0f, 1.0f,-1.0f,-1.0f,-1.0f,-1.0f, 1.0f, 1.0f,-1.0f,-1.0f, 1.0f,1.0f,-1.0f, 1.0f,1.0f, 1.0f, 1.0f,1.0f,-1.0f,-1.0f,1.0f, 1.0f,-1.0f,1.0f,-1.0f,-1.0f,1.0f, 1.0f, 1.0f,1.0f,-1.0f, 1.0f,1.0f, 1.0f, 1.0f,1.0f, 1.0f,-1.0f,-1.0f, 1.0f,-1.0f,1.0f, 1.0f, 1.0f,-1.0f, 1.0f,-1.0f,-1.0f, 1.0f, 1.0f,1.0f, 1.0f, 1.0f,-1.0f, 1.0f, 1.0f,1.0f,-1.0f, 1.0f};
	    
	    positionVBO = GL.GenBuffer();
	    VBOs.Add(positionVBO);
	    GL.BindBuffer(BufferTarget.ArrayBuffer, positionVBO);
	    GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
	    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
	    GL.EnableVertexAttribArray(0);

	    float [] colors = {0.583f,0.771f,0.014f,0.609f,0.115f,0.436f,0.327f,0.483f,0.844f,0.822f,0.569f,0.201f,0.435f,0.602f,0.223f,0.310f,0.747f,0.185f,0.597f,0.770f,0.761f,0.559f,0.436f,0.730f,0.359f,0.583f,0.152f,0.483f,0.596f,0.789f,0.559f,0.861f,0.639f,0.195f,0.548f,0.859f,0.014f,0.184f,0.576f,0.771f,0.328f,0.970f,0.406f,0.615f,0.116f,0.676f,0.977f,0.133f,0.971f,0.572f,0.833f,0.140f,0.616f,0.489f,0.997f,0.513f,0.064f,0.945f,0.719f,0.592f,0.543f,0.021f,0.978f,0.279f,0.317f,0.505f,0.167f,0.620f,0.077f,0.347f,0.857f,0.137f,0.055f,0.953f,0.042f,0.714f,0.505f,0.345f,0.783f,0.290f,0.734f,0.722f,0.645f,0.174f,0.302f,0.455f,0.848f,0.225f,0.587f,0.040f,0.517f,0.713f,0.338f,0.053f,0.959f,0.120f,0.393f,0.621f,0.362f,0.673f,0.211f,0.457f,0.820f,0.883f,0.371f,0.982f,0.099f,0.879f};

	    colorVBO = GL.GenBuffer();
	    VBOs.Add(colorVBO);
	    GL.BindBuffer(BufferTarget.ArrayBuffer, colorVBO);
	    GL.BufferData(BufferTarget.ArrayBuffer, colors.Length * sizeof(float), colors, BufferUsageHint.StaticDraw);
	    GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
	    GL.EnableVertexAttribArray(1);

	    GL.BindVertexArray(0);
	    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

	    vertexCount = vertices.Length / 3;
	    
	    modelMatrix = mat;
	}

	public void drawMesh (Shader shader, Camera camera) {
	    GL.BindVertexArray(meshVAO);
	    
	    shader.Use();
	    Shader.setMat4(shader.Handle, modelMatrix * camera.getViewMatrix() * camera.getProjectionMatrix(), "mvp");

	    GL.DrawArrays(PrimitiveType.Triangles, 0, vertexCount);
	}

	public void deleteResources () {
	    GL.BindVertexArray(0);
	    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
	    GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
	    GL.DeleteVertexArrays(VAOs.Count, VAOs.ToArray());
	    GL.DeleteBuffers(VBOs.Count, VBOs.ToArray());
	    GL.DeleteBuffers(EBOs.Count, EBOs.ToArray());
	}
    }
}
