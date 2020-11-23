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

	// Temp varibles, only here to get the camera working
	int vao, posvbo, colorvbo;
	
	public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base (gameWindowSettings, nativeWindowSettings) { }

        protected override void OnRenderFrame (FrameEventArgs e) {
	    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

	    terrainShader.Use();
	    Shader.setMat4(terrainShader.Handle, camera.getViewMatrix() * camera.getProjectionMatrix(), "mvp");

	    GL.DrawArrays(PrimitiveType.Triangles, 0, 12 * 3);
	    
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
	    if (KeyboardState.IsKeyDown(Keys.W)) camera.Position += camera.Front * .1f;
	    if (KeyboardState.IsKeyDown(Keys.S)) camera.Position -= camera.Front * .1f;
	    if (KeyboardState.IsKeyDown(Keys.A)) camera.Position -= camera.Right * .1f;
	    if (KeyboardState.IsKeyDown(Keys.D)) camera.Position += camera.Right * .1f;
	    if (KeyboardState.IsKeyDown(Keys.Space)) camera.Position += camera.Up * .1f;
	    if (KeyboardState.IsKeyDown(Keys.LeftShift)) camera.Position -= camera.Up * .1f;
	    
            base.OnUpdateFrame(e);
        }

        protected override void OnLoad () {
	    GL.ClearColor(0.0f, 0.0f, 1.0f, 1.0f);
	    GL.Enable(EnableCap.DepthTest);

	    System.Console.WriteLine(IsMultiThreaded);
	    
	    CursorGrabbed = true;
	    	    
	    terrainShader = new Shader("terrain.vert", "terrain.frag");

	    float [] vertices = { 
		-1.0f,-1.0f,-1.0f,
		-1.0f,-1.0f, 1.0f,
		-1.0f, 1.0f, 1.0f,
		1.0f, 1.0f,-1.0f,
		-1.0f,-1.0f,-1.0f,
		-1.0f, 1.0f,-1.0f,
		1.0f,-1.0f, 1.0f,
		-1.0f,-1.0f,-1.0f,
		1.0f,-1.0f,-1.0f,
		1.0f, 1.0f,-1.0f,
		1.0f,-1.0f,-1.0f,
		-1.0f,-1.0f,-1.0f,
		-1.0f,-1.0f,-1.0f,
		-1.0f, 1.0f, 1.0f,
		-1.0f, 1.0f,-1.0f,
		1.0f,-1.0f, 1.0f,
		-1.0f,-1.0f, 1.0f,
		-1.0f,-1.0f,-1.0f,
		-1.0f, 1.0f, 1.0f,
		-1.0f,-1.0f, 1.0f,
		1.0f,-1.0f, 1.0f,
		1.0f, 1.0f, 1.0f,
		1.0f,-1.0f,-1.0f,
		1.0f, 1.0f,-1.0f,
		1.0f,-1.0f,-1.0f,
		1.0f, 1.0f, 1.0f,
		1.0f,-1.0f, 1.0f,
		1.0f, 1.0f, 1.0f,
		1.0f, 1.0f,-1.0f,
		-1.0f, 1.0f,-1.0f,
		1.0f, 1.0f, 1.0f,
		-1.0f, 1.0f,-1.0f,
		-1.0f, 1.0f, 1.0f,
		1.0f, 1.0f, 1.0f,
		-1.0f, 1.0f, 1.0f,
		1.0f,-1.0f, 1.0f
	    };

	    float [] colors = { 
		0.583f,  0.771f,  0.014f,
		0.609f,  0.115f,  0.436f,
		0.327f,  0.483f,  0.844f,
		0.822f,  0.569f,  0.201f,
		0.435f,  0.602f,  0.223f,
		0.310f,  0.747f,  0.185f,
		0.597f,  0.770f,  0.761f,
		0.559f,  0.436f,  0.730f,
		0.359f,  0.583f,  0.152f,
		0.483f,  0.596f,  0.789f,
		0.559f,  0.861f,  0.639f,
		0.195f,  0.548f,  0.859f,
		0.014f,  0.184f,  0.576f,
		0.771f,  0.328f,  0.970f,
		0.406f,  0.615f,  0.116f,
		0.676f,  0.977f,  0.133f,
		0.971f,  0.572f,  0.833f,
		0.140f,  0.616f,  0.489f,
		0.997f,  0.513f,  0.064f,
		0.945f,  0.719f,  0.592f,
		0.543f,  0.021f,  0.978f,
		0.279f,  0.317f,  0.505f,
		0.167f,  0.620f,  0.077f,
		0.347f,  0.857f,  0.137f,
		0.055f,  0.953f,  0.042f,
		0.714f,  0.505f,  0.345f,
		0.783f,  0.290f,  0.734f,
		0.722f,  0.645f,  0.174f,
		0.302f,  0.455f,  0.848f,
		0.225f,  0.587f,  0.040f,
		0.517f,  0.713f,  0.338f,
		0.053f,  0.959f,  0.120f,
		0.393f,  0.621f,  0.362f,
		0.673f,  0.211f,  0.457f,
		0.820f,  0.883f,  0.371f,
		0.982f,  0.099f,  0.879f
	    };

	    
	    vao = GL.GenVertexArray();
	    GL.BindVertexArray(vao);

	    posvbo = GL.GenBuffer();
	    GL.BindBuffer(BufferTarget.ArrayBuffer, posvbo);
	    GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
	    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
	    GL.EnableVertexAttribArray(0);

	    colorvbo = GL.GenBuffer();
	    GL.BindBuffer(BufferTarget.ArrayBuffer, colorvbo);
	    GL.BufferData(BufferTarget.ArrayBuffer, colors.Length * sizeof(float), colors, BufferUsageHint.StaticDraw);
	    GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
	    GL.EnableVertexAttribArray(1);
	    
	    camera = new Camera(new Vector3(1.5f, 1.5f, 1.5f));

	    base.OnLoad();
        }

        protected override void OnUnload () {
            base.OnUnload();
        }

        protected override void OnResize (ResizeEventArgs e) {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

    }
}
