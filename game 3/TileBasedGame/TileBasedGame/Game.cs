using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
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
        private ControlManager _controls = new ControlManager();
        private AnimationManager _animations = new AnimationManager();
        private PoolManager _pool = new PoolManager();
        private ShootingManager _shootings = new ShootingManager();


        public bool BulletReloadable = false;
        public static bool Quit;
        public uint Ticks = SDL.SDL_GetTicks();
        public Player Player;
        public Camera Camera;
        public TiledMap tiledMap = new TiledMap();

        public Game()
        {
        }

        /*
         //for testing Observer and Observable
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
        */

        public void BuildBackground(string source)
        {
            int winW = Program.Window.Width;
            int winH = Program.Window.Height;

            GameObject bg;


            for (int i = -1; i < (winH / Globals.VeryBigImageSize * Globals.BigMultiplier); i++)
            {
                bg = _objects.CreateGameBackground(source, Globals.VeryBigImageSize * Globals.BigMultiplier, Globals.BigImageSize * Globals.BigMultiplier, 0, Globals.BigImageSize * Globals.BigMultiplier * i);
                bg.Active = true;
                bg.AddComponent(_physics.CreateBGComponent());

                for (int j = 0; j < winW / (Globals.BigImageSize * Globals.BigMultiplier); j++)
                {
                    bg.AddComponent(_rendering.CreateBGComponent(0, 0, Globals.VeryBigImageSize, Globals.BigImageSize, Globals.VeryBigImageSize * Globals.BigMultiplier, 64 * Globals.BigMultiplier, 128 * Globals.BigMultiplier * j));
                }

                bg.AddComponent(_ai.CreateComponent());
            }
        }

        public GameObject BuildPlayer()
        {
            Player = _objects.CreateGamePlayer("player", Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            Player.Active = true;
            Player.AddComponent(_physics.CreateComponent());
            Player.AddComponent(_rendering.CreateComponent(Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            Player.AddComponent(_rendering.CreateInfoComponent());
            Player.AddComponent(_controls.CreateComponent());
            Player.AddComponent(_collisions.CreateComponent());
            Player.AddComponent(_shootings.CreatePlayerComponent());

            Camera = new Camera(Player);
            return Player;
        }


        //Enemy
        public GameObject BuildShip(GameObject ship)
        {
            ship = _objects.CreateGameShip("ship", Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            ship.AddComponent(_physics.CreateComponent());
            ship.AddComponent(_rendering.CreateComponent(Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            ship.AddComponent(_ai.CreateComponent());
            ship.AddComponent(_collisions.CreateComponent());
            ship.AddComponent(_animations.CreateComponent());
            ship.AddComponent(_shootings.CreateEnemyComponent());

            return ship;

        }

        //Enemy
        public GameObject BuildUfo(GameObject ufo)
        {
            ufo = _objects.CreateGameUfo("ufo", Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            ufo.AddComponent(_physics.CreateComponent());
            ufo.AddComponent(_rendering.CreateComponent(Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            ufo.AddComponent(_ai.CreateComponent());
            ufo.AddComponent(_collisions.CreateComponent());
            ufo.AddComponent(_animations.CreateComponent());

            return ufo;

        }

        public GameObject BuildPlayerBullet(GameObject bullet, GameObject player)
        {
            bullet = _objects.CreatePlayerBullet("playerBullet", player, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            bullet.AddComponent(_physics.CreateComponent());
            bullet.AddComponent(_rendering.CreateComponent(Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            bullet.AddComponent(_ai.CreateComponent());
            bullet.AddComponent(_collisions.CreateComponent());
            return bullet;

        }

        // Enemy Bullet
        public GameObject BuildEnemyBullet(GameObject bullet, GameObject enemy)
        {
            bullet = _objects.CreateEnemyBullet("enemyBullet", enemy, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            bullet.AddComponent(_physics.CreateComponent());
            bullet.AddComponent(_rendering.CreateComponent(Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            bullet.AddComponent(_ai.CreateComponent());
            bullet.AddComponent(_collisions.CreateComponent());
            bullet.AddComponent(_animations.CreateComponent());
            return bullet;

        }

        // Blocks 
        public GameObject BuildBlocks(Block block)
        {
            block = _objects.CreateBlock("Block", 32, 32, (int)block.PosX, (int)block.PosY, block.Img, block.ImgChange);
            block.AddComponent(_rendering.CreateComponent(16 * 2, 16 * 2));
            block.AddComponent(_collisions.CreateComponent());
            block.Active = true;
            block.Died = false;
            return block;

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

        public GameObject RequestEnemyBullet(GameObject enemy)
        {
            return _pool.RequestEnemyBullet(enemy);
        }

        public void DespawnPlayerBullet(GameObject bullet)
        {
            _pool.DespawnPlayerBullet(bullet);
        }

        public void DespawnEnemyBullet(GameObject bullet)
        {
            _pool.DespawnEnemyBullet(bullet);
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

        public void Shoot(float deltaT)
        {
            _shootings.Shoot(deltaT);
        }

        public void UpdateCamera(Player player)
        {
            Camera.UpdateCamera(player);
        }






        //Game loop
        public void Run() {
            //BuildBackground("level 1");
            Player = (Player)BuildPlayer();
            tiledMap.load("image/MiniPixelPack3/Maps/Level1.json", "image/MiniPixelPack3/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png");
            tiledMap.build();
            _levels.Run();    
        }


 


        public void QuitGame()
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


            SDL.SDL_DestroyWindow(Program.Window.Show);
            _audio.CleanUp();
            SDL.SDL_Quit();
        }
    }
}
