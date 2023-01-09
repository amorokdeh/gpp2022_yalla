using System;
using SDL2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class RenderingManager
    {
        private List<RenderingComponent> _renderingComponents = new List<RenderingComponent>();
        
        internal Component CreateComponent(int w, int h)
        {

            RenderingComponent rc = new ImageRenderingComponent(this, 0, 0, 16, 16, w, h);
            _renderingComponents.Add(rc);
            return rc;
        }
        internal Component CreateInfoComponent()
        {

            InfoRenderingComponent rc = new InfoRenderingComponent(this);
            _renderingComponents.Add(rc);
            return rc;
        }
        internal Component CreateBGComponent(int x, int y, int w, int h, int dstW, int dstH, int dstX = 0)
        {
            RenderingComponent rc = new ImageRenderingComponent(this, x, y, w, h, dstW, dstH, dstX);
            _renderingComponents.Add(rc);
            return rc;
        }

        public void Render()
        {
            SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(Program.Window.Renderer);
            foreach (var component in _renderingComponents)
            {
                if(component.GameObject.Active)
                    component.Render();

                
            }
            Program.Window.FPSCalculate();


            // _renderingComponents[0].Render();
            /*
            foreach(var rc in SubsetThatNeedsRendering())
            {
                rc.Render();
            }*/
            if (!Program.Game._levels.MainMenu.Running)
            {
                SDL.SDL_RenderPresent(Program.Window.Renderer);
            }
        }

        //noch machen, wenn es wirklich gebraucht wird
        public IEnumerable<RenderingComponent> SubsetThatNeedsRendering()
        {
            yield break;
        }
    }
}
