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
        public Loader loader = new Loader();
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
            Component pc = _physics.CreatePlayerComponent();
            Player.AddComponent(pc);

            Player.AddComponent(_rendering.CreateComponent(Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            Player.AddComponent(_rendering.CreateInfoComponent());

            Component cc = _controls.CreateComponent();
            Player.AddComponent(cc);

            Component coc = _collisions.CreateComponent("good");
            Player.AddComponent(coc);

            Player.AddComponent(_shootings.CreatePlayerComponent());

            Component uc = _updates.CreatePlayerComponent();
            Player.AddComponent(uc);

            //cc.AddObserver(pc);
            coc.AddObserver(uc);


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
            ufo.AddComponent(_rendering.CreateComponent(Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            ufo.AddComponent(_ai.CreateComponent());
            Component coc = _collisions.CreateComponent("bad");
            ufo.AddComponent(coc);
            ufo.AddComponent(_animations.CreateComponent());
            Component uc = _updates.CreateEnemyComponent();
            ufo.AddComponent(uc);

            coc.AddObserver(uc);

            return ufo;

        }

        public GameObject BuildPlayerBullet(GameObject bullet, GameObject player)
        {
            bullet = _objects.CreatePlayerBullet("playerBullet", player, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            bullet.AddComponent(_physics.CreateComponent());
            bullet.AddComponent(_rendering.CreateComponent(Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
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
            bullet.AddComponent(_rendering.CreateComponent(Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
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
        public GameObject BuildTiles(Tile tile)
        {
            tile = _objects.CreateTile("Tile", Globals.MediumImageSize, Globals.MediumImageSize, (int)tile.PosX, (int)tile.PosY, tile.Img, tile.imgFrame);
            tile.AddComponent(_rendering.CreateComponent(Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize));
            tile.Active = true;
            tile.Died = false;
            return tile;

        }

        // Blocks tiles
        public GameObject BuildBlocks(Block block)
        {
            block = _objects.CreateBlock("Block", Globals.MediumImageSize, Globals.MediumImageSize, (int)block.PosX, (int)block.PosY, block.Img, block.imgFrame);
            block.AddComponent(_rendering.CreateComponent(Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize));
            Component coc = _collisions.CreateComponent("block");
            block.AddComponent(coc);
            block.Active = true;
            block.Died = false;
            return block;

        }

        // Spikes tiles
        public GameObject BuildSpikes(Spike spike)
        {
            spike = _objects.CreateSpike("Spike", Globals.MediumImageSize, Globals.MediumImageSize, (int)spike.PosX, (int)spike.PosY, spike.Img, spike.imgFrame);
            spike.AddComponent(_rendering.CreateComponent(Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize));
            Component coc = _collisions.CreateComponent("bad");
            spike.AddComponent(coc);
            spike.Active = true;
            spike.Died = false;
            return spike;

        }
        
        // End door tiles
        public GameObject BuildEndDoor(EndDoor endDoor)
        {
            endDoor = _objects.CreateEndDoor("End door", Globals.MediumImageSize, Globals.MediumImageSize, (int)endDoor.PosX, (int)endDoor.PosY, endDoor.Img, endDoor.imgFrame);
            endDoor.AddComponent(_rendering.CreateComponent(Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize));
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
            loader.loadImages();
            loader.loadJson();
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
