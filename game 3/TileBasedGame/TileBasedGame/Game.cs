using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Game
    {
        public GameObjectManager _objects = new GameObjectManager();
        public PhysicsManager _physics = new PhysicsManager();
        public RenderingManager _rendering = new RenderingManager();
        public AudioManager _audio = new AudioManager();
        public LevelManager _levels = new LevelManager();
        public CollisionManager _collisions = new CollisionManager();
        public AIManager _ai = new AIManager();
        public ControlManager _controls = new ControlManager();
        public AnimationManager _animations = new AnimationManager();
        public PoolManager _pool = new PoolManager();
        public ShootingManager _shootings = new ShootingManager();
        public MapManager _maps = new MapManager();
        public UpdateManager _updates = new UpdateManager();
        public Loader _loader = new Loader();
        public Cleaner _cleaner = new Cleaner();


        public bool BulletReloadable = false;
        public static bool Quit;
        public uint Ticks = SDL.SDL_GetTicks();
        public Player Player;
        public Camera Camera;

        public Game()
        {

        }





        public void BuildBackground(string source)
        {
            /*
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
            }*/
        }

        public GameObject BuildPlayer()
        {
            Player = _objects.CreateGamePlayer("player", Globals.MediumImageSize, Globals.BigImageSize);
            Player.Active = true;
            Component pc = _physics.CreatePlayerComponent();
            Player.AddComponent(pc);

            Player.AddComponent(_rendering.CreateComponent(_loader.playerImg, Globals.MediumImageSize, Globals.BigImageSize, Globals.MediumImageSize, Globals.BigImageSize));
            Player.AddComponent(_rendering.CreateInfoComponent());

            Component cc = _controls.CreateComponent();
            Player.AddComponent(cc);

            Component coc = _collisions.CreateComponent("good");
            Player.AddComponent(coc);

            Player.AddComponent(_shootings.CreatePlayerComponent());

            Component uc = _updates.CreatePlayerComponent();
            Player.AddComponent(uc);

            Player.AddComponent(_animations.CreateComponent());

            //cc.AddObserver(pc);
            coc.AddObserver(uc);


            Camera = new Camera(Player);
            return Player;
        }


        //Enemy
        public GameObject BuildShip(GameObject ship)
        {
            ship = _objects.CreateGameShip("ship", Globals.MediumImageSize, Globals.BigImageSize);
            ship.AddComponent(_physics.CreateComponent());
            ship.AddComponent(_rendering.CreateComponent(_loader.shipImg, Globals.MediumImageSize, Globals.BigImageSize, Globals.MediumImageSize, Globals.BigImageSize));
            ship.AddComponent(_ai.CreateComponent());
            Component coc = _collisions.CreateComponent("bad");
            ship.AddComponent(coc);
            ship.AddComponent(_animations.CreateComponent());
            ship.AddComponent(_shootings.CreateEnemyComponent());
            Component uc = _updates.CreateEnemyComponent();
            ship.AddComponent(uc);

            coc.AddObserver(uc);

            return ship;

        }

        //Enemy
        public GameObject BuildUfo(GameObject ufo)
        {
            ufo = _objects.CreateGameUfo("ufo", Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            ufo.AddComponent(_physics.CreateComponent());
            Component rc = _rendering.CreateComponent(_loader.ufoImg, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            ufo.AddComponent(rc);
            ufo.AddComponent(_ai.CreateComponent());
            Component coc = _collisions.CreateComponent("bad");
            ufo.AddComponent(coc);
            Component ac = _animations.CreateComponent();
            ufo.AddComponent(ac);
            Component uc = _updates.CreateEnemyComponent();
            ufo.AddComponent(uc);

            coc.AddObserver(uc);
            ac.AddObserver(rc);

            return ufo;

        }

        public GameObject BuildPlayerBullet(GameObject bullet, GameObject player)
        {
            bullet = _objects.CreatePlayerBullet("playerBullet", player, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            bullet.AddComponent(_physics.CreateComponent());
            bullet.AddComponent(_rendering.CreateComponent(_loader.bulletImg, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            bullet.AddComponent(_ai.CreateComponent());
            Component coc = _collisions.CreateComponent("good");
            bullet.AddComponent(coc);
            Component uc = _updates.CreateBulletComponent();
            bullet.AddComponent(uc);

            coc.AddObserver(uc);
            return bullet;
        }

        // Enemy Bullet
        public GameObject BuildEnemyBullet(GameObject bullet, GameObject enemy)
        {
            bullet = _objects.CreateEnemyBullet("enemyBullet", enemy, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            bullet.AddComponent(_physics.CreateComponent());
            bullet.AddComponent(_rendering.CreateComponent(_loader.enemyBulletImg, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            bullet.AddComponent(_ai.CreateComponent());
            Component coc = _collisions.CreateComponent("bad");
            bullet.AddComponent(coc);
            bullet.AddComponent(_animations.CreateComponent());
            Component uc = _updates.CreateBulletComponent();
            bullet.AddComponent(uc);

            coc.AddObserver(uc);
            return bullet;

        }

        // Background tiles
        public GameObject BuildTiles(string name, int w, int h, int x, int y, int imgFrame, Image img)
        {
            Tile tile = _objects.CreateTile(name, w, h, x, y);
            tile.AddComponent(_rendering.CreateComponent(img, imgFrame, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            tile.Active = true;
            tile.Died = false;
            return tile;

        }

        // Blocks tiles
        public GameObject BuildBlocks(string name, int w, int h, int x, int y, int imgFrame, Image img)
        {
            Block block = _objects.CreateBlock(name, w, h, x, y);
            block.AddComponent(_rendering.CreateComponent(img, imgFrame, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            Component coc = _collisions.CreateComponent("block");
            block.AddComponent(coc);
            block.Active = true;
            block.Died = false;
            return block;

        }

        // Spikes tiles
        public GameObject BuildSpikes(string name, int w, int h, int x, int y, int imgFrame, Image img)
        {
            Spike spike = _objects.CreateSpike(name, w, h, x, y);
            spike.AddComponent(_rendering.CreateComponent(img, imgFrame, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            Component coc = _collisions.CreateComponent("bad");
            spike.AddComponent(coc);
            spike.Active = true;
            spike.Died = false;
            return spike;

        }
        
        // End door tiles
        public GameObject BuildEndDoor(string name, int w, int h, int x, int y, int imgFrame, Image img)
        {
            EndDoor endDoor = _objects.CreateEndDoor(name, w, h, x, y);
            endDoor.AddComponent(_rendering.CreateComponent(img, imgFrame, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            Component coc = _collisions.CreateComponent("door");
            endDoor.AddComponent(coc);
            endDoor.Active = true;
            endDoor.Died = false;
            return endDoor;

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

        public void DespawnBullet(GameObject bullet)
        {
            _pool.DespawnBullet(bullet);
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

        public void DoUpdate()
        {
            _updates.DoUpdate();
        }


        //Game loop
        public void Run() {
            //BuildBackground("level 1");
            Player = (Player) BuildPlayer();
            //mapManager.loadMap("Level 1");
            //mapManager.createMap();
            _loader.loadImages();
            _loader.loadJson();
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
