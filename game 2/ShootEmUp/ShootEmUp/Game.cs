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
        public LevelManager _levels = new LevelManager();
        private CollisionManager _collisions = new CollisionManager();
        private AIManager _ai = new AIManager();
        public ControlManager _controls = new ControlManager();

        private PoolManager _pool = new PoolManager();

        public bool Quit;

        public uint ticks = SDL.SDL_GetTicks();


        //public int display;


        public Game()
        {
            
            //display = 1;
        }

        public void tryHeroStuff()
        {
            Hero hero = new Hero();
            GameMaster gm = new GameMaster();
            hero.AddObserver(gm);
            while (Console.ReadKey().Key != ConsoleKey.Escape) {
                hero.SlayMonsters();
                //gm.DoTurn();
            }

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

        public GameObject BuildPlayer()
        {
            GameObject player = _objects.CreateGamePlayer("player", 16*2, 16*2);
            player.Active = true;
            player.AddComponent(_physics.CreateComponent());
            player.AddComponent(_rendering.CreateComponent(16*2, 16*2));
            player.AddComponent(_rendering.CreateInfoComponent());
            player.AddComponent(_controls.CreateComponent());
            player.AddComponent(_collisions.CreateComponent());
            return player;
        }


        //Enemy
        public GameObject BuildShip(GameObject ship)
        {
            ship = _objects.CreateGameShip("ship", 16*2, 16*2);
            ship.AddComponent(_physics.CreateComponent());
            ship.AddComponent(_rendering.CreateComponent(16*2, 16*2));
            ship.AddComponent(_ai.CreateComponent());
            ship.AddComponent(_collisions.CreateComponent());

            return ship;

        }

        //Enemy
        public GameObject BuildUfo(GameObject ufo)
        {
            ufo = _objects.CreateGameUfo("ufo", 16*2, 16*2);
            ufo.AddComponent(_physics.CreateComponent());
            ufo.AddComponent(_rendering.CreateComponent(16*2, 16*2));
            ufo.AddComponent(_ai.CreateComponent());
            ufo.AddComponent(_collisions.CreateComponent());

            return ufo;

        }

        public GameObject BuildPlayerBullet(GameObject bullet, GameObject player)
        {
            bullet = _objects.CreatePlayerBullet("bullet",player, 16 * 2, 16 * 2);
            bullet.AddComponent(_physics.CreateComponent());
            bullet.AddComponent(_rendering.CreateComponent(16 * 2, 16 * 2));
            bullet.AddComponent(_ai.CreateComponent());
            bullet.AddComponent(_collisions.CreateComponent());
            return bullet;

        }

        public GameObject RequestEnemyUfo()
        {
            return _pool.RequestEnemyUfo();
        }
        public void DespawnEnemyUfo(GameObject enemy)
        {
            _pool.DespawnEnemyUfo(enemy);
        }
        public GameObject RequestEnemyShip()
        {
            return _pool.RequestEnemyShip();
        }
        public void DespawnEnemyShip(GameObject enemy)
        {
            _pool.DespawnEnemyShip(enemy);
        }
        public GameObject RequestPlayerBullet(GameObject player)
        {
            return _pool.RequestPlayerBullet(player);
        }
        public void DespawnPlayerBullet(GameObject bullet)
        {
            _pool.DespawnPlayerBullet(bullet);
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

        public void Collide()
        {
            _collisions.Collide();
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
