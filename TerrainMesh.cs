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
	private readonly int indexCount;
	
	private readonly Matrix4 modelMatrix;

	// Temp constructor
	public TerrainMesh (Block [,,] blocks, int offX, int offY, int offZ) {

	    offX *= Constants.terrainMeshSize;
	    offY *= Constants.terrainMeshSize;
	    offZ *= Constants.terrainMeshSize;
	    
	    meshVAO = GL.GenVertexArray();
	    VAOs.Add(meshVAO);
	    GL.BindVertexArray(meshVAO);

	    List<uint> vertexIndices = new List<uint>();
	    List <float> positions = new List<float>();
	    List <float> colors = new List<float>();

	    vertexCount = 0;
	    indexCount = 0;

	    // Clean up later
	    for (int x = offX; x != Constants.terrainMeshSize + offX; x++) {
		for (int y = offY; y != Constants.terrainMeshSize + offY; y++) {
		    for (int z = offZ; z != Constants.terrainMeshSize + offZ; z++) {
			if (blocks [x,y,z].isntAir && (blocks [x,y,z].visibility & 128) != 0) {
			    if ((blocks [x, y, z].visibility & 1) != 0) {
				// Add face vertexes
				for (int i = 0; i != 4; i++) {
				    positions.Add(Constants.uFaceVertices [i * 3]     + (x % Constants.terrainMeshSize));
				    positions.Add(Constants.uFaceVertices [i * 3 + 1] + (y % Constants.terrainMeshSize));
				    positions.Add(Constants.uFaceVertices [i * 3 + 2] + (z % Constants.terrainMeshSize));
				}

				for (int i = 0; i != 4; i++) {
				    colors.Add(1);
				    colors.Add(0);
				    colors.Add(0);
				    colors.Add(1);
				}

				vertexIndices.Add(3 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(2 + (uint)vertexCount);
				
				vertexCount += 4;
				indexCount += 6;
			    }
			    if ((blocks [x, y, z].visibility & 2) != 0) {
				// Add face vertexes
			        for (int i = 0; i != 4; i++) {
				    positions.Add(Constants.dFaceVertices [i * 3]     + (x % Constants.terrainMeshSize));
				    positions.Add(Constants.dFaceVertices [i * 3 + 1] + (y % Constants.terrainMeshSize));
				    positions.Add(Constants.dFaceVertices [i * 3 + 2] + (z % Constants.terrainMeshSize));
				}

				for (int i = 0; i != 4; i++) {
				    colors.Add(0.9f);
				    colors.Add(0);
				    colors.Add(0);
				    colors.Add(1);
				}

				vertexIndices.Add(3 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(2 + (uint)vertexCount);
				
				vertexCount += 4;
				indexCount += 6;
			    }
			    if ((blocks [x, y, z].visibility & 4) != 0) {
				// Add face vertexes
			        for (int i = 0; i != 4; i++) {
				    positions.Add(Constants.eFaceVertices [i * 3]     + (x % Constants.terrainMeshSize));
				    positions.Add(Constants.eFaceVertices [i * 3 + 1] + (y % Constants.terrainMeshSize));
				    positions.Add(Constants.eFaceVertices [i * 3 + 2] + (z % Constants.terrainMeshSize));
				}

				for (int i = 0; i != 4; i++) {
				    colors.Add(0.9666666f);
				    colors.Add(0);
				    colors.Add(0);
				    colors.Add(1);
				}

				vertexIndices.Add(3 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(2 + (uint)vertexCount);
				
				vertexCount += 4;
				indexCount += 6;
			    }
			    if ((blocks [x, y, z].visibility & 8) != 0) {
				// Add face vertexes
			        for (int i = 0; i != 4; i++) {
				    positions.Add(Constants.wFaceVertices [i * 3]     + (x % Constants.terrainMeshSize));
				    positions.Add(Constants.wFaceVertices [i * 3 + 1] + (y % Constants.terrainMeshSize));
				    positions.Add(Constants.wFaceVertices [i * 3 + 2] + (z % Constants.terrainMeshSize));
				}

				for (int i = 0; i != 4; i++) {
				    colors.Add(0.9666666f);
				    colors.Add(0);
				    colors.Add(0);
				    colors.Add(1);
				}

				vertexIndices.Add(3 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(2 + (uint)vertexCount);
				
				vertexCount += 4;
				indexCount += 6;
			    }
			    if ((blocks [x, y, z].visibility & 16) != 0) {
				// Add face vertexes
			        for (int i = 0; i != 4; i++) {
				    positions.Add(Constants.nFaceVertices [i * 3]     + (x % Constants.terrainMeshSize));
				    positions.Add(Constants.nFaceVertices [i * 3 + 1] + (y % Constants.terrainMeshSize));
				    positions.Add(Constants.nFaceVertices [i * 3 + 2] + (z % Constants.terrainMeshSize));
				}

				for (int i = 0; i != 4; i++) {
				    colors.Add(0.93333333f);
				    colors.Add(0);
				    colors.Add(0);
				    colors.Add(1);
				}

				vertexIndices.Add(3 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(2 + (uint)vertexCount);
				
				vertexCount += 4;
				indexCount += 6;
			    }
			    if ((blocks [x, y, z].visibility & 32) != 0) {
				// Add face vertexes
			        for (int i = 0; i != 4; i++) {
				    positions.Add(Constants.sFaceVertices [i * 3]     + (x % Constants.terrainMeshSize));
				    positions.Add(Constants.sFaceVertices [i * 3 + 1] + (y % Constants.terrainMeshSize));
				    positions.Add(Constants.sFaceVertices [i * 3 + 2] + (z % Constants.terrainMeshSize));
				}

				for (int i = 0; i != 4; i++) {
				    colors.Add(0.93333333f);
				    colors.Add(0);
				    colors.Add(0);
				    colors.Add(1);
				}

				vertexIndices.Add(3 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(0 + (uint)vertexCount);
				vertexIndices.Add(1 + (uint)vertexCount);
				vertexIndices.Add(2 + (uint)vertexCount);
				
				vertexCount += 4;
				indexCount += 6;
			    }
			}
		    }
		}
	    }
	    
	    meshEBO = GL.GenBuffer();
	    EBOs.Add(meshEBO);
	    GL.BindBuffer(BufferTarget.ElementArrayBuffer, meshEBO);
	    GL.BufferData(BufferTarget.ElementArrayBuffer, vertexIndices.Count * sizeof(uint), vertexIndices.ToArray(), BufferUsageHint.StaticDraw);
	    
	    positionVBO = GL.GenBuffer();
	    VBOs.Add(positionVBO);
	    GL.BindBuffer(BufferTarget.ArrayBuffer, positionVBO);
	    GL.BufferData(BufferTarget.ArrayBuffer, positions.Count * sizeof(float), positions.ToArray(), BufferUsageHint.StaticDraw);
	    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
	    GL.EnableVertexAttribArray(0);
	    
	    colorVBO = GL.GenBuffer();
	    VBOs.Add(colorVBO);
	    GL.BindBuffer(BufferTarget.ArrayBuffer, colorVBO);
	    GL.BufferData(BufferTarget.ArrayBuffer, colors.Count * sizeof(float), colors.ToArray(), BufferUsageHint.StaticDraw);
	    GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 0, 0);
	    GL.EnableVertexAttribArray(1);

	    GL.BindVertexArray(0);
	    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
	    
	    modelMatrix = Matrix4.CreateTranslation(offX, offY, offZ);
	}

	public void drawMesh (Shader shader, Camera camera) {
	    GL.BindVertexArray(meshVAO);
	    
	    shader.Use();
	    Shader.setMat4(shader.Handle, modelMatrix * camera.getViewMatrix() * camera.getProjectionMatrix(), "mvp");

	    GL.DrawElements(PrimitiveType.Triangles, indexCount, DrawElementsType.UnsignedInt, 0);
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
