using OpenTK.Mathematics;
using System;

namespace INVOX {
    class Camera {

	private static float _near = 0.01f;
	private static float _far = 100f;
	
	public Vector3 _position;

	public Vector3 Position {
	    get => _position;
	    set {
		_position = value;
	    }
	}
	
	private float _pitch, _yaw, _fov, _aspect;
	
	public float Pitch {
            get => MathHelper.RadiansToDegrees(_pitch);
            set {
                _pitch = MathHelper.DegreesToRadians(MathHelper.Clamp(value, -90f, 90f));
		_UpdateVectors();
            }
        }
	public float Yaw {
            get => MathHelper.RadiansToDegrees(_yaw);
            set {
                _yaw = MathHelper.DegreesToRadians(value % 360);
                _UpdateVectors();
            }
        }
	
	public float Fov {
            get => MathHelper.RadiansToDegrees(_fov);
            set {
                _fov = MathHelper.DegreesToRadians(MathHelper.Clamp(value, 0f, 120f));
            }
        }

	public float Aspect {
	    get => _aspect;
	    set {
		_aspect = value;
	    }
	}
	
	private Vector3 _front = -Vector3.UnitZ;
	private Vector3 _right =  Vector3.UnitX;
	private Vector3 _up    =  Vector3.UnitY;

	// Temporary, entities will keep track of most this 
	public Vector3 Front {get => _front;}
	public Vector3 Right {get => _right;}
	public Vector3 Up {get => _up;}
	
	public Camera (Vector3 position) {
	    Position = position;
	    Pitch = 0;
	    Yaw = -90;
	    Fov = 70;
	    Aspect = 16f / 9;
	}

	public Camera (Vector3 position, float pitch, float yaw) {
	    Position = position;
	    Pitch = pitch;
	    Yaw = yaw;
	    Fov = 70;
	    Aspect = 16f / 9;
	}

	public Camera (Vector3 position, float pitch, float yaw, float fov, float aspect) {
	    Position = position;
	    Pitch = pitch;
	    Yaw = yaw;
	    Fov = fov;
	    Aspect = aspect;
	}

	public Matrix4 getViewMatrix () {
	    return Matrix4.LookAt(_position, _position + _front, _up);
	}
	
	public Matrix4 getProjectionMatrix () {
	    return Matrix4.CreatePerspectiveFieldOfView(_fov, _aspect, _near, _far);
	}

	private void _UpdateVectors () {
            _front.X = (float)Math.Cos(_pitch) * (float)Math.Cos(_yaw);
            _front.Y = (float)Math.Sin(_pitch);
            _front.Z = (float)Math.Cos(_pitch) * (float)Math.Sin(_yaw);

            _front = Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }
	
    }
}
