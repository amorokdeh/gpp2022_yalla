using SDL2;
using System.Collections.Generic;

namespace TileBasedGame
{
    class RenderingManager
    {
        private List<RenderingComponent> _renderingComponents = new List<RenderingComponent>();
        SDL.SDL_Rect BackgroundRect;
        private float _opacity = 0;
        private float _gap = 0;

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

            if (!Program.Game.Levels.MainMenu.Running && _opacity == 0)
            {
                SDL.SDL_RenderPresent(Program.Window.Renderer);
            }
        }

        public void SetOpacity(float opacity)
        {
            _opacity = opacity;
        }

        public void SetUpRedBlend()
        {
            _opacity = 0;
            SDL.SDL_SetRenderDrawBlendMode(Program.Window.Renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);

            BackgroundRect = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = Program.Window.Width,
                h = Program.Window.Height,
            };
        }
        public void RedBlend(float deltaT)
        {
            _opacity += deltaT * 220f;            
            int op = (int)_opacity;
            if (op> 255)
                op = 255;
            if(_gap > 0.1)
            {
                _gap = 0;
            } 
            else if(_gap > 0.05)
            {
                SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 100, 0, 0, (byte)op);
                
            }
            else
            {
                SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 255, 0, 0, (byte)op);
            }
            _gap += deltaT;
            SDL.SDL_RenderFillRect(Program.Window.Renderer, ref BackgroundRect);
            SDL.SDL_RenderPresent(Program.Window.Renderer);
        }


        public void ClearObjects()
        {  
            for (int i = 0; i < _renderingComponents.Count; i++)
            {
                _renderingComponents[i] = null;
            }
            _renderingComponents.Clear();
        }
    }
}
