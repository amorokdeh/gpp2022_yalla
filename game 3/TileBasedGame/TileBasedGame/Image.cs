using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace TileBasedGame
{
    class Image
    {

        public IntPtr ImageTexture;
        public IntPtr ImageSurface;
        public SDL.SDL_Rect SRect;
        public SDL.SDL_Rect TRect;
        public SDL.SDL_RendererFlip flipped = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
        public int rotateAngle = 0;

        public Image() {}

        public void SetUp()
        {
            var imgFlags = SDL_image.IMG_InitFlags.IMG_INIT_PNG;
            if ((SDL_image.IMG_Init(imgFlags) > 0 & imgFlags > 0) == false)
            {
                Console.WriteLine("SDL_image could not initialize! SDL_image Error: {0}", SDL.SDL_GetError());
            }

        }

        public void LoadImage(String source)
        {
            //image
            ImageSurface = SDL_image.IMG_Load(source);
            ImageTexture = SDL.SDL_CreateTextureFromSurface(Program.Window.Renderer, ImageSurface);
            SDL.SDL_QueryTexture(ImageTexture, out _, out _, out SRect.w, out SRect.h);
        }
    }
}
