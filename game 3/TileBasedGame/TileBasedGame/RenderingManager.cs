using System;
using SDL2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class RenderingManager
    {
        private List<RenderingComponent> _renderingComponents = new List<RenderingComponent>();


        internal Component CreateComponent(Image img, int w, int h)
        {
            return CreateComponent(img, w, h, Globals.NormalImageSize, Globals.NormalImageSize);
        }

        internal Component CreateComponent(Image img, int w, int h, int imgW, int imgH)
        {

            RenderingComponent rc = new ImageRenderingComponent(this,img, 0, 0, imgW, imgH, w, h);
            _renderingComponents.Add(rc);
            return rc;
        }

        internal Component CreateComponent(Image img, int imgFrame, int w, int h, int imgW, int imgH)
        {

            RenderingComponent rc = new ImageRenderingComponent(this, imgFrame, img, 0, 0, imgW, imgH, w, h);
            _renderingComponents.Add(rc);
            return rc;
        }
        internal Component CreateComponent(Image img, int w, int h, int imgW, int imgH, int x, int y)
        {

            RenderingComponent rc = new ImageRenderingComponent(this, img, x, y, imgW, imgH, w, h);
            _renderingComponents.Add(rc);
            return rc;
        }

        internal Component CreateInfoComponent()
        {

            InfoRenderingComponent rc = new InfoRenderingComponent(this);
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

        public void clearObjects()
        {

            for (int i = 0; i < _renderingComponents.Count; i++)
            {
                _renderingComponents[i] = null;
            }
            _renderingComponents.Clear();
            GC.Collect();
        }
    }
}
