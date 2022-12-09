using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class RenderingComponent : Component
    {
        private SDL.SDL_Rect rect;
        private SDL.SDL_Rect srcRect;

        RenderingManager RenderingManager;
        public RenderingComponent(RenderingManager rm)
        {
            this.RenderingManager = rm;
            srcRect.x = 16;
            srcRect.y = 0;
            srcRect.w = 16;
            srcRect.h = 16;

        }

        public void Render()
        {
            //SDL.SDL_SetRenderDrawColor(Program.window.renderer, 0, 60, 20, 255);            
            rect.x = (int)GameObject.PosX;
            rect.y = (int)GameObject.PosY;
            rect.w = 16;
            rect.h = 16;
            //SDL.SDL_RenderFillRect(Program.window.renderer, ref rect);

            SDL.SDL_RenderCopy(Program.window.renderer, GameObject.Img.imageTexture, ref srcRect, ref rect);
        }
    }
}
