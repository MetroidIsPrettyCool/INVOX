using OpenTK.Mathematics;

namespace INVOX {
    class Face {
	public readonly Vertex vertex1, vertex2, vertex3, vertex4;

	public Face (Vertex vertex1, Vertex vertex2, Vertex vertex3, Vertex vertex4) {
	    this.vertex1 = vertex1;
	    this.vertex2 = vertex2;
	    this.vertex3 = vertex3;
	    this.vertex4 = vertex4;
	}
    }
}
