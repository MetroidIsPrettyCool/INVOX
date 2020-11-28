using OpenTK.Mathematics;
using System;

namespace INVOX {
    class Camera {
	private Vector3 position;
	
	private float pitch, yaw;
		
	private Vector3 front = -Vector3.UnitZ;
	private Vector3 right =  Vector3.UnitX;
	private Vector3 up    =  Vector3.UnitY;

	// Replace with an index later
	private Entity watchedEntity;

	public Camera (Entity e) {
	    e.updateCamera(this);
	    watchedEntity = e;
	}

	public void updateCamera () {
	    watchedEntity.updateCamera(this);
	    updateVectors();
	}
	
	public void updateCamera (Vector3 position, float pitch, float yaw) {
	    this.position = position;
	    this.pitch = pitch;
	    this.yaw = yaw;
	}

	public Matrix4 getViewMatrix () {
	    return Matrix4.LookAt(position, position + front, up);
	}
        
	private void updateVectors () {
            front.X = (float)Math.Cos(pitch) * (float)Math.Cos(yaw);
            front.Y = (float)Math.Sin(pitch);
            front.Z = (float)Math.Cos(pitch) * (float)Math.Sin(yaw);

            front = Vector3.Normalize(front);

            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }
	
    }
}
