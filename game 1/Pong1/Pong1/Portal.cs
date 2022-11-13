using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Pong
{
    class Portal
    {

        public int leftX = Program.window.width / 2 - 100;
        int upY;
        SDL.SDL_Rect portal;
        byte red, green, blue;


        public Portal(byte r, byte g, byte b, int y)
        {
            red = r;
            green = g;
            blue = b;
            upY = y;
            portal = new SDL.SDL_Rect
            {
                x = leftX,
                y = y,
                w = 0,
                h = 0
            };
        }



        public void render(IntPtr renderer)
        {
            
            
            // Farbverlauf erstellen
            for(int i=0; i<12; i++)
            {       
                SDL.SDL_SetRenderDrawBlendMode(renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
                SDL.SDL_SetRenderDrawColor(renderer, red, green, blue, (byte)(120 - i * 10));
                portal.x = leftX - i;
                portal.y = upY - i ;
                portal.w = 200 + i * 2;
                portal.h = 1 + i *2;
                SDL.SDL_RenderFillRect(renderer, ref portal);

            }
            
        }
    }
}
