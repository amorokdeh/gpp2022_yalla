﻿using SDL2;
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
        int dstX = 0;

        RenderingManager RenderingManager;
        public RenderingComponent(RenderingManager rm, int x, int y, int w, int h, int dstW, int dstH, int dstX=0)
        {
            this.RenderingManager = rm;
            srcRect.x = x;
            srcRect.y = y;
            srcRect.w = w;
            srcRect.h = h;
            rect.w = dstW;
            rect.h = dstH;
            this.dstX = dstX;
        }

        public void Render()
        {         
            rect.x = (int)GameObject.PosX + dstX;
            rect.y = (int)GameObject.PosY;      
            
            //zum testen

                srcRect.x = GameObject.ImgChange;


            SDL.SDL_RenderCopy(Program.window.renderer, GameObject.Img.imageTexture, ref srcRect, ref rect);
        }
    }
}