using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    class Control
    {
        //Game Controller 1 handler
        IntPtr gGameController = new IntPtr();
        public string name;
        public int num_axes;
        public int num_buttons;
        public int num_hats;
        public Control() { 
        
        }
        public bool setup()
        {
            //Initialization flag
            bool success = true;
            SDL.SDL_Init(SDL.SDL_INIT_GAMECONTROLLER); //Init SDL and Control


            //Check for joysticks
            if (SDL.SDL_NumJoysticks() < 1)
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
    }
}