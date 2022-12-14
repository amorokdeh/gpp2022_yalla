﻿using System;
using SDL2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class RenderingManager
    {
        List<RenderingComponent> _renderingComponents = new List<RenderingComponent>();

        internal Component CreateComponent(int w, int h)
        {

            RenderingComponent rc = new RenderingComponent(this, 0, 0, 16, 16, w, h);
            _renderingComponents.Add(rc);
            return rc;
        }
        internal Component CreateBGComponent(int x, int y, int w, int h, int dstW, int dstH, int dstX = 0)
        {
            RenderingComponent rc = new RenderingComponent(this, x, y, w, h, dstW, dstH, dstX);
            _renderingComponents.Add(rc);
            return rc;
        }

        public void Render()
        {
            SDL.SDL_SetRenderDrawColor(Program.window.renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(Program.window.renderer);
            foreach (var component in _renderingComponents)
            {
                if(component.GameObject.Active)
                    component.Render();
            }
               // _renderingComponents[0].Render();
            /*
            foreach(var rc in SubsetThatNeedsRendering())
            {
                rc.Render();
            }*/
            SDL.SDL_RenderPresent(Program.window.renderer);
        }

        public IEnumerable<RenderingComponent> SubsetThatNeedsRendering()
        {
            yield break;
        }
    }
}