using SDL2;
using System;
using System.IO;
using System.Text;

namespace Pong
{
    class Window
    {
        public int heigh;
        public int width;
        public IntPtr show;

        public Window(int SCREEN_HEIGH, int SCREEN_WIDTH)
        {
            heigh = SCREEN_HEIGH;
            width = SCREEN_WIDTH;
        }

        public void setup() {
            // Create a new window given a title, size, and passes it a flag indicating it should be shown.
            show = SDL.SDL_CreateWindow(
                        "SDL .NET 6 Tutorial",
                        SDL.SDL_WINDOWPOS_UNDEFINED,
                        SDL.SDL_WINDOWPOS_UNDEFINED,
                        width,
                        heigh,
                        SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (show == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
            }
        }
    }
}


