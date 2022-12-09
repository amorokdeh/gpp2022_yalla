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
        List<RenderingComponent> _renderingComponents = new List<RenderingComponent>();

        internal Component CreateComponent()
        {
            RenderingComponent rc = new RenderingComponent(this, 16, 0, 16, 16, 16*2, 16*2);
            _renderingComponents.Add(rc);
            return rc;
        }
        internal Component CreateBGComponent()
        {
            RenderingComponent rc = new RenderingComponent(this, 0, 0, 128, 64, 128*6, 64*6);
            _renderingComponents.Add(rc);
            return rc;
        }

        public void Render()
        {
            SDL.SDL_SetRenderDrawColor(Program.window.renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(Program.window.renderer);
            foreach (var component in _renderingComponents)
            {
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
