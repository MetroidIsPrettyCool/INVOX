using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using OpenTK.Windowing.GraphicsLibraryFramework;

using OpenTK.Graphics.OpenGL4;

using OpenTK.Mathematics;

namespace INVOX {
    class Window : GameWindow {

	private Shader terrainShader;

	public Camera camera;

	public float aspect, fov, near, far;
	
	Player testPlayer;

	Level testLevel;

	// Temp for now, will eventually move into a seperate thread / class
	int x = 0, y = 0, z = 0;
	bool genMeshes = true;
	
	public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base (gameWindowSettings, nativeWindowSettings) {
	    aspect = 16f/ 9;
	    fov = MathHelper.DegreesToRadians(70);
	    near = 0.01f;
	    far = 500f;
	}

        protected override void OnRenderFrame (FrameEventArgs e) {
	    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

	    // The game will attempt to generate four meshes per frame
	    
	    if (genMeshes) for (int i = 0; i != 4; i++) {
		if (genMeshes) {
		    testLevel.generateMeshAt(x, y, z);
		    x++;
		    if (x == Constants.levelSizeX / Constants.terrainMeshSize) {
			y++;
			x = 0;
		    }
		    if (y == Constants.levelSizeY / Constants.terrainMeshSize) {
			z++;
			y = 0;
		    }
		    if (z == Constants.levelSizeZ / Constants.terrainMeshSize) {
			genMeshes = false;
		    }
		}
	    }
	    
	    testLevel.drawLevel(terrainShader, camera, this);
	    
            SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame (FrameEventArgs e) {
	    
	    if (KeyboardState.IsKeyPressed(Keys.GraveAccent)) {
		CursorGrabbed = !CursorGrabbed;
		if (!CursorGrabbed) CursorVisible = true;
	    }
	    
            if (KeyboardState.IsKeyDown(Keys.Q)) Close();
	    
	    if (CursorGrabbed) {
		testPlayer.updateEntity(testLevel, this, e);
	    }

	    camera.updateCamera();
	    
            base.OnUpdateFrame(e);
        }

        protected override void OnLoad () {
	    GL.ClearColor(0.0f, 0.0f, 1.0f, 1.0f);
	    GL.Enable(EnableCap.DepthTest);
	    GL.Enable(EnableCap.CullFace);
	    
	    CursorGrabbed = true;
	    	    
	    terrainShader = new Shader("terrain.vert", "terrain.frag");

	    testLevel = new Level();

	    testPlayer = new Player(new Vector3(5, 130, 5));

	    camera = new Camera(testPlayer);
	    
	    base.OnLoad();
        }

        protected override void OnUnload () {
            base.OnUnload();
        }

	public Matrix4 getProjectionMatrix () {
	    return Matrix4.CreatePerspectiveFieldOfView(fov, aspect, near, far);
	}

        protected override void OnResize (ResizeEventArgs e) {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
	    aspect = (float)Size.X / Size.Y;
        }

    }
}
