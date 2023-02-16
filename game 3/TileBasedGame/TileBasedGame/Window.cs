using SDL2;
using System;

namespace TileBasedGame
{
    class Window
    {
        public int Height;
        public int Width;
        public SDL.SDL_DisplayMode DisplayMode;//real size
        public IntPtr Show;
        public IntPtr Renderer;
        public string ScreenMode = "Mini screen";

        //FPS
        private DateTime _timeNow;
        private DateTime _lastTime;
        private int _framesRendered;
        private int _fps;
        private Text _txt = new Text();

        public String ShowFPSRunning = "Yes";

        public uint LimitedFPS;
        public float DesiredDelta;
        public uint StartFPS;

        public Window(int SCREEN_HEIGHT, int SCREEN_WIDTH)
        {
            Height = SCREEN_HEIGHT;
            Width = SCREEN_WIDTH;
        }

        public void Setup()
        {
            // Create a new window given a title, size, and passes it a flag indicating it should be shown.
            Show = SDL.SDL_CreateWindow(
                        "Yalla Shoot Em Up",
                        SDL.SDL_WINDOWPOS_UNDEFINED,
                        SDL.SDL_WINDOWPOS_UNDEFINED,
                        Width,
                        Height,
                        SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (Show == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
            }

            Renderer = SDL.SDL_CreateRenderer(
                Program.Window.Show,
                -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (Renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
            }

            //SDL.SDL_GetRendererOutputSize(renderer, w, h);

            //fps text
            _txt.SetUp();
            _txt.LoadText(1);
            //limit fps
            LimitedFPS = 60;
            DesiredDelta = 1000 / LimitedFPS;

        }
        public void FullScreen()
        {
            SDL.SDL_SetWindowFullscreen(Show, 0x1u);
            ScreenMode = "Full screen";

        }
        public void MiniScreen()
        {
            SDL.SDL_SetWindowFullscreen(Show, 0);
            ScreenMode = "Mini screen";
        }

        public void ChangeScreenMode()
        {
            if (ScreenMode.Equals("Full screen"))
            {
                MiniScreen();
            }
            else
            {
                FullScreen();             
            }
        }
        public void FPSCalculate()
        {
            _timeNow = DateTime.Now;
            _framesRendered++;
            if ((_timeNow - _lastTime).TotalSeconds >= 1)
            {
                _fps = _framesRendered;
                _framesRendered = 0;
                _lastTime = _timeNow;
            }

            //FPS Render
            if (ShowFPSRunning.Equals("Yes"))
            {
                String text = "FPS: " + _fps.ToString();
                IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(_txt.Font, text, _txt.White);
                _txt.AddText(Program.Window.Renderer, surfaceMessage, Width/90, Height / 90, 75, 20);
            }
        }

        //limit Fps
        public void DeltaFPS()
        {
            uint delta = SDL.SDL_GetTicks() - StartFPS;
            if (delta < DesiredDelta)
            {
                uint deltaFPS = (uint) DesiredDelta - delta;
                if (Program.Game.Levels.MainMenu.Running) { deltaFPS *= 2; } //Because we have in main menu x2 Render (we want to fix this problem later)
                SDL.SDL_Delay(deltaFPS);
            }
        }

        public float CalculateFPS()
        {
            StartFPS = SDL.SDL_GetTicks();
            return DesiredDelta / 1000;
        }
        public void ChangeFPSLimit() {
            LimitedFPS = LimitedFPS * 2;
            if (LimitedFPS > 120) {
                LimitedFPS = 15;
            }
            DesiredDelta = 1000 / LimitedFPS;
        }

        public void ShowHideFPS() {
            if (ShowFPSRunning.Equals("Yes"))
            {
                ShowFPSRunning = "No";
            }
            else {
                ShowFPSRunning = "Yes";
            }
        }

        public void ChangeWindowSize() {

            if (Width == 1024 && Height == 768)
            {
                Width = 1920;
                Height = 1080;
                SDL.SDL_SetWindowSize(Show, Width, Height);
            }
            else {
                Width = 1024;
                Height = 768;
                SDL.SDL_SetWindowSize(Show, Width, Height);
            }
        }
    }
}


