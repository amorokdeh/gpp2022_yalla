using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShootEmUp.LevelManager;

namespace ShootEmUp
{
    class ControlComponent : Component
    {
        ControlManager ControlManager;

        //Controller
        public IntPtr gGameController = new IntPtr();
        string axisX = "None";
        string axisY = "None";
        bool movingX = false;
        bool movingY = false;

        //to avoid some bugs with the velocity
        bool pressedOnKeyboard = false;
        bool pressedOnJoystick = false;

        public ControlComponent(ControlManager cm)
        {
            this.ControlManager = cm;
            setupController();
        }

        public void Control()
        {

        }

        public bool setupController()
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
            
            //If a key was pressed
            if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0 && !pressedOnJoystick)
            {
                pressedOnKeyboard = true;
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
            else if (e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0 && !pressedOnJoystick)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP: GameObject.CurrentVelY += GameObject.VelY; pressedOnKeyboard = false; break;
                    case SDL.SDL_Keycode.SDLK_DOWN: GameObject.CurrentVelY -= GameObject.VelY; pressedOnKeyboard = false; break;
                    case SDL.SDL_Keycode.SDLK_LEFT: GameObject.CurrentVelX += GameObject.VelX; pressedOnKeyboard = false; break;
                    case SDL.SDL_Keycode.SDLK_RIGHT: GameObject.CurrentVelX -= GameObject.VelX; pressedOnKeyboard = false; break;

                }
            }
            else if(!pressedOnKeyboard)
            {
                //Controller
                int x = SDL.SDL_JoystickGetAxis(gGameController, 0);
                int y = SDL.SDL_JoystickGetAxis(gGameController, 1);
                int movingPoint = 16384; //you can change it (It works perfectly on PS5 controller)

                if (e.type == SDL.SDL_EventType.SDL_JOYAXISMOTION)
                {

                    if (x < -movingPoint) // Move character left
                    {
                        axisX = "Left";
                    }
                    else if (x > movingPoint) // Move character right
                    {
                        axisX = "Right";
                    }
                    else //stop caracter
                    {
                        if (axisX == "Left") { GameObject.CurrentVelX += GameObject.VelX; }
                        if (axisX == "Right") { GameObject.CurrentVelX -= GameObject.VelX; }
                        axisX = "None";
                        movingX = false;
                    }

                    if (y < -movingPoint) // Move character up
                    {
                        axisY = "Up";
                    }
                    else if (y > movingPoint) // Move character down
                    {
                        axisY = "Down";
                    }
                    else //stop caracter
                    {
                        if (axisY == "Up") { GameObject.CurrentVelY += GameObject.VelY; }
                        if (axisY == "Down") { GameObject.CurrentVelY -= GameObject.VelY; }
                        axisY = "None";
                        movingY = false;
                    }
                }

                //Move using controller
                if (axisX == "Left" && !movingX) { GameObject.CurrentVelX -= GameObject.VelX; movingX = true; pressedOnJoystick = true; }
                if (axisX == "Right" && !movingX) { GameObject.CurrentVelX += GameObject.VelX; movingX = true; pressedOnJoystick = true; }
                if (axisY == "Up" && !movingY) { GameObject.CurrentVelY -= GameObject.VelY; movingY = true; pressedOnJoystick = true; }
                if (axisY == "Down" && !movingY) { GameObject.CurrentVelY += GameObject.VelY; movingY = true; pressedOnJoystick = true; }
                if (!movingX && !movingY) { pressedOnJoystick = false; }

            }
        }
    }
}
