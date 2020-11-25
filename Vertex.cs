using OpenTK.Mathematics;
using System;

namespace INVOX {
    class Vertex {
	public readonly Vector3 position;
	public readonly Color4 color;

	public Vertex (Vector3 position, Color4 color) {
	    this.position = position;
	    this.color = color;
	}

	public override bool Equals(Object obj) {
	    if ((obj == null) || ! this.GetType().Equals(obj.GetType())) {
		return false;
	    }
	    else {
		Vertex v = (Vertex)obj;
		return v.position.Equals(this.position) && v.color.Equals(this.color);
	    }
	}
	public override int GetHashCode () {
	    return (position.GetHashCode()*color.GetHashCode())/(position.GetHashCode()+color.GetHashCode());
	}
	
    }
}
