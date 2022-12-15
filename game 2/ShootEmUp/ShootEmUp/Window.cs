using SDL2;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ShootEmUp
{
    class Window
    {
        public int heigh;
        public int width;
        public IntPtr show;
        public IntPtr renderer;
        public string screenMode = "Mini screen";

        //FPS
        DateTime timeNow;
        DateTime _lastTime;
        int _framesRendered;
        int _fps;
        Text txt = new Text();
        bool showFPSRunning = true;

        public uint limitedFPS;
        public uint desiredDelta;
        public uint startFPS;

        public Window(int SCREEN_HEIGH, int SCREEN_WIDTH)
        {
            heigh = SCREEN_HEIGH;
            width = SCREEN_WIDTH;
        }

        public void setup()
        {
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

            //fps text
            txt.setUp();
            txt.loadText(1);
            //limit fps
            limitedFPS = 30;
            desiredDelta = 1000 / limitedFPS;

        }
        public void fullScreen()
        {
            SDL.SDL_SetWindowFullscreen(show, 0x1u);
            screenMode = "Full screen";
        }
        public void miniScreen()
        {
            SDL.SDL_SetWindowFullscreen(show, 0);
            screenMode = "Mini screen";
        }

        public void changeScreenMode()
        {
            if (screenMode.Equals("Full screen"))
            {
                miniScreen();
            }
            else
            {
                fullScreen();
            }
        }
        public void fpsCalculate()
        {
            timeNow = DateTime.Now;
            _framesRendered++;
            if ((timeNow - _lastTime).TotalSeconds >= 1)
            {
                _fps = _framesRendered;
                _framesRendered = 0;
                _lastTime = timeNow;
            }

            //FPS Render
            if (showFPSRunning)
            {
                String text = "FPS: " + _fps.ToString();
                IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, txt.White);
                txt.addText(Program.window.renderer, surfaceMessage, 30, 10, 75, 20);
            }

        }

        //limit Fps
        public void deltaFPS()
        {
            uint delta = SDL.SDL_GetTicks() - startFPS;
            if (delta < desiredDelta)
            {
                uint deltaFPS = desiredDelta - delta;
                SDL.SDL_Delay(deltaFPS);
            }
        }

        public void calculateFPS()
        {
            startFPS = SDL.SDL_GetTicks();
        }

    }
}


