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
        public AudioManager _audio = new AudioManager();
        public LevelManager _levels = new LevelManager();
        private CollisionManager _collisions = new CollisionManager();
        private AIManager _ai = new AIManager();
        public ControlManager _controls = new ControlManager();
        private AnimationManager _animations = new AnimationManager();
        private PoolManager _pool = new PoolManager();


        public bool bulletReloadable = false;

        

        public static bool Quit;

        public uint ticks = SDL.SDL_GetTicks();



        public Player Player;
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

        public void BuildBackground(string source)
        {
            int winW = Program.window.width;
            int winH = Program.window.height;

            GameObject bg;


            for (int i = -1; i < (winH / 128 * 4); i++)
            {
                bg = _objects.CreateGameBackground(source, 128 * 4, 64 * 4, 0, 64 * 4 * i);
                bg.Active = true;
                bg.AddComponent(_physics.CreateBGComponent());

                for (int j = 0; j < winW / (64 * 4); j++)
                {
                    bg.AddComponent(_rendering.CreateBGComponent(0, 0, 128, 64, 128 * 4, 64 * 4, 128 * 4 * j));
                }

                bg.AddComponent(_ai.CreateComponent());
            }
        }

        public GameObject BuildPlayer()
        {
            Player player = _objects.CreateGamePlayer("player", 16 * 2, 16 * 2);
            player.Active = true;
            player.AddComponent(_physics.CreateComponent());
            player.AddComponent(_rendering.CreateComponent(16 * 2, 16 * 2));
            player.AddComponent(_rendering.CreateInfoComponent());
            player.AddComponent(_controls.CreateComponent());
            player.AddComponent(_collisions.CreateComponent());
            return player;
        }


        //Enemy
        public GameObject BuildShip(GameObject ship)
        {
            ship = _objects.CreateGameShip("ship", 16 * 2, 16 * 2);
            ship.AddComponent(_physics.CreateComponent());
            ship.AddComponent(_rendering.CreateComponent(16 * 2, 16 * 2));
            ship.AddComponent(_ai.CreateComponent());
            ship.AddComponent(_collisions.CreateComponent());
            ship.AddComponent(_animations.CreateComponent());

            return ship;

        }

        //Enemy
        public GameObject BuildUfo(GameObject ufo)
        {
            ufo = _objects.CreateGameUfo("ufo", 16 * 2, 16 * 2);
            ufo.AddComponent(_physics.CreateComponent());
            ufo.AddComponent(_rendering.CreateComponent(16 * 2, 16 * 2));
            ufo.AddComponent(_ai.CreateComponent());
            ufo.AddComponent(_collisions.CreateComponent());
            ufo.AddComponent(_animations.CreateComponent());

            return ufo;

        }

        public GameObject BuildPlayerBullet(GameObject bullet, GameObject player)
        {
            bullet = _objects.CreatePlayerBullet("bullet", player, 16 * 2, 16 * 2);
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
        public void DespawnEnemy(GameObject enemy)
        {
            _pool.DespawnEnemy(enemy);
        }
        public GameObject RequestEnemyShip()
        {
            return _pool.RequestEnemyShip();
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

        public void SetInactive()
        {
            _pool.SetInactive();

        }

        public void Animate(float deltaT)
        {
            _animations.Animate(deltaT);
        }






        //Game loop
        public void run() {
            BuildBackground("level 1");
            Player = (Player)BuildPlayer();
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
            _audio.cleanUp();
            SDL.SDL_Quit();
        }
    }
}
