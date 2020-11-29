using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using System.Collections.Generic;

namespace INVOX {
    class ChunkMesh {

	private static List<int> VAOs = new List<int>();
	private static List<int> VBOs = new List<int>();
	private static List<int> EBOs = new List<int>();
	
	private readonly int meshVAO;
	private readonly int meshEBO;
	private readonly int positionVBO, colorVBO, shimmerVBO;

	private int vertexCount;
	private int indexCount;
	
	private readonly Matrix4 modelMatrix;

	public ChunkMesh (Level level, int offX, int offY, int offZ) {

	    offX *= Constants.chunkSize;
	    offY *= Constants.chunkSize;
	    offZ *= Constants.chunkSize;
	    
	    meshVAO = GL.GenVertexArray();
	    VAOs.Add(meshVAO);
	    GL.BindVertexArray(meshVAO);

	    List<uint> vertexIndices = new List<uint>();
	    List <float> positions = new List<float>();
	    List <float> colors = new List<float>();

	    vertexCount = 0;
	    indexCount = 0;

	    // Clean up later
	    for (int x = offX; x != Constants.chunkSize + offX; x++) {
		for (int y = offY; y != Constants.chunkSize + offY; y++) {
		    for (int z = offZ; z != Constants.chunkSize + offZ; z++) {
			if (level.getBlockAt(x,y,z).isntAir && (level.getBlockAt(x,y,z).visibility & 128) != 0) {
			    // Go through each face and check it's visibility, and if it's visible, add it to the mesh
			    for (int i = 0; i != 6; i++) {
				if ((level.getBlockAt(x, y, z).visibility & (1 << i)) != 0) {
				    // Add face vertexes
				    for (int j = 0; j != 4; j++) {
					positions.Add(Constants.faceVertices [i] [j*3]   + (x % Constants.chunkSize));
					positions.Add(Constants.faceVertices [i] [j*3+1] + (y % Constants.chunkSize));
					positions.Add(Constants.faceVertices [i] [j*3+2] + (z % Constants.chunkSize));
				    }

				    float lightvalue = ((level.getBlockAt(x,y,z).lighting + 15) - (i == 1 ? 3 : i/2)) / 30f;
				    
				    // Rewrite this later to use blockType
				    for (int j = 0; j != 4; j++) {
				        colors.Add(level.blockTypes[level.getBlockAt(x,y,z).blockTypeIndex].getFaceColor(i).R * lightvalue);
					colors.Add(level.blockTypes[level.getBlockAt(x,y,z).blockTypeIndex].getFaceColor(i).G * lightvalue);
					colors.Add(level.blockTypes[level.getBlockAt(x,y,z).blockTypeIndex].getFaceColor(i).B * lightvalue);
					colors.Add(level.blockTypes[level.getBlockAt(x,y,z).blockTypeIndex].getFaceColor(i).A);
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

	public void drawMesh (Shader shader, Camera camera, Window window) {
	    GL.BindVertexArray(meshVAO);
	    
	    shader.Use();
	    Shader.setMat4(shader.Handle, modelMatrix * camera.getViewMatrix() * window.getProjectionMatrix(), "mvp");

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
