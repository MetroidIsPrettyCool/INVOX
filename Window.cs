using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using OpenTK.Windowing.GraphicsLibraryFramework;

using OpenTK.Graphics.OpenGL4;

using OpenTK.Mathematics;

namespace INVOX {
    public class Window : GameWindow {

	private Shader terrainShader;

	// Temp camera for testing
	Camera camera;

	// Temp level for testing
	Level testLevel;

	// Temp for now, will eventually move into a seperate thread / class
	int x = 0, y = 0, z = 0;
	bool genMeshes = true;
	
	public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base (gameWindowSettings, nativeWindowSettings) { }

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
	    
	    testLevel.drawLevel(terrainShader, camera);
	    
            SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame (FrameEventArgs e) {

	    if (KeyboardState.IsKeyPressed(Keys.GraveAccent)) {
		CursorGrabbed = !CursorGrabbed;
		if (!CursorGrabbed) CursorVisible = true;
	    }
	    
            if (KeyboardState.IsKeyDown(Keys.Q)) Close();
	    
	    // Temp camera controls
	    if (CursorGrabbed) {
		if (KeyboardState.IsKeyDown(Keys.W)) camera.Position += camera.Front * (float)e.Time * 5.5f;
		if (KeyboardState.IsKeyDown(Keys.S)) camera.Position -= camera.Front * (float)e.Time * 5.5f;
		if (KeyboardState.IsKeyDown(Keys.A)) camera.Position -= camera.Right * (float)e.Time * 5.5f;
		if (KeyboardState.IsKeyDown(Keys.D)) camera.Position += camera.Right * (float)e.Time * 5.5f;
		if (KeyboardState.IsKeyDown(Keys.Space)) camera.Position += camera.Up * (float)e.Time * 5.5f;
		if (KeyboardState.IsKeyDown(Keys.LeftShift)) camera.Position -= camera.Up * (float)e.Time * 5.5f;

		camera.Pitch -= MouseState.Delta.Y * 0.3f;
		camera.Yaw += MouseState.Delta.X * 0.3f;
	    }
	    
            base.OnUpdateFrame(e);
        }

        protected override void OnLoad () {
	    GL.ClearColor(0.0f, 0.0f, 1.0f, 1.0f);
	    GL.Enable(EnableCap.DepthTest);
	    GL.Enable(EnableCap.CullFace);
	    
	    CursorGrabbed = true;
	    	    
	    terrainShader = new Shader("terrain.vert", "terrain.frag");

	    camera = new Camera(new Vector3(1.5f, 1.5f, 1.5f));

	    testLevel = new Level();
	    
	    base.OnLoad();
        }

        protected override void OnUnload () {
            base.OnUnload();
        }

        protected override void OnResize (ResizeEventArgs e) {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
	    camera.Aspect = (float)Size.X / Size.Y;
        }

    }
}
