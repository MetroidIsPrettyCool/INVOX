using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

using System;

namespace INVOX {
    class Entity {
	protected Vector3 [] hitbox = new Vector3 [2];

	protected Vector3 position;

	protected Vector3 headOffset; // The offset of the head relative to the body (mostly for camera stuff)
	
        protected Vector3 frontHead, rightHead, upHead;
        protected Vector3 frontBody, rightBody, upBody;

	protected float pitch, yaw;
	
	// Pretty much usable for anything
	private int state;
	
	public Entity () {
	    hitbox [0] = Vector3.Zero;
	    hitbox [1] = Vector3.Zero;

	    position = Vector3.Zero;

	    pitch = 0;
	    yaw = -MathHelper.PiOver2;
	    
	    frontHead = -Vector3.UnitZ;
	    rightHead =  Vector3.UnitX;
	    upHead    =  Vector3.UnitY;

	    frontBody = -Vector3.UnitZ;
	    rightBody =  Vector3.UnitX;
	    upBody    =  Vector3.UnitY;
	}

	public Entity (Vector3 position) {
	    hitbox [0] = Vector3.Zero;
	    hitbox [1] = Vector3.Zero;

	    this.position = position;

	    pitch = 0;
	    yaw = -MathHelper.PiOver2;
	    
	    frontHead = -Vector3.UnitZ;
	    rightHead =  Vector3.UnitX;
	    upHead    =  Vector3.UnitY;

	    frontBody = -Vector3.UnitZ;
	    rightBody =  Vector3.UnitX;
	    upBody    =  Vector3.UnitY;
	}

	public void updateCamera (Camera camera) {
	    camera.updateCamera(position + headOffset, pitch, yaw);
	}
	
	public void updateEntity (Level level, Window window, FrameEventArgs e) {
	}

        protected void moveToPoint (Vector3 position, Level level, Window window) {
	    if (!doesPositionCollide(position.X, this.position.Y, this.position.Z, level)) this.position.X = position.X;
	    if (!doesPositionCollide(this.position.X, position.Y, this.position.Z, level)) this.position.Y = position.Y;
	    if (!doesPositionCollide(this.position.X, this.position.Y, position.Z, level)) this.position.Z = position.Z;
	    updateVectors();
	}

	protected bool doesPositionCollide (float posX, float posY, float posZ, Level level) {
	    // Is this needlessly complex? Yeah. Could I stand to rewrite it? Yeah. Will I? Not for a long while, at least.
	    for (int x = (int)(hitbox[0].X + posX); x <= (int)(hitbox[1].X + posX); x++) {
		for (int y = (int)(hitbox[0].Y + posY); y <= (int)(hitbox[1].Y + posY); y++) {
		    for (int z = (int)(hitbox[0].Z + posZ); z <= (int)(hitbox[1].Z + posZ); z++) {
			if (x >= Constants.levelSizeX || x < 0 || y >= Constants.levelSizeY || y < 0 || z >= Constants.levelSizeZ || z < 0) return true;
			if (level.getBlockAt(x,y,z).isntAir && level.blockTypes[level.getBlockAt(x,y,z).blockTypeIndex].isSolid) return true;
		    }
		}
	    }

	    for (float x = (hitbox[0].X + posX); x <= (hitbox[1].X + posX); x++) {
		for (float y = (hitbox[0].Y + posY); y <= (hitbox[1].Y + posY); y++) {
		    for (float z = (hitbox[0].Z + posZ); z <= (hitbox[1].Z + posZ); z++) {
			if (x >= Constants.levelSizeX || x <= 0 || y >= Constants.levelSizeY || y <= 0 || z >= Constants.levelSizeZ || z <= 0) return true;
		    }
		}
	    }
	    
	    return false;
	}

	protected void updateHitbox () {
	    
	}
	
	protected void updateVectors () {
	    frontHead.X = (float)Math.Cos(pitch) * (float)Math.Cos(yaw);
            frontHead.Y = (float)Math.Sin(pitch);
            frontHead.Z = (float)Math.Cos(pitch) * (float)Math.Sin(yaw);

            frontHead = Vector3.Normalize(frontHead);

            rightHead = Vector3.Normalize(Vector3.Cross(frontHead, Vector3.UnitY));
            upHead = Vector3.Normalize(Vector3.Cross(rightHead, frontHead));

	    frontBody.X = (float)Math.Cos(yaw);
            frontBody.Z = (float)Math.Sin(yaw);

            frontBody = Vector3.Normalize(frontBody);

            rightBody = Vector3.Normalize(Vector3.Cross(frontBody, Vector3.UnitY));
            upBody = Vector3.Normalize(Vector3.Cross(rightBody, frontBody));
	}
	
    }
}
