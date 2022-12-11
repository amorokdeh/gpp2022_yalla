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

        private PoolManager _pool = new PoolManager();

        public bool Quit;


        

        //public int display;


        public Game()
        {
            
            //display = 1;
        }

        public void BuildBackground()
        {
            GameObject bg = _objects.CreateGameBackground("background", 128*4, 64*4, 0, 0);
            bg.Active = true;
            bg.AddComponent(_physics.CreateBGComponent());
            bg.AddComponent(_rendering.CreateBGComponent(0, 0, 128, 64, 128 * 4, 64 * 4));
            bg.AddComponent(_rendering.CreateBGComponent(0, 0, 128, 64, 128 * 4, 64 * 4, 128*4));
            bg.AddComponent(_ai.CreateComponent());

            GameObject bg2 = _objects.CreateGameBackground("background", 128 * 4, 64 * 4, 0, -64*4);
            bg2.Active = true;
            bg2.AddComponent(_physics.CreateBGComponent());
            bg2.AddComponent(_rendering.CreateBGComponent(0, 0, 128, 64, 128 * 4, 64 * 4));
            bg2.AddComponent(_rendering.CreateBGComponent(0, 0, 128, 64, 128 * 4, 64 * 4, 128 * 4));
            bg2.AddComponent(_ai.CreateComponent());

            GameObject bg3 = _objects.CreateGameBackground("background", 128 * 4, 64 * 4, 0, 64*4);
            bg3.Active = true;
            bg3.AddComponent(_physics.CreateBGComponent());
            bg3.AddComponent(_rendering.CreateBGComponent(0, 0, 128, 64, 128 * 4, 64 * 4));
            bg3.AddComponent(_rendering.CreateBGComponent(0, 0, 128, 64, 128 * 4, 64 * 4, 128 * 4));
            bg3.AddComponent(_ai.CreateComponent());
        }

        public void BuildPlayer()
        {
            GameObject player = _objects.CreateGamePlayer("player", 16*2, 16*2);
            player.Active = true;
            player.AddComponent(_physics.CreateComponent());
            player.AddComponent(_rendering.CreateComponent(16*2, 16*2));
            player.AddComponent(_controls.CreateComponent());
        }


        //Enemy
        public void BuildShip()
        {
            GameObject ship = _objects.CreateGameShip("ship", 16*2, 16*2);
            ship.Active = true;
            ship.AddComponent(_physics.CreateComponent());
            ship.AddComponent(_rendering.CreateComponent(16*2, 16*2));
            ship.AddComponent(_ai.CreateComponent());

        }

        //Enemy
        public GameObject BuildUfo(GameObject ufo)
        {
            ufo = _objects.CreateGameUfo("ufo", 16*2, 16*2);
            ufo.AddComponent(_physics.CreateComponent());
            ufo.AddComponent(_rendering.CreateComponent(16*2, 16*2));
            ufo.AddComponent(_ai.CreateComponent());

            return ufo;

        }

        public GameObject RequestEnemy()
        {
            return _pool.RequestEnemy();
        }
        public void DespawnEnemy(GameObject enemy)
        {
            _pool.DespawnEnemy(enemy);
        }


        public void ControlPlayer()
        {
            _controls.Control();
        }
        public void ControlEnemy()
        {
            _ai.Control();
        }


        public void Move(float deltaT)
        {
            _physics.Move(deltaT);
        }


        public void Render()
        {
            _rendering.Render();
        }




        
        
        //Game loop
        public void run() {
            _levels.run();    
        }


 


        public void quit()
        {
            /*
            _levels = null;
            _objects = null;
            _physics = null;
            _rendering = null;
            _audio = null;            
            _collisions = null;
            _ai = null;
            _controls = null;*/


            SDL.SDL_DestroyWindow(Program.window.show);
            SDL.SDL_Quit();
        }
    }
}
