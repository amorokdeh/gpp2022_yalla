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
        public InfoBoxManager _infoBox = new InfoBoxManager();
        public Loader _loader = new Loader();
        public Cleaner _cleaner = new Cleaner();


        public static bool Quit;
        public Player Player;
        public Camera Camera;

        public Game()
        {

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

            Component coc = _collisions.CreateComponent("player");
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
        public GameObject BuildEnemy(GameObject enemy, int type)
        {
            Image img = new Image();
            switch (type) {
                case 1: img = _loader.enemy1Img; break;
                case 2: img = _loader.enemy2Img; break;
            }
            enemy = _objects.CreateGameEnemy("Enemy", Globals.MediumImageSize, Globals.BigImageSize, (int)enemy.PosX, (int)enemy.PosY);
            enemy.AddComponent(_physics.CreateComponent());
            enemy.AddComponent(_rendering.CreateComponent(img, Globals.MediumImageSize, Globals.BigImageSize, Globals.MediumImageSize, Globals.BigImageSize));
            enemy.AddComponent(_ai.CreateComponent());
            Component coc = _collisions.CreateComponent("bad");
            enemy.AddComponent(coc);
            enemy.AddComponent(_animations.CreateComponent());
            Component uc = _updates.CreateEnemyComponent();
            enemy.AddComponent(uc);
            enemy.Active = true;
            coc.AddObserver(uc);
            return enemy;

        }

        public GameObject BuildPlayerBullet(GameObject bullet, GameObject player)
        {
            bullet = _objects.CreatePlayerBullet("playerBullet", player, Globals.MediumImageSize, Globals.MediumImageSize);
            bullet.AddComponent(_physics.CreateComponent());
            bullet.AddComponent(_rendering.CreateComponent(_loader.bulletImg, Globals.MediumImageSize, Globals.MediumImageSize));
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
            bullet.AddComponent(_rendering.CreateComponent(_loader.bulletImg, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            bullet.AddComponent(_ai.CreateComponent());
            Component coc = _collisions.CreateComponent("bad");
            bullet.AddComponent(coc);
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
            Component coc = _collisions.CreateComponent("spike");
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
        //Coins
        public GameObject BuildCoin(Coin coin, int imgX, int imgY)
        {
            coin = _objects.CreateCoin("Coin", Globals.MediumImageSize, Globals.MediumImageSize, (int)coin.PosX, (int)coin.PosY);
            coin.AddComponent(_rendering.CreateComponent(_loader.coinImg, Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize, imgX, imgY));
            Component coc = _collisions.CreateComponent("coin");
            coin.AddComponent(coc);
            coin.Active = true;
            coin.Died = false;
            return coin;

        }
        //Coins
        public GameObject BuildPower(Power power)
        {
            power = _objects.CreatePower("Power", Globals.MediumImageSize, Globals.MediumImageSize, (int)power.PosX, (int)power.PosY);
            power.AddComponent(_rendering.CreateComponent(_loader.powerImg, Globals.MediumImageSize, Globals.MediumImageSize, Globals.NormalImageSize, Globals.NormalImageSize));
            Component coc = _collisions.CreateComponent("power");
            power.AddComponent(coc);
            power.AddComponent(_animations.CreateComponent());
            power.Active = true;
            power.Died = false;
            return power;

        }

        public void DespawnEnemy(GameObject enemy)
        {
            _pool.DespawnEnemy(enemy);
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
            Player = (Player) BuildPlayer();
            _loader.load();
            _levels.Run();
        }





        public void QuitGame(){
            SDL.SDL_DestroyWindow(Program.Window.Show);
            _audio.CleanUp();
            SDL.SDL_Quit();
        }
    }
}
