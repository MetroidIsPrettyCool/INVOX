using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

using System;

namespace INVOX {
    class Player : Entity {
	public Player () {
	    hitbox [0] = Vector3.Zero;
	    hitbox [1] = new Vector3 (.5f,1.9f,.5f);

	    position = Vector3.Zero;

	    pitch = 0;
	    yaw = -MathHelper.PiOver2;
	    
	    frontHead = -Vector3.UnitZ;
	    rightHead =  Vector3.UnitX;
	    upHead    =  Vector3.UnitY;

	    frontBody = -Vector3.UnitZ;
	    rightBody =  Vector3.UnitX;
	    upBody    =  Vector3.UnitY;

	    headOffset = new Vector3(.5f, 1.8f, .5f);
	}

	public Player (Vector3 position) {
	    hitbox [0] = Vector3.Zero;
	    hitbox [1] = new Vector3 (.6f,1.8f,.6f);

	    this.position = position;

	    pitch = 0;
	    yaw = -MathHelper.PiOver2;
	    
	    frontHead = -Vector3.UnitZ;
	    rightHead =  Vector3.UnitX;
	    upHead    =  Vector3.UnitY;

	    frontBody = -Vector3.UnitZ;
	    rightBody =  Vector3.UnitX;
	    upBody    =  Vector3.UnitY;

	    headOffset = new Vector3(.3f, 1.63f, .3f);
	}

	new public void updateEntity (Level level, Window window, FrameEventArgs e) {

	    Vector3 newPosition = position;
	    
	    if (window.KeyboardState.IsKeyDown(Keys.W))         newPosition += frontBody * (float)e.Time * 5.5f;
	    if (window.KeyboardState.IsKeyDown(Keys.S))         newPosition -= frontBody * (float)e.Time * 5.5f;
	    if (window.KeyboardState.IsKeyDown(Keys.A))         newPosition -= rightBody * (float)e.Time * 5.5f;
	    if (window.KeyboardState.IsKeyDown(Keys.D))         newPosition += rightBody * (float)e.Time * 5.5f;
	    if (window.KeyboardState.IsKeyDown(Keys.Space))     newPosition += upBody * (float)e.Time * 5.5f;
	    if (window.KeyboardState.IsKeyDown(Keys.LeftShift)) newPosition -= upBody * (float)e.Time * 5.5f;

	    pitch = (float)Math.Clamp(pitch - window.MouseState.Delta.Y * 0.005f, -MathHelper.PiOver2 + 0.01, MathHelper.PiOver2 - 0.01);
	    yaw = (yaw + window.MouseState.Delta.X * 0.005f) % MathHelper.TwoPi;

	    moveToPoint(newPosition, level, window);
	}
	
    }
}
