using System;
using SDL2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class ImageRenderingComponent : RenderingComponent
    {
        private SDL.SDL_Rect _rect;
        private SDL.SDL_Rect _srcRect;
        private int _dstX = 0;

        public ImageRenderingComponent(RenderingManager rm, int x, int y, int w, int h, int dstW, int dstH, int dstX = 0)
        {
            this.RenderingManager = rm;
            _srcRect.x = x;
            _srcRect.y = y;
            _srcRect.w = w;
            _srcRect.h = h;
            _rect.w = dstW;
            _rect.h = dstH;
            this._dstX = dstX;
        }

        override public void Render()
        {
            _rect.x = (int)(GameObject.PosX + _dstX - Program.Game.Camera.PosX + Program.Window.Width / 2);
            _rect.y = (int)(GameObject.PosY - Program.Game.Camera.PosY + Program.Window.Height / 2);

            //zum testen
            _srcRect.x = GameObject.ImgChange;


            SDL.SDL_RenderCopy(Program.Window.Renderer, GameObject.Img.ImageTexture, ref _srcRect, ref _rect);

            SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 255, 255, 255, 255);
            //SDL.SDL_RenderDrawLine(Program.window.renderer, 0, 0, (int)GameObject.PosX, (int)GameObject.PosY);
        }
    }
}
