using System;
using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace INVOX {

    // Work on packing this down later
    
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct Block {
	public bool  isntAir; // True if the block is empty
	public byte  visibility; // Bitfield, sorta. 1 = U, 2 = D, 4 = E, 8 = W, 16 = N, 32 = S, 64 = reserved, 128 = whole block's visibility. 0 indicates something isn't visible, 1 indicates it is visilbe
	public byte  lighting; // Only uses the first four bytes, might put something else in the upper half later
	public short blockTypeIndex; // Index into the level's blocktype array
    }
    
    class Level {

	public Block [,,] blocks = new Block [Constants.levelSizeX, Constants.levelSizeY, Constants.levelSizeZ];
	public TerrainMesh [,,] meshes = new TerrainMesh [Constants.levelSizeX / Constants.terrainMeshSize, Constants.levelSizeY / Constants.terrainMeshSize, Constants.levelSizeZ / Constants.terrainMeshSize];

	private long seed;

	public Level () {
	    
	    Console.WriteLine("Generating blocks");

	    Random r = new Random();
	    
	    for (int x = 0; x != 128; x++) {
		for (int y = 0; y != 128; y++) {
		    for (int z = 0; z != 128; z++) {
			if (r.Next(2) == 0) {
			    blocks[x,y,z].isntAir = true;
			    blocks [x,y,z].visibility = 255;
			}
		    }
		}
	    }
	    
	    Console.WriteLine("Generating meshes...");
	    
	    for (int mx = 0; mx != meshes.GetLength(0); mx++) {
		for (int my = 0; my != meshes.GetLength(1); my++) {
		    for (int mz = 0; mz != meshes.GetLength(2); mz++) {
			meshes [mx, my, mz] = new TerrainMesh(blocks, mx, my, mz);
			Console.WriteLine("Generated mesh @ " + mx + "," + my + "," + mz);
		    }
		}
	    }
	    Console.WriteLine("Level generated");
	}
	public void drawLevel (Shader shader, Camera camera) {
	    foreach (TerrainMesh mesh in meshes) {
		if (mesh != null)
		    mesh.drawMesh(shader, camera);
	    }
	}
	
    }
}
