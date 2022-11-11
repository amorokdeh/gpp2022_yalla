using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Text
    {
        // font
        public IntPtr Font = IntPtr.Zero;
        public IntPtr Message = IntPtr.Zero;

        public SDL.SDL_Color Gray;
        public SDL.SDL_Color LightGray;
        public SDL.SDL_Color White;
        public SDL.SDL_Color Red;
        public Text() { 
        
        }

        public void setUp() {
            // Initialize SDL_ttf
            if (SDL_ttf.TTF_Init() == -1)
            {
                Console.WriteLine("SDL_ttf could not initialize! SDL_ttf Error: {0}", SDL.SDL_GetError());
            }
            Gray = new SDL.SDL_Color() { r = 150, g = 150, b = 150 };
            White = new SDL.SDL_Color() { r = 255, g = 255, b = 255 };
            LightGray = new SDL.SDL_Color() { r = 235, g = 235, b = 240 };
            Red = new SDL.SDL_Color() { r = 255, g = 0, b = 0 };
        }

        public void loadText(int num) {
            switch (num)
            {
                case 1: Font = SDL_ttf.TTF_OpenFont("font/automati.ttf", 16); break;
                case 2: Font = SDL_ttf.TTF_OpenFont("font/lazy.ttf", 40); break;
                case 3: Font = SDL_ttf.TTF_OpenFont("font/aerial.ttf", 40); break;
                case 4: Font = SDL_ttf.TTF_OpenFont("font/Adequate-ExtraLight.ttf", 40); break;
            }
        }

        public void addText(IntPtr renderer, IntPtr surfaceMessage, int x, int y, int w, int h) {
            Message = SDL.SDL_CreateTextureFromSurface(renderer, surfaceMessage);

            SDL.SDL_Rect Message_rect;
            Message_rect.x = x;
            Message_rect.y = y;
            Message_rect.w = w;
            Message_rect.h = h;
            SDL.SDL_RenderCopy(renderer, Message, IntPtr.Zero, ref Message_rect);
        }

    }
}
