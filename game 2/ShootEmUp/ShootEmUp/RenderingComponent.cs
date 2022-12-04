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

        RenderingManager RenderingManager;
        public RenderingComponent(RenderingManager rm)
        {
            this.RenderingManager = rm;
        }

        public void Render()
        {
            //SDL.SDL_SetRenderDrawColor(Program.window.renderer, 0, 60, 20, 255);            
            rect.x = GameObject.PosX;
            rect.y = GameObject.PosY;
            rect.w = GameObject.Width;
            rect.h = GameObject.Height;
            //SDL.SDL_RenderFillRect(Program.window.renderer, ref rect);

            SDL.SDL_RenderCopy(Program.window.renderer, GameObject.Img.pumpkinTexture, ref GameObject.Img.sRect, ref rect);
        }
    }
}
