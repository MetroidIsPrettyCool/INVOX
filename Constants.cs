using OpenTK.Mathematics;

namespace INVOX {
    class Constants {
	public enum Face { // U / D are +Y and -Y, obv. N = +Z, S = -Z. E = +X, W = -X.
	    U,
	    D,
	    E,
	    W,
	    N,
	    S
	}
	public const int levelSizeX = 1024;
	public const int levelSizeY = 2048;
	public const int levelSizeZ = 1024;
	public const int terrainMeshSize = 64;
	
	public static readonly float [] uFaceVertices = {
	    1, 1, 1,
	    0, 1, 0,
	    0, 1, 1,
	    1, 1, 0
	};

	public static readonly float [] dFaceVertices = {
	    1, 0, 0,
	    0, 0, 1,
	    0, 0, 0,
	    1, 0, 1
	};

	public static readonly float [] eFaceVertices = {
	    1, 1, 1,
	    1, 0, 0,
	    1, 1, 0,
	    1, 0, 1
	};

	public static readonly float [] wFaceVertices = {
	    0, 1, 0,
	    0, 0, 1,
	    0, 1, 1,
	    0, 0, 0
	};

	public static readonly float [] nFaceVertices = {
	    0, 1, 1,
	    1, 0, 1,
	    1, 1, 1,
	    0, 0, 1
	};

	public static readonly float [] sFaceVertices = {
	    1, 1, 0,
	    0, 0, 0,
	    0, 1, 0,
	    1, 0, 0
	};

	public static readonly float [] [] faceVertices = {
	    uFaceVertices,
	    dFaceVertices,
	    eFaceVertices,
	    wFaceVertices,
	    nFaceVertices,
	    sFaceVertices
	};
    }
}
