using OpenTK.Mathematics;

namespace INVOX {
    class Block {

	// Plan to rewrite this class to cache its faces
	
	private static Vector3 [] vertex1Positions {get;} = {
	    new Vector3 ( 1, 1, 1),
	    new Vector3 ( 1,-1,-1),
	    new Vector3 (-1, 1,-1),
	    new Vector3 ( 1, 1, 1),
	    new Vector3 (-1, 1, 1),
	    new Vector3 ( 1, 1,-1)
	};
	
	private static Vector3 [] vertex2Positions {get;} = {
	    new Vector3 (-1, 1, 1),
	    new Vector3 (-1,-1,-1),
	    new Vector3 (-1, 1, 1),
	    new Vector3 ( 1, 1,-1),
	    new Vector3 ( 1, 1, 1),
	    new Vector3 (-1, 1,-1)
	};

	private static Vector3 [] vertex3Positions {get;} = {
	    new Vector3 (-1, 1,-1),
	    new Vector3 (-1,-1, 1),
	    new Vector3 (-1,-1, 1),
	    new Vector3 ( 1,-1,-1),
	    new Vector3 ( 1,-1, 1),
	    new Vector3 (-1,-1,-1)
	};

	private static Vector3 [] vertex4Positions {get;} = {
	    new Vector3 ( 1, 1,-1),
	    new Vector3 ( 1,-1, 1),
	    new Vector3 (-1,-1,-1),
	    new Vector3 ( 1,-1, 1),
	    new Vector3 (-1,-1, 1),
	    new Vector3 ( 1,-1,-1)
	};
	
	private int lighting = 15;
	
	public Block () {
	}

	public bool isVisible () {
	    return true;
	}
	
	private bool isFaceVisible (int face) {
	    return true;
	}
	
	private Color4 getFaceColor(int face) {
	    float lightness = (lighting + 15 - (face != (int)Constants.Face.D ? face % 2 : 3)) / 30f;
	    //System.Console.WriteLine(lightness);
	    return new Color4(lightness, 0, 0, 1);
	}

	public Face getFace (int face) {
	    Vertex vertex1 = new Vertex(vertex1Positions [face], getFaceColor(face));
	    Vertex vertex2 = new Vertex(vertex2Positions [face], getFaceColor(face));
	    Vertex vertex3 = new Vertex(vertex3Positions [face], getFaceColor(face));
	    Vertex vertex4 = new Vertex(vertex4Positions [face], getFaceColor(face));
	    return new Face (vertex1, vertex2, vertex3, vertex4);
	}
	
    }
}
