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

	// Temp meshes for testing
	TerrainMesh mesh;
	TerrainMesh mesh2;
	TerrainMesh mesh3;
	
	public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base (gameWindowSettings, nativeWindowSettings) { }

        protected override void OnRenderFrame (FrameEventArgs e) {
	    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

	    mesh.drawMesh(terrainShader, camera);
	    mesh2.drawMesh(terrainShader, camera);
	    mesh3.drawMesh(terrainShader, camera);
	    
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
		if (KeyboardState.IsKeyDown(Keys.W)) camera.Position += camera.Front * .1f;
		if (KeyboardState.IsKeyDown(Keys.S)) camera.Position -= camera.Front * .1f;
		if (KeyboardState.IsKeyDown(Keys.A)) camera.Position -= camera.Right * .1f;
		if (KeyboardState.IsKeyDown(Keys.D)) camera.Position += camera.Right * .1f;
		if (KeyboardState.IsKeyDown(Keys.Space)) camera.Position += camera.Up * .1f;
		if (KeyboardState.IsKeyDown(Keys.LeftShift)) camera.Position -= camera.Up * .1f;

		camera.Pitch -= MouseState.Delta.Y * 0.2f;
		camera.Yaw += MouseState.Delta.X * 0.2f;
	    }
	    
            base.OnUpdateFrame(e);
        }

        protected override void OnLoad () {
	    GL.ClearColor(0.0f, 0.0f, 1.0f, 1.0f);
	    GL.Enable(EnableCap.DepthTest);

	    System.Console.WriteLine(IsMultiThreaded);
	    
	    CursorGrabbed = true;
	    	    
	    terrainShader = new Shader("terrain.vert", "terrain.frag");

	    camera = new Camera(new Vector3(1.5f, 1.5f, 1.5f));

	    mesh = new TerrainMesh(Matrix4.Identity);
	    mesh2 = new TerrainMesh(Matrix4.CreateScale(2.0f) * Matrix4.CreateTranslation(-1.3f, -2.5f, 0));
	    mesh3 = new TerrainMesh(Matrix4.CreateFromAxisAngle(new Vector3(.5f, 0, .5f), 1.5f) * Matrix4.CreateTranslation(-4, 2, 3));
	    
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
