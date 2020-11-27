using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using OpenTK.Mathematics;

namespace INVOX {

    // Work on packing this down later
    
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct Block {
	public bool  isntAir; // True if the block is empty
	public byte  visibility; // Bitfield, sorta. 1 = U, 2 = D, 4 = E, 8 = W, 16 = N, 32 = S, 64 = reserved, 128 = whole block's visibility. 0 indicates something isn't visible, 1 indicates it is visible
	public byte  lighting; // Only uses the first four bytes, might put something else in the upper half later
	public short blockTypeIndex; // Index into the level's blocktype array
    }
    
    class Level {

	public Block [,,] blocks = new Block [Constants.levelSizeX, Constants.levelSizeY, Constants.levelSizeZ];
	public TerrainMesh [,,] meshes = new TerrainMesh [Constants.levelSizeX / Constants.terrainMeshSize, Constants.levelSizeY / Constants.terrainMeshSize, Constants.levelSizeZ / Constants.terrainMeshSize];

	public List <BlockType> blockTypes = new List <BlockType>();
	
	private long seed;

	public Level () {

	    // Temp, will replace later with a real system that creates the level's block pallete
	    blockTypes.Add(new BlockType());
	    
	    Console.WriteLine("Generating blocks");

	    Random r = new Random();

	    // Create our blocks
	    for (int x = 0; x != 128; x++) {
		for (int y = 0; y != 128; y++) {
		    for (int z = 0; z != 128; z++) {
			if (r.Next(2) == 0) {
			    blocks[x,y,z].isntAir = true;
			}
			else {
			    blocks [x,y,z].isntAir = false;
			}
		    }
		}
	    }

	    // Set up the visibility values for each block
	    for (int x = 0; x != 128; x++) {
		for (int y = 0; y != 128; y++) {
		    for (int z = 0; z != 128; z++) {
			if (blocks [x,y,z].isntAir) {
			    // Face visibility
			    if (y == Constants.levelSizeY || !blocks [x, y + 1, z].isntAir || !blockTypes[blocks [x, y + 1, z].blockTypeIndex].isOpaque) blocks [x,y,z].visibility |=  1;
			    if (y == 0                    || !blocks [x, y - 1, z].isntAir || !blockTypes[blocks [x, y - 1, z].blockTypeIndex].isOpaque) blocks [x,y,z].visibility |=  2;
			    if (x == Constants.levelSizeX || !blocks [x + 1, y, z].isntAir || !blockTypes[blocks [x + 1, y, z].blockTypeIndex].isOpaque) blocks [x,y,z].visibility |=  4;
			    if (x == 0                    || !blocks [x - 1, y, z].isntAir || !blockTypes[blocks [x - 1, y, z].blockTypeIndex].isOpaque) blocks [x,y,z].visibility |=  8;
			    if (z == Constants.levelSizeZ || !blocks [x, y, z + 1].isntAir || !blockTypes[blocks [x, y, z + 1].blockTypeIndex].isOpaque) blocks [x,y,z].visibility |= 16;
			    if (z == 0                    || !blocks [x, y, z - 1].isntAir || !blockTypes[blocks [x, y, z - 1].blockTypeIndex].isOpaque) blocks [x,y,z].visibility |= 32;
			    // If, at the end of all this, the block has any faces visible, set its total block visibility to true
			    if ((blocks [x,y,z].visibility & 63) != 0) blocks [x,y,z].visibility |= 128;
			}
		    }
		}
	    }
	    	    
	    Console.WriteLine("Level generated");
	}

	public void generateMeshAt (int x, int y, int z) {
	    meshes [x, y, z] = new TerrainMesh(blocks, x, y, z);
	    Console.WriteLine("Generated mesh @ " + x + "," + y + "," + z);
	}
	
	public void drawLevel (Shader shader, Camera camera) {
	    foreach (TerrainMesh mesh in meshes) {
		if (mesh != null)
		    mesh.drawMesh(shader, camera);
	    }
	}
	
    }
}
