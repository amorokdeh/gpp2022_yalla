using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Image
    {
        
        public IntPtr ImageTexture;
        public IntPtr ImageSurface;
        public SDL.SDL_Rect SRect;
        public SDL.SDL_Rect TRect;

        uint format;
        int access;
        public Image() {
            
        }

        public void SetUp() {

            var imgFlags = SDL_image.IMG_InitFlags.IMG_INIT_PNG;
            if ((SDL_image.IMG_Init(imgFlags) > 0 & imgFlags > 0) == false)
            {
                Console.WriteLine("SDL_image could not initialize! SDL_image Error: {0}", SDL.SDL_GetError());
            }

        }

        public void LoadImage(String source) {
            //image
            ImageSurface = SDL_image.IMG_Load(source);
            ImageTexture = SDL.SDL_CreateTextureFromSurface(Program.Window.Renderer, ImageSurface);
            SRect.x = 0;
            SRect.y = 0;
            SDL.SDL_QueryTexture(ImageTexture, out format, out access, out SRect.w, out SRect.h);
        }
    }
}
