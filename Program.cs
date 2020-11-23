using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace INVOX {
    class Program {
        static void Main(string[] args) {
	    GameWindowSettings gameWindowSettings = new GameWindowSettings();
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings();

            nativeWindowSettings.Size = new Vector2i(1280, 720);
            nativeWindowSettings.Title = "INVOX";

	    gameWindowSettings.UpdateFrequency = 20;

            using (Window window = new Window(gameWindowSettings, nativeWindowSettings)) {
                window.Run();
            }
        }
    }
}
