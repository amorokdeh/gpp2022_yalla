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
                gGameController = SDL.SDL_JoystickOpen(0);
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
            // The joystick was moved
            int x = SDL.SDL_JoystickGetAxis(gGameController, 0);
            int y = SDL.SDL_JoystickGetAxis(gGameController, 1);
            Console.WriteLine(x);

            //If a key was pressed
            if (e.type == SDL.SDL_EventType.SDL_JOYAXISMOTION)
            {
              

                // Move the character based on the joystick input
                if (x < -16384)
                {
                    // Move character left
                    Console.WriteLine("Left");
                    GameObject.CurrentVelX -= GameObject.VelX;
                }
                else if (x > 16384)
                {
                    // Move character right
                    Console.WriteLine("Right");
                    GameObject.CurrentVelX += GameObject.VelX;
                }
                else {
                    //stop haracter
                    GameObject.CurrentVelX *= -1;
                }

                if (y < -16384)
                {
                    // Move character up
                    Console.WriteLine("up");
                    GameObject.CurrentVelY -= GameObject.VelY;
                }
                else if (y > 16384)
                {
                    // Move character down
                    Console.WriteLine("down");
                    GameObject.CurrentVelY += GameObject.VelY;
                }
                else
                {
                    //stop haracter
                    GameObject.CurrentVelY *= -1;
                }

            }
        }  
            
        
    }
}