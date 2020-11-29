using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using OpenTK.Mathematics;

namespace INVOX {

    // Work on packing this down later
    
    [StructLayout(LayoutKind.Sequential, Pack=1)]
    public struct Block {
	public bool   isntAir;        // True if the block is empty (Plan to replace when air becomes an actual block type)
	public byte   visibility;     // Bitfield, sorta. 1 = U, 2 = D, 4 = E, 8 = W, 16 = N, 32 = S, 64 = reserved, 128 = are any faces visible?
	public byte   lighting;       // Only uses the first four bytes, might put something else in the upper half later
	public ushort blockTypeIndex; // Index into the level's blocktype array
    }
    
    class Level {

	//public Block [,,] blocks = new Block [Constants.levelSizeX, Constants.levelSizeY, Constants.levelSizeZ];
	public ChunkMesh [,,] meshes = new ChunkMesh [Constants.levelSizeX / Constants.chunkSize, Constants.levelSizeY / Constants.chunkSize, Constants.levelSizeZ / Constants.chunkSize];

	public Chunk [,,] chunks = new Chunk [Constants.levelSizeX / Constants.chunkSize, Constants.levelSizeY / Constants.chunkSize, Constants.levelSizeZ / Constants.chunkSize];

	public List <BlockType> blockTypes = new List <BlockType>();
	
	private long seed;

	public Level () {

	    // Temp, will replace later with a real system that creates the level's block pallete

	    blockTypes.Add(new BlockType(Color4.LightGray));
	    blockTypes.Add(new BlockType(Color4.Brown));
	    blockTypes.Add(new BlockType(Color4.Green));
	    
	    // Init our chunk objects
	    for (int x = 0; x != chunks.GetLength(0); x++) {
		for (int y = 0; y != chunks.GetLength(1); y++) {
		    for (int z = 0; z != chunks.GetLength(2); z++) {
			chunks [x,y,z] = new Chunk();
		    }
		}
	    }
	    
	    // Create our blocks
	    Random r = new Random();
		    
	    for (int x = 0; x != 128; x++) {
		for (int y = 0; y != 123; y++) {
		    for (int z = 0; z != 128; z++) {
			if (r.Next(2) == 0) {
			    Block block = new Block();
			    block.isntAir = true;
			    block.blockTypeIndex = 2;
			    block.lighting = 15;
			    setBlockAt(x,y,z,block);
			}
		    }
		}
	    }

	    for (int x = 0; x != 128; x++) {
		for (int y = 123; y != 127; y++) {
		    for (int z = 0; z != 128; z++) {
			if (r.Next(2) == 0) {
			    Block block = new Block();
			    block.isntAir = true;
			    block.blockTypeIndex = 1;
			    block.lighting = 15;
			    setBlockAt(x,y,z,block);
			}
		    }
		}
	    }

	    for (int x = 0; x != 128; x++) {
		for (int y = 127; y != 128; y++) {
		    for (int z = 0; z != 128; z++) {
			if (r.Next(2) == 0) {
			    Block block = new Block();
			    block.isntAir = true;
			    block.blockTypeIndex = 2;
			    block.lighting = 15;
			    setBlockAt(x,y,z,block);
			}
		    }
		}
	    }
	    
	    // Set up the visibility values for each block
	    for (int x = 0; x != 256; x++) {
		for (int y = 0; y != 256; y++) {
		    for (int z = 0; z != 256; z++) {
			if (getBlockAt(x,y,z).isntAir) {
			    Block block = getBlockAt(x,y,z);
			    // Face visibility
			    if (y == Constants.levelSizeY || !getBlockAt(x, y + 1, z).isntAir || !blockTypes[getBlockAt(x, y + 1, z).blockTypeIndex].isOpaque) block.visibility |=  1;
			    if (y == 0                    || !getBlockAt(x, y - 1, z).isntAir || !blockTypes[getBlockAt(x, y - 1, z).blockTypeIndex].isOpaque) block.visibility |=  2;
			    if (x == Constants.levelSizeX || !getBlockAt(x + 1, y, z).isntAir || !blockTypes[getBlockAt(x + 1, y, z).blockTypeIndex].isOpaque) block.visibility |=  4;
			    if (x == 0                    || !getBlockAt(x - 1, y, z).isntAir || !blockTypes[getBlockAt(x - 1, y, z).blockTypeIndex].isOpaque) block.visibility |=  8;
			    if (z == Constants.levelSizeZ || !getBlockAt(x, y, z + 1).isntAir || !blockTypes[getBlockAt(x, y, z + 1).blockTypeIndex].isOpaque) block.visibility |= 16;
			    if (z == 0                    || !getBlockAt(x, y, z - 1).isntAir || !blockTypes[getBlockAt(x, y, z - 1).blockTypeIndex].isOpaque) block.visibility |= 32;
			    // If, at the end of all this, the block has any faces visible, set its total block visibility to true
			    if ((block.visibility & 63) != 0) block.visibility |= 128;
			    // Set the block @ the position
			    setBlockAt(x,y,z, block);
			}
		    }
		}
	    }
	    	    
	    Console.WriteLine("Level generated");
	}

	public Block getBlockAt (int x, int y, int z) {
	    return chunks [x / Constants.chunkSize, y / Constants.chunkSize, z / Constants.chunkSize].blocks [x % Constants.chunkSize, y % Constants.chunkSize, z % Constants.chunkSize];
	}

	public void setBlockAt (int x, int y, int z, Block block) {
	    chunks [x / Constants.chunkSize, y / Constants.chunkSize, z / Constants.chunkSize].blocks [x % Constants.chunkSize, y % Constants.chunkSize, z % Constants.chunkSize] = block;
	}
	
	public void generateMeshAt (int x, int y, int z) {
	    meshes [x, y, z] = new ChunkMesh(this, x, y, z);
	}
	
	public void drawLevel (Shader shader, Camera camera, Window window) {
	    foreach (ChunkMesh mesh in meshes) {
		if (mesh != null)
		    mesh.drawMesh(shader, camera, window);
	    }
	}
    }
}
