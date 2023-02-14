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
        public GameObjectManager Objects = new GameObjectManager();
        public PhysicsManager Physics = new PhysicsManager();
        public RenderingManager Rendering = new RenderingManager();
        public AudioManager Audio = new AudioManager();
        public LevelManager Levels = new LevelManager();
        public CollisionManager Collisions = new CollisionManager();
        public AIManager Ai = new AIManager();
        public ControlManager Controls = new ControlManager();
        public PoolManager Pool = new PoolManager();
        public ShootingManager Shootings = new ShootingManager();
        public MapManager Maps = new MapManager();
        public UpdateManager Updates = new UpdateManager();
        public InfoBoxManager InfoBox = new InfoBoxManager();
        public Loader Loader = new Loader();
        public Cleaner Cleaner = new Cleaner();


        public static bool Quit;
        public Player Player;
        public Camera Camera;

        public Game()
        {

        }

        public GameObject BuildPlayer()
        {
            Player = Objects.CreateGamePlayer("player", Globals.MediumImageSize, Globals.BigImageSize);
            Player.Active = true;
            Component pc = Physics.CreateComponent();
            Player.AddComponent(pc);

            Player.AddComponent(Rendering.CreateComponent(Loader.PlayerImg, Globals.MediumImageSize, Globals.BigImageSize, Globals.MediumImageSize, Globals.BigImageSize));
            Player.AddComponent(Rendering.CreateInfoComponent());

            Component cc = Controls.CreateComponent();
            Player.AddComponent(cc);

            Component coc = Collisions.CreateComponent("player");
            Player.AddComponent(coc);

            Player.AddComponent(Shootings.CreatePlayerComponent());

            Component uc = Updates.CreatePlayerComponent();
            Player.AddComponent(uc);

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
                case 1: img = Loader.Enemy1Img; break;
                case 2: img = Loader.Enemy2Img; break;
            }
            enemy = Objects.CreateGameEnemy("Enemy", Globals.MediumImageSize, Globals.BigImageSize, (int)enemy.PosX, (int)enemy.PosY);
            enemy.AddComponent(Physics.CreateComponent());
            enemy.AddComponent(Rendering.CreateComponent(img, Globals.MediumImageSize, Globals.BigImageSize, Globals.MediumImageSize, Globals.BigImageSize));
            enemy.AddComponent(Ai.CreateComponent());
            Component coc = Collisions.CreateComponent("bad");
            enemy.AddComponent(coc);
            Component uc = Updates.CreateEnemyComponent();
            enemy.AddComponent(uc);
            enemy.Active = true;
            coc.AddObserver(uc);
            return enemy;

        }

        public GameObject BuildPlayerBullet(GameObject bullet, GameObject player)
        {
            bullet = Objects.CreatePlayerBullet("playerBullet", player, Globals.MediumImageSize, Globals.MediumImageSize);
            bullet.AddComponent(Physics.CreateComponent());
            bullet.AddComponent(Rendering.CreateComponent(Loader.BulletImg, Globals.MediumImageSize, Globals.MediumImageSize));
            bullet.AddComponent(Ai.CreateComponent());
            Component coc = Collisions.CreateComponent("good");
            bullet.AddComponent(coc);
            Component uc = Updates.CreateBulletComponent();
            bullet.AddComponent(uc);
            coc.AddObserver(uc);
            return bullet;
        }

        // Enemy Bullet
        public GameObject BuildEnemyBullet(GameObject bullet, GameObject enemy)
        {
            bullet = Objects.CreateEnemyBullet("enemyBullet", enemy, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier);
            bullet.AddComponent(Physics.CreateComponent());
            bullet.AddComponent(Rendering.CreateComponent(Loader.BulletImg, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            bullet.AddComponent(Ai.CreateComponent());
            Component coc = Collisions.CreateComponent("bad");
            bullet.AddComponent(coc);
            Component uc = Updates.CreateBulletComponent();
            bullet.AddComponent(uc);
            coc.AddObserver(uc);
            return bullet;

        }

        // Background tiles
        public GameObject BuildTiles(string name, int w, int h, int x, int y, int imgFrame, Image img)
        {
            Tile tile = Objects.CreateTile(name, w, h, x, y);
            tile.AddComponent(Rendering.CreateComponent(img, imgFrame, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            tile.Active = true;
            tile.Died = false;
            return tile;

        }

        // Blocks tiles
        public GameObject BuildBlocks(string name, int w, int h, int x, int y, int imgFrame, Image img)
        {
            Block block = Objects.CreateBlock(name, w, h, x, y);
            block.AddComponent(Rendering.CreateComponent(img, imgFrame, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            Component coc = Collisions.CreateComponent("block");
            block.AddComponent(coc);
            block.Active = true;
            block.Died = false;
            return block;

        }

        // Spikes tiles
        public GameObject BuildSpikes(string name, int w, int h, int x, int y, int imgFrame, Image img)
        {
            Spike spike = Objects.CreateSpike(name, w, h, x, y);
            spike.AddComponent(Rendering.CreateComponent(img, imgFrame, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            Component coc = Collisions.CreateComponent("spike");
            spike.AddComponent(coc);
            spike.Active = true;
            spike.Died = false;
            return spike;

        }
        
        // End door tiles
        public GameObject BuildEndDoor(string name, int w, int h, int x, int y, int imgFrame, Image img)
        {
            EndDoor endDoor = Objects.CreateEndDoor(name, w, h, x, y);
            endDoor.AddComponent(Rendering.CreateComponent(img, imgFrame, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier, Globals.NormalImageSize * Globals.Multiplier));
            Component coc = Collisions.CreateComponent("EndDoor");
            endDoor.AddComponent(coc);
            endDoor.Active = true;
            endDoor.Died = false;
            return endDoor;

        }
        //Coins
        public GameObject BuildCoin(Coin coin, int imgX, int imgY)
        {
            coin = Objects.CreateCoin("Coin", Globals.MediumImageSize, Globals.MediumImageSize, (int)coin.PosX, (int)coin.PosY);
            coin.AddComponent(Rendering.CreateComponent(Loader.CoinImg, Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize, Globals.MediumImageSize, imgX, imgY));
            Component coc = Collisions.CreateComponent("coin");
            coin.AddComponent(coc);
            coin.Active = true;
            coin.Died = false;
            return coin;

        }
        //Coins
        public GameObject BuildPower(Power power)
        {
            power = Objects.CreatePower("Power", Globals.MediumImageSize, Globals.MediumImageSize, (int)power.PosX, (int)power.PosY);
            power.AddComponent(Rendering.CreateComponent(Loader.PowerImg, Globals.MediumImageSize, Globals.MediumImageSize, Globals.NormalImageSize, Globals.NormalImageSize));
            Component coc = Collisions.CreateComponent("power");
            power.AddComponent(coc);
            power.Active = true;
            power.Died = false;
            return power;

        }

        public void DespawnEnemy(GameObject enemy)
        {
            Pool.DespawnEnemy(enemy);
        }

        public GameObject RequestPlayerBullet(GameObject player)
        {
            return Pool.RequestPlayerBullet(player);
        }

        public GameObject RequestEnemyBullet(GameObject enemy)
        {
            return Pool.RequestEnemyBullet(enemy);
        }

        public void DespawnPlayerBullet(GameObject bullet)
        {
            Pool.DespawnPlayerBullet(bullet);
        }

        public void DespawnEnemyBullet(GameObject bullet)
        {
            Pool.DespawnEnemyBullet(bullet);
        }

        public void DespawnBullet(GameObject bullet)
        {
            Pool.DespawnBullet(bullet);
        }


        public void ControlPlayer()
        {
            Controls.Control();
        }
        public void ControlEnemy()
        {
            Ai.Control();
        }


        public void Move(float deltaT)
        {
            Physics.Move(deltaT);
        }

        public void Collide(float deltaT)
        {
            Collisions.Collide(deltaT);
        }


        public void Render()
        {
            Rendering.Render();
        }

        public void SetInactive()
        {
            Pool.SetInactive();

        }


        public void Shoot(float deltaT)
        {
            Shootings.Shoot(deltaT);
        }

        public void UpdateCamera(Player player, float deltaT)
        {
            Camera.UpdateCamera(player, deltaT);
        }

        public void DoUpdate()
        {
            Updates.DoUpdate();
        }


        //Game loop
        public void Run() {
            Loader.Load();
            Player = (Player)BuildPlayer();
            //Player.CharData = Loader.PlayerAnimation;
            Levels.Run();
        }





        public void QuitGame(){
            SDL.SDL_DestroyWindow(Program.Window.Show);
            Audio.CleanUp();
            SDL.SDL_Quit();
        }
    }
}
