using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Image
    {
        public IntPtr pumpkinTexture = IntPtr.Zero;
        public SDL.SDL_Rect sRect;
        public SDL.SDL_Rect tRect;
        public Image() {
            
        }

        public void setUp() {

            var imgFlags = SDL_image.IMG_InitFlags.IMG_INIT_PNG;
            if ((SDL_image.IMG_Init(imgFlags) > 0 & imgFlags > 0) == false)
            {
                Console.WriteLine("SDL_image could not initialize! SDL_image Error: {0}", SDL.SDL_GetError());
            }

        }

        public void loadImage(IntPtr renderer, String source) {
            //image
            pumpkinTexture = SDL_image.IMG_LoadTexture(renderer, source);
            sRect.x = 0;
            sRect.y = 0;
            sRect.w = 128;
            sRect.h = 128;

            tRect.x = Program.window.width / 2 - 1024 / 2;
            tRect.y = Program.window.heigh / 2 - (1024 - 480);
            tRect.w = 1024;
            tRect.h = 1024;

            //SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, ref tRect);
            SDL.SDL_RenderCopy(renderer, pumpkinTexture, ref sRect, ref tRect);
            //SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, IntPtr.Zero);

            // Switches out the currently presented render surface with the one we just did work on.
            SDL.SDL_RenderPresent(renderer);
        }
    }
}
