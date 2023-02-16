using SDL2;
using System;

namespace TileBasedGame
{
    class InfoBox
    {
        public Text Txt = new Text();
        public bool Running = true;
        public int AnimText = 0;

        public InfoBox()
        {
            Txt.SetUp();
            Txt.LoadText(1);
        }
        public void PrintTxt(string txt, int y, int size)
        {
            IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(Txt.Font, txt, Txt.Black);
            Txt.AddText(Program.Window.Renderer, surfaceMessage, Program.Window.Width / 2 - txt.Length * 10, y, txt.Length * 20, size);
        }

        public void RenderBox()
        {

            SDL.SDL_RenderPresent(Program.Window.Renderer);
            // draw the box rectangle
            SDL.SDL_Rect borderRect = new SDL.SDL_Rect
            {
                x = Program.Window.Width * 23 / 100,
                y = Program.Window.Height * 28 / 100,
                w = Program.Window.Width - Program.Window.Width * 46 / 100,
                h = Program.Window.Height * 34 / 100,
            };
            SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 0, 0, 0, 0);

            SDL.SDL_RenderFillRect(Program.Window.Renderer, ref borderRect);
            // draw the box rectangle
            SDL.SDL_Rect backgroundRect = new SDL.SDL_Rect
            {
                x = Program.Window.Width * 25 / 100,
                y = Program.Window.Height * 30 / 100,
                w = Program.Window.Width - Program.Window.Width * 50 / 100,
                h = Program.Window.Height * 30 / 100,
            };
            SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 255, 255, 255, 255);
            SDL.SDL_RenderFillRect(Program.Window.Renderer, ref backgroundRect);           
        }

        public bool Control()
        {
            //let player stop moving
            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.StopMoving, Program.Game.Player));
            //Key
            SDL.SDL_Event e;
            // Handle events on queue
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                {

                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_RETURN: Running = false; return false;
                    }
                }
            }
            return true;
        }

        public void PrintAnimText(string text) {
            if (AnimText <= 15)
            {
                PrintTxt(text, (Program.Window.Height * 30 / 100) + Program.Window.Height * 24 / 100, 30);
                AnimText++;
            }
            else if (AnimText < 30)
                AnimText++;
            else
                AnimText = 0;
        }
    }
}
