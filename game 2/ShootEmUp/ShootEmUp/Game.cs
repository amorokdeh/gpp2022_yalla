using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Game
    {


        private GameObjectManager _objects = new GameObjectManager();
        private PhysicsManager _physics = new PhysicsManager();
        private RenderingManager _rendering = new RenderingManager();
        private AudioManager _audio = new AudioManager();
        private LevelManager _levels = new LevelManager();





        public void BuildGameObject()
        {
            GameObject ship = _objects.CreateGameObject("ship");
            ship.AddComponent(_physics.CreateComponent());
        }





        public int display;

        public Game() {
            display = 1;
        }
        
        
        //Game loop
        public void run() {
            while (true) { 
                switch (display) { //if display = 0 end the game
                    case 0: quit(); return;
                    case 1: _levels.runMainMenu(); break;
                    case 2: _levels.runLevel1(); break;
                    case 3: _levels.runLevel2(); break;
                    case 4: _levels.runLevel3(); break;
                    case 5: _levels.runGameOver(); break;
                }
            }
            
        }

        public void quit()
        {
            _objects = null;
            _physics = null;
            _rendering = null;
            _audio = null;
            _levels = null;


            SDL.SDL_DestroyWindow(Program.window.show);
            SDL.SDL_Quit();
        }
    }
}
