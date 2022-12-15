using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class ControlComponent : Component
    {
        ControlManager ControlManager;
        public ControlComponent(ControlManager cm)
        {
            this.ControlManager = cm;
        }

        public void Control()
        {

        }
        public void HandleEvent(SDL.SDL_Event e)
        {

            //If a key was pressed
            if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0)
            {

                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP: GameObject.CurrentVelY -= GameObject.VelY; break;
                    case SDL.SDL_Keycode.SDLK_DOWN: GameObject.CurrentVelY += GameObject.VelY; break;
                    case SDL.SDL_Keycode.SDLK_LEFT: GameObject.CurrentVelX -= GameObject.VelX; break;
                    case SDL.SDL_Keycode.SDLK_RIGHT: GameObject.CurrentVelX += GameObject.VelX; break;

                }
            }
            else if (e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP: GameObject.CurrentVelY += GameObject.VelY; break;
                    case SDL.SDL_Keycode.SDLK_DOWN: GameObject.CurrentVelY -= GameObject.VelY; break;
                    case SDL.SDL_Keycode.SDLK_LEFT: GameObject.CurrentVelX += GameObject.VelX; break;
                    case SDL.SDL_Keycode.SDLK_RIGHT: GameObject.CurrentVelX -= GameObject.VelX; break;

                }
            }

            
        }
    }
}
