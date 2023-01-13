using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TileBasedGame.LevelManager;

namespace TileBasedGame
{
    class ControlComponent : Component
    {
        ControlManager ControlManager;

        //Controller
        public IntPtr GGameController = new IntPtr();
        private string _axisX = "None";
        private string _axisY = "None";
        private bool _movingX = false;
        private bool _movingY = false;

        //to avoid some bugs with the velocity
        private bool _pressedOnKeyboard = false;
        private bool _pressedOnJoystick = false;

        public ControlComponent(ControlManager cm)
        {
            this.ControlManager = cm;
            SetupController();
        }

        public void Control()
        {

        }

        public bool SetupController()
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
                GGameController = SDL.SDL_JoystickOpen(0);
                if (GGameController == null)
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
            if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0 && !_pressedOnJoystick)
            {
                _pressedOnKeyboard = true;
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
            else if (e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0 && !_pressedOnJoystick)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP: GameObject.CurrentVelY += GameObject.VelY; _pressedOnKeyboard = false; break;
                    case SDL.SDL_Keycode.SDLK_DOWN: GameObject.CurrentVelY -= GameObject.VelY; _pressedOnKeyboard = false; break;
                    case SDL.SDL_Keycode.SDLK_LEFT: GameObject.CurrentVelX += GameObject.VelX; _pressedOnKeyboard = false; break;
                    case SDL.SDL_Keycode.SDLK_RIGHT: GameObject.CurrentVelX -= GameObject.VelX; _pressedOnKeyboard = false; break;

                }
            }
            else if(!_pressedOnKeyboard)
            {
                //Controller
                int x = SDL.SDL_JoystickGetAxis(GGameController, 0);
                int y = SDL.SDL_JoystickGetAxis(GGameController, 1);
                int movingPoint = 16384; //you can change it (It works perfectly on PS5 controller)

                if (e.type == SDL.SDL_EventType.SDL_JOYAXISMOTION)
                {

                    if (x < -movingPoint) // Move character left
                    {
                        _axisX = "Left";
                    }
                    else if (x > movingPoint) // Move character right
                    {
                        _axisX = "Right";
                    }
                    else //stop caracter
                    {
                        if (_axisX == "Left") { GameObject.CurrentVelX += GameObject.VelX; }
                        if (_axisX == "Right") { GameObject.CurrentVelX -= GameObject.VelX; }
                        _axisX = "None";
                        _movingX = false;
                    }

                    if (y < -movingPoint) // Move character up
                    {
                        _axisY = "Up";
                    }
                    else if (y > movingPoint) // Move character down
                    {
                        _axisY = "Down";
                    }
                    else //stop caracter
                    {
                        if (_axisY == "Up") { GameObject.CurrentVelY += GameObject.VelY; }
                        if (_axisY == "Down") { GameObject.CurrentVelY -= GameObject.VelY; }
                        _axisY = "None";
                        _movingY = false;
                    }
                }

                //Move using controller
                if (_axisX == "Left" && !_movingX) { GameObject.CurrentVelX -= GameObject.VelX; _movingX = true; _pressedOnJoystick = true; }
                if (_axisX == "Right" && !_movingX) { GameObject.CurrentVelX += GameObject.VelX; _movingX = true; _pressedOnJoystick = true; }
                if (_axisY == "Up" && !_movingY) { GameObject.CurrentVelY -= GameObject.VelY; _movingY = true; _pressedOnJoystick = true; }
                if (_axisY == "Down" && !_movingY) { GameObject.CurrentVelY += GameObject.VelY; _movingY = true; _pressedOnJoystick = true; }
                if (!_movingX && !_movingY) { _pressedOnJoystick = false; }

            }
        }
    }
}
