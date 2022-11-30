using SDL2;
using System;
using System.IO;
using System.Text;

namespace ShootEmUp
{
    class Window
    {
        public int heigh;
        public int width;
        public IntPtr show;
        public IntPtr renderer;
        public string screenMode = "Mini screen";
        public Window(int SCREEN_HEIGH, int SCREEN_WIDTH)
        {
            heigh = SCREEN_HEIGH;
            width = SCREEN_WIDTH;
        }

        public void setup() {
            // Create a new window given a title, size, and passes it a flag indicating it should be shown.
            show = SDL.SDL_CreateWindow(
                        "Yalla Pong",
                        SDL.SDL_WINDOWPOS_UNDEFINED,
                        SDL.SDL_WINDOWPOS_UNDEFINED,
                        width,
                        heigh,
                        SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (show == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
            }

            renderer = SDL.SDL_CreateRenderer(
                Program.window.show,
                -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
            }

        }
        public void fullScreen() {
            SDL.SDL_SetWindowFullscreen(show, 0x1u);
            screenMode = "Full screen";
        }
        public void miniScreen()
        {
            SDL.SDL_SetWindowFullscreen(show, 0);
            screenMode = "Mini screen";
        }

        public void changeScreenMode() {
            if (screenMode.Equals("Full screen")) {
                miniScreen();
            }
            else {
                fullScreen();
            }
        }
    }
}


