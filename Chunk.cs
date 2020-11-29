using OpenTK.Mathematics;

using System.Collections.Generic;

namespace INVOX {
    class Chunk {
	public Block [,,] blocks = new Block [Constants.chunkSize,Constants.chunkSize,Constants.chunkSize];

	// Gonna do a lot more with these later
	public Chunk () {
	}
    }
}
