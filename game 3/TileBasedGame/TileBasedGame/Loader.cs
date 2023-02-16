using Newtonsoft.Json;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;

namespace TileBasedGame
{
    class Loader
    {
        //Number of assets to load
        private int _totalAssets = 33;
        private int _assetsLoaded = 0;

        public Text Txt = new Text();

        //Images
        public Image PlayerImg = new Image();
        public Image Enemy1Img = new Image();
        public Image Enemy2Img = new Image();
        public Image BulletImg = new Image();
        public Image CoinImg = new Image();
        public Image PowerImg = new Image();
        public Image Level1Img = new Image();
        public Image Level2Img = new Image();
        public Image Level3Img = new Image();
        public Image ExplodingImg = new Image();
        public Image BackgroundImg = new Image();
        //Json
        public string Json;
        public string Src;
        public Dictionary<string, object> Level1Json;
        public Dictionary<string, object> Level2Json;
        public Dictionary<string, object> Level3Json;
        //Sounds
        public AudioComponent MenuButtons = new AudioComponent();
        public AudioComponent MenuClick = new AudioComponent();
        public AudioComponent GameOver = new AudioComponent();
        public AudioComponent Shooting = new AudioComponent();
        public AudioComponent ReloadShooting = new AudioComponent();
        public AudioComponent ExplodEnemy = new AudioComponent();
        public AudioComponent ExplodPlayer = new AudioComponent();
        public AudioComponent ShootingEnemy = new AudioComponent();
        // music effects
        public AudioComponent MenuMusic = new AudioComponent();
        public AudioComponent Level1Music = new AudioComponent();
        public AudioComponent Level2Music = new AudioComponent();
        public AudioComponent Level3Music = new AudioComponent();
        public AudioComponent Jump = new AudioComponent();
        public AudioComponent Coin = new AudioComponent();
        public AudioComponent Power = new AudioComponent();
        public AudioComponent Win = new AudioComponent();
        // animation
        public CharacterConfig PlayerConfig;
        public CharacterConfig EnemyConfig;

        public Loader() {} 

        public string loadAssets(int i) {
            switch (i)
            {
                case 0:  return LoadImage(PlayerImg, "image/PlayerShip/player.png");
                case 1:  return LoadImage(Enemy1Img, "image/Enemies/Owlet_Monster_Walk_6.png");
                case 2:  return LoadImage(Enemy2Img, "image/Enemies/Dude_Monster_Walk_6.png");
                case 3:  return LoadImage(BulletImg, "image/Projectiles/Bullet.png");
                case 4:  return LoadImage(CoinImg, "image/Items/Coin.png");
                case 5:  return LoadImage(PowerImg, "image/Items/Power.png");
                case 6:  return LoadImage(Level1Img, "image/Maps/Level1.png");
                case 7:  return LoadImage(Level2Img, "image/Maps/Level2.png");
                case 8:  return LoadImage(Level3Img, "image/Maps/Level3.png");
                case 9:  return LoadImage(ExplodingImg, "image/Effects/Explosion.png");
                case 10: return LoadImage(BackgroundImg, "image/Background.png");

                case 11: return LoadCharacter(ref PlayerConfig, "animation/playerAnimation.json");
                case 12: return LoadCharacter(ref EnemyConfig, "animation/enemyAnimation.json");
                //case 13: return LoadCharacter(ref PlayerConfig, "animation/playerAnimation.json");

                case 14: return LoadMusic(MenuMusic, "sound/MainMenu Music.wav");
                case 15: return LoadMusic(Level1Music, "sound/Level1 Music.wav");
                case 16: return LoadMusic(Level2Music, "sound/Level2 Music.wav");
                case 17: return LoadMusic(Level3Music, "sound/Level3 Music.wav");

                case 18: return LoadSound(MenuButtons, "sound/Menu buttons.wav");
                case 19: return LoadSound(MenuClick, "sound/Menu click.wav");
                case 20: return LoadSound(GameOver, "sound/game_over.wav");
                case 21: return LoadSound(Shooting, "sound/shooting.wav");
                case 22: return LoadSound(ExplodEnemy, "sound/Death.wav");
                case 23: return LoadSound(ExplodPlayer, "sound/explod_1.wav");
                case 24: return LoadSound(ShootingEnemy, "sound/lasergun.wav");
                case 25: return LoadSound(Jump, "sound/Jump.wav");
                case 26: return LoadSound(Coin, "sound/Coin.wav");
                case 27: return LoadSound(Power, "sound/Power.wav");
                case 28: return LoadSound(ReloadShooting, "sound/reload.wav");
                case 29: return LoadSound(Win, "sound/Win.wav");

                case 30: return LoadLevel(Level1Json = LoadMap("image/Maps/Level1.json"));
                case 31: return LoadLevel(Level2Json = LoadMap("image/Maps/Level2.json"));
                case 32: return LoadLevel(Level3Json = LoadMap("image/Maps/Level3.json"));

                default: return "";
            }
        }

        public string LoadCharacter(ref CharacterConfig config, string src)
        {
            string file = File.ReadAllText(src);
            config = CharacterConfig.Load(file);
            return src;
        }

        public string LoadImage(Image img, string src) { 
            img.LoadImage(src);
            return src;
        }
        public string LoadLevel(Dictionary<string, object> map) {
            return Src;
        }

        public Dictionary<string, object> LoadMap(string src) {
            this.Src = src;
            Json = File.ReadAllText(src);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(Json);
        }
        public string LoadMusic(AudioComponent music, string src)
        {
            music.LoadMusic(src);
            return src;
        }
        public string LoadSound(AudioComponent sound, string src)
        {
            sound.LoadSound(src);
            return src;
        }
        public void PrintTxt(string txt, int y, int size) {
            IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(Txt.Font, txt, Txt.White);
            Txt.AddText(Program.Window.Renderer, surfaceMessage, Program.Window.Width / 2 - txt.Length * 10, y , txt.Length * 20, size);
        }
        public void Load()
        {
            Txt.SetUp();
            Txt.LoadText(1);

            // loop through the assets and load each one
            for (int i = 0; i < _totalAssets; i++)
            {
                SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 0, 0, 0, 0);
                SDL.SDL_RenderPresent(Program.Window.Renderer);
                SDL.SDL_RenderClear(Program.Window.Renderer);
                PrintTxt("Loading", 500, 30);

                // load the asset
                string loadState = loadAssets(i);
                PrintTxt(loadState, 550, 25);
                _assetsLoaded++;

                // update the loading bar with the current progress
                int percentLoaded = (int)((float)_assetsLoaded / _totalAssets * 100);

                // draw the background rectangle
                SDL.SDL_Rect backgroundRect = new SDL.SDL_Rect
                {
                    x = Program.Window.Width * 10 / 100,
                    y = Program.Window.Height * 80 / 100,
                    w = Program.Window.Width - Program.Window.Width * 20 / 100,
                    h = 50
                };
                SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 255, 255, 255, 255);
                SDL.SDL_RenderFillRect(Program.Window.Renderer, ref backgroundRect);

                // draw the loading bar rectangle
                SDL.SDL_Rect loadingBarRect = new SDL.SDL_Rect
                {
                    x = Program.Window.Width * 10 / 100 + 5,
                    y = Program.Window.Height * 80 / 100 + 5,
                    w = (int)(percentLoaded / 100.0f * (Program.Window.Width - Program.Window.Width * 20 / 100)) - 10,
                    h = 50 - 10
                };
                SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 0, 255, 0, 255);
                SDL.SDL_RenderFillRect(Program.Window.Renderer, ref loadingBarRect);

                SDL.SDL_Delay(200);
            }
            SetAudio();
        }
        public void SetAudio() {
            Program.Game.Audio.AddAudio(MenuButtons);
            Program.Game.Audio.AddAudio(MenuClick);
            Program.Game.Audio.AddAudio(GameOver);
            Program.Game.Audio.AddAudio(Shooting);
            Program.Game.Audio.AddAudio(ExplodEnemy);
            Program.Game.Audio.AddAudio(ExplodPlayer);
            Program.Game.Audio.AddAudio(ShootingEnemy);
            Program.Game.Audio.AddAudio(MenuMusic);
            Program.Game.Audio.AddAudio(Level1Music);
            Program.Game.Audio.AddAudio(Level2Music);
            Program.Game.Audio.AddAudio(Level3Music);
            Program.Game.Audio.AddAudio(Jump);
            Program.Game.Audio.AddAudio(Coin);
            Program.Game.Audio.AddAudio(Power);
            Program.Game.Audio.AddAudio(ReloadShooting);
        }
    }
}
