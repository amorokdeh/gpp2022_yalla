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
        }
    }
}
