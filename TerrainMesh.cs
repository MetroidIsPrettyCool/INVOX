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

	    List<uint> vertexIndices = new List<uint>();
	    List<Vertex> vertexList = new List<Vertex>();
	    
	    // Temporary, just for testing the block class
	    Block block = new Block();
	    for (int i = 0; i != 6; i++) {
		Face f = block.getFace(i);
		vertexList.Add(f.vertex1);
		vertexList.Add(f.vertex2);
		vertexList.Add(f.vertex3);
		vertexList.Add(f.vertex4);
		
		// 4 = the number of vertices in a cube face (a quad)
		vertexIndices.Add(0 + (uint)i*4);
		vertexIndices.Add(1 + (uint)i*4);
		vertexIndices.Add(2 + (uint)i*4);

		vertexIndices.Add(0 + (uint)i*4);
		vertexIndices.Add(2 + (uint)i*4);
		vertexIndices.Add(3 + (uint)i*4);
	    }
	    
	    Vector3 [] vertices = new Vector3 [vertexList.Count];
	    Color4 [] colors = new Color4 [vertexList.Count];

	    for (int i = 0; i != vertexList.Count; i++) {
		vertices [i] = vertexList[i].position;
		colors [i] = vertexList[i].color;
	    }

	    meshEBO = GL.GenBuffer();
	    EBOs.Add(meshEBO);
	    GL.BindBuffer(BufferTarget.ElementArrayBuffer, meshEBO);
	    GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndices.Count * sizeof(uint), vertexIndices.ToArray(), BufferUsageHint.StaticDraw);
	    
	    positionVBO = GL.GenBuffer();
	    VBOs.Add(positionVBO);
	    GL.BindBuffer(BufferTarget.ArrayBuffer, positionVBO);
	    GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float) * 3, vertices, BufferUsageHint.StaticDraw);
	    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
	    GL.EnableVertexAttribArray(0);
	    
	    colorVBO = GL.GenBuffer();
	    VBOs.Add(colorVBO);
	    GL.BindBuffer(BufferTarget.ArrayBuffer, colorVBO);
	    GL.BufferData(BufferTarget.ArrayBuffer, colors.Length * sizeof(float) * 4, colors, BufferUsageHint.StaticDraw);
	    GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, 0);
	    GL.EnableVertexAttribArray(1);

	    GL.BindVertexArray(0);
	    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

	    vertexCount = vertexIndices.Count;
	    
	    modelMatrix = mat;
	}

	public void drawMesh (Shader shader, Camera camera) {
	    GL.BindVertexArray(meshVAO);
	    
	    shader.Use();
	    Shader.setMat4(shader.Handle, modelMatrix * camera.getViewMatrix() * camera.getProjectionMatrix(), "mvp");

	    GL.DrawElements(PrimitiveType.Triangles, vertexCount, DrawElementsType.UnsignedInt, 0);
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
