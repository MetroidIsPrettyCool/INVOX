using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using OpenTK.Windowing.GraphicsLibraryFramework;

using OpenTK.Graphics.OpenGL4;

using OpenTK.Mathematics;

namespace INVOX {
    public class Window : GameWindow {

	public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base (gameWindowSettings, nativeWindowSettings) { }

        protected override void OnRenderFrame (FrameEventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();

            base.OnRenderFrame(e);
        }

        // This function runs on every update frame.
        protected override void OnUpdateFrame (FrameEventArgs e) {

            if (this.KeyboardState.IsKeyDown(Keys.Escape)) {
                this.Close();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnLoad () {
            GL.ClearColor(0.0f, 0.0f, 1.0f, 1.0f);
	    
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
