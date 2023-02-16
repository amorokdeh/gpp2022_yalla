using SDL2;
using System;
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

        public void Control() {}

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
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_UP:
                        MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoUp, Program.Game.Player));
                        break;
                    case SDL.SDL_Keycode.SDLK_DOWN:
                        MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoDown, Program.Game.Player));
                        break;
                    case SDL.SDL_Keycode.SDLK_LEFT:
                        MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoLeft, Program.Game.Player));
                        break;
                    case SDL.SDL_Keycode.SDLK_RIGHT:
                        MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoRight, Program.Game.Player));
                        break;
                    case SDL.SDL_Keycode.SDLK_SPACE:
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.TryShooting, Program.Game.Player));
                        break;
                    case SDL.SDL_Keycode.SDLK_ESCAPE:
                        LevelManager.display = GameState.MainMenu;
                        LevelManager.ControlQuitRequest = true;
                        break; //quit game
                }
            }
            else if (e.type == SDL.SDL_EventType.SDL_KEYUP)
            {
                //Adjust the velocity
                switch (e.key.keysym.sym)
                {
                    case SDL.SDL_Keycode.SDLK_LEFT:
                        MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoRight, Program.Game.Player));
                        break;
                    case SDL.SDL_Keycode.SDLK_RIGHT:
                        MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoLeft, Program.Game.Player));
                        break;
                    case SDL.SDL_Keycode.SDLK_SPACE:
                        MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.StopShooting, Program.Game.Player));
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
                    MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoLeft, Program.Game.Player));
                    _axisX = "left";
                }
                else if (x > movingPoint) // Move character right
                {
                    MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoRight, Program.Game.Player));
                    _axisX = "right";
                }
                else { //stop
                    if (_axisX == "left") { MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoRight, Program.Game.Player)); _axisX = "none"; }
                    if (_axisX == "right") { MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoLeft, Program.Game.Player)); _axisX = "none"; }
                }


                if (y < -movingPoint) // Move character up
                {
                    MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoUp, Program.Game.Player));
                    _axisY = "up";
                }
                else if (y > movingPoint) // ducking
                {
                    MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoDown, Program.Game.Player));
                    _axisY = "down";
                }
                else
                { //stop
                    if (_axisY == "up") { MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoDown, Program.Game.Player)); _axisY = "none"; }
                    if (_axisY == "down") { MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoUp, Program.Game.Player)); _axisY = "none"; }
                }
            }
        }
    }
}
