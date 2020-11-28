using OpenTK.Mathematics;
using System;

namespace INVOX {
    class Camera {

	// Plan to move this to the window class eventually
	private static float near = 0.01f;
	private static float far = 500f;

	private Vector3 position;
	
	private float pitch, yaw;
	private float fov, aspect;
		
	private Vector3 front = -Vector3.UnitZ;
	private Vector3 right =  Vector3.UnitX;
	private Vector3 up    =  Vector3.UnitY;

	// Replace with an index later
	private Entity watchedEntity;
	
	public Camera (Vector3 position) {
	    this.position = position;
	    this.pitch = 0;
	    this.yaw = MathHelper.PiOver2;
	    this.fov = MathHelper.DegreesToRadians(70);
	    this.aspect = 16f / 9;
	}

	public Camera (Vector3 position, float pitch, float yaw) {
	    this.position = position;
	    this.pitch = pitch;
	    this.yaw = yaw;
	    this.fov = MathHelper.DegreesToRadians(70);
	    this.aspect = 16f / 9;
	}

	public Camera (Vector3 position, float pitch, float yaw, float fov, float aspect) {
	    this.position = position;
	    this.pitch = pitch;
	    this.yaw = yaw;
	    this.fov = fov;
	    this.aspect = aspect;
	}

	public Camera (Entity e) {
	    e.updateCamera(this);
	    watchedEntity = e;
	    this.fov = MathHelper.DegreesToRadians(70);
	    this.aspect = 16f / 9;
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
	
	public Matrix4 getProjectionMatrix () {
	    return Matrix4.CreatePerspectiveFieldOfView(fov, aspect, near, far);
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
