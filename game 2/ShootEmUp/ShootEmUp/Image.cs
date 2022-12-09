using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Image
    {
        
        public IntPtr imageTexture;
        public IntPtr imageSurface;
        public SDL.SDL_Rect sRect;
        public SDL.SDL_Rect tRect;

        uint format;
        int access;
        public Image() {
            
        }

        public void setUp() {

            var imgFlags = SDL_image.IMG_InitFlags.IMG_INIT_PNG;
            if ((SDL_image.IMG_Init(imgFlags) > 0 & imgFlags > 0) == false)
            {
                Console.WriteLine("SDL_image could not initialize! SDL_image Error: {0}", SDL.SDL_GetError());
            }

        }



        public void loadImage(String source) {
            //image
            imageSurface = SDL_image.IMG_Load(source);
            imageTexture = SDL.SDL_CreateTextureFromSurface(Program.window.renderer, imageSurface);
            sRect.x = 0;
            sRect.y = 0;
            SDL.SDL_QueryTexture(imageTexture, out format, out access, out sRect.w, out sRect.h);
            Console.WriteLine("image w h ", sRect.w, sRect.h);

            //sRect.w = 48;
            //sRect.h = 16;
            /*
            tRect.x = Program.window.width / 2 - 1024 / 2;
            tRect.y = Program.window.heigh / 2 - (1024 - 480);
            tRect.w = 1024;
            tRect.h = 1024;

            //SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, ref tRect);
            SDL.SDL_RenderCopy(renderer, pumpkinTexture, ref sRect, ref tRect);
            //SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, IntPtr.Zero);

            // Switches out the currently presented render surface with the one we just did work on.
            SDL.SDL_RenderPresent(renderer);*/
        }
    }
}
