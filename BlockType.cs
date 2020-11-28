using OpenTK.Mathematics;

namespace INVOX {
    class BlockType {
	public Color4 color;
	public bool isOpaque;
	public bool isSolid;
	
	public BlockType () {
	    color = Color4.SpringGreen;
	    isOpaque = true;
	    isSolid = true;
	}

	public BlockType (Color4 color) {
	    this.color = color;
	    isOpaque = true;
	    isSolid = true;
	}

	public Color4 getFaceColor (int face) {
	    return color;
	}
	
    }
}
