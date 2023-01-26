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
        private string _axisX = "none";
        private string _axisY = "none";


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
            if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP:
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoUp));
                        break;
                    case SDL.SDL_Keycode.SDLK_DOWN:
                        //MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoDown));
                        break;
                    case SDL.SDL_Keycode.SDLK_LEFT:
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoLeft));
                        break;
                    case SDL.SDL_Keycode.SDLK_RIGHT:
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoRight));
                        break;
                    case SDL.SDL_Keycode.SDLK_ESCAPE: 
                        LevelManager.display = GameState.MainMenu; 
                        LevelManager.ControlQuitRequest = true; 
                        break; //quit game

                }
            }
            else if (e.type == SDL.SDL_EventType.SDL_KEYUP && e.key.repeat == 0)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP:
                        //MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoDown));
                        break;
                    case SDL.SDL_Keycode.SDLK_DOWN:
                       // MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoUp));
                        break;
                    case SDL.SDL_Keycode.SDLK_LEFT:
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoRight));
                        break;
                    case SDL.SDL_Keycode.SDLK_RIGHT:
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoLeft));
                        break;


                }
            }
            //Controller
            int x = SDL.SDL_JoystickGetAxis(GGameController, 0);
            int y = SDL.SDL_JoystickGetAxis(GGameController, 1);
            int movingPoint = 16384; //you can change it (It works perfectly on PS5 controller)

            if (e.type == SDL.SDL_EventType.SDL_JOYAXISMOTION)
            {

                if (x < -movingPoint) // Move character left
                {
                    MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoLeft));
                    _axisX = "left";
                }
                else if (x > movingPoint) // Move character right
                {
                    MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoRight));
                    _axisX = "right";
                }
                else { //stop
                    if (_axisX == "left") { MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoRight)); _axisX = "none"; }
                    if (_axisX == "right") { MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoLeft)); _axisX = "none"; }
                }
                    

                if (y < -movingPoint) // Move character up
                {
                    MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoUp));
                    _axisY = "up";
                }
                else if (y > movingPoint) // Move character down
                {
                    MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoDown));
                    _axisY = "down";
                }
                else
                { //stop
                    if (_axisY == "up") { MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoDown)); _axisY = "none"; }
                    if (_axisY == "down") { MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.GoUp)); _axisY = "none"; }
                }
            }

        }
    }
}
