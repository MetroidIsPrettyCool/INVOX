using OpenTK.Mathematics;

namespace INVOX {
    class BlockType {
	public Color4 color;
	public bool isOpaque;

	public BlockType () {
	    color = Color4.SpringGreen;
	    isOpaque = true;
	}

	public BlockType (Color4 color) {
	    this.color = color;
	    isOpaque = true;
	}
    }
}
