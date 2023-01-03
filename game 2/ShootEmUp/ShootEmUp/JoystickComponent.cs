using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShootEmUp.LevelManager;

namespace ShootEmUp
{
    class JoystickComponent : Component
    {
        //Game Controller 1 handler
        IntPtr gGameController = new IntPtr();
        public string name;
        public int num_axes;
        public int num_buttons;
        public int num_hats;
        ControlManager ControlManager;
        public JoystickComponent(ControlManager cm)
        {

            this.ControlManager = cm;

        }
        public void Control()
        {

        }
        public bool setup()
        {
            //Initialization flag
            bool success = true;
            SDL.SDL_Init(SDL.SDL_INIT_JOYSTICK); //Init SDL and Control


            //Check for joysticks
            if (SDL.SDL_NumJoysticks() <= 1)
            {
                Console.WriteLine("Warning: No Controller connected!\n");
                success = false;
            }
            else
            {
                //Load joystick
                gGameController = SDL.SDL_GameControllerOpen(0);
                if (gGameController == null)
                {
                    Console.WriteLine("Warning: Unable to open game controller! SDL Error: %s\n", SDL.SDL_GetError());
                    success = false;
                }
            }
            if (success)
            {
                Console.WriteLine("Controller connected!");
            }
            return success;
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
                    case SDL.SDL_Keycode.SDLK_ESCAPE: LevelManager.display = GameState.MainMenu; LevelManager.ControlQuitRequest = true; break; //quit game

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