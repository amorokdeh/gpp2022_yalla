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
        private CollisionManager _collisions = new CollisionManager();
        private AIManager _ai = new AIManager();
        private ControlManager _controls = new ControlManager();



        public int display;


        public Game()
        {
            
            display = 1;
        }

        public void BuildPlayer()
        {
            GameObject player = _objects.CreateGamePlayer("player");
            player.AddComponent(_physics.CreateComponent());
            player.AddComponent(_rendering.CreateComponent());
            player.AddComponent(_controls.CreateComponent());
        }


        //Enemy
        public void BuildShip()
        {
            GameObject ship = _objects.CreateGameShip("ship");
            ship.AddComponent(_physics.CreateComponent());
            ship.AddComponent(_rendering.CreateComponent());
            ship.AddComponent(_ai.CreateComponent());

        }

        //Enemy
        public void BuildUfo()
        {
            GameObject ufo = _objects.CreateGameUfo("ufo");
            ufo.AddComponent(_physics.CreateComponent());
            ufo.AddComponent(_rendering.CreateComponent());
            ufo.AddComponent(_ai.CreateComponent());

        }


        public void ControlPlayer()
        {
            _controls.Control();
        }
        public void ControlEnemy()
        {
            _ai.Control();
        }


        public void Move()
        {
            _physics.Move();
        }


        public void Render()
        {
            _rendering.Render();
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
            _collisions = null;
            _ai = null;
            _controls = null;


            SDL.SDL_DestroyWindow(Program.window.show);
            SDL.SDL_Quit();
        }
    }
}
