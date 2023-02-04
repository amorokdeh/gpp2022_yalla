using Newtonsoft.Json;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TileBasedGame
{
    class Loader
    {
        //Number of assets to load
        private int totalAssets = 28;
        private int assetsLoaded = 0;

        public Text Txt = new Text();

        //Images
        public Image playerImg = new Image();
        public Image enemy1Img = new Image();
        public Image enemy2Img = new Image();
        public Image bulletImg = new Image();
        public Image coinImg = new Image();
        public Image powerImg = new Image();
        public Image level1Img = new Image();
        public Image level2Img = new Image();
        public Image level3Img = new Image();
        public Image explodingImg = new Image();
        public Image BackgroundImg = new Image();
        //Json
        public string json;
        public string src;
        public Dictionary<string, object> level1Json;
        public Dictionary<string, object> level2Json;
        public Dictionary<string, object> level3Json;
        //Sounds
        public AudioComponent MenuButtons = new AudioComponent();
        public AudioComponent MenuClick = new AudioComponent();
        public AudioComponent GameOver = new AudioComponent();
        public AudioComponent Shooting = new AudioComponent();
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

        public Loader() {

        } 

        public string loadAssets(int i) {
            switch (i)
            {
                case 0:  return loadImage(playerImg, "image/PlayerShip/player.png");
                case 1:  return loadImage(enemy1Img, "image/Enemies/Owlet_Monster_Walk_6.png");
                case 2:  return loadImage(enemy2Img, "image/Enemies/Dude_Monster_Walk_6.png");
                case 3:  return loadImage(bulletImg, "image/Projectiles/Bullet.png");
                case 4:  return loadImage(coinImg, "image/Items/Coin.png");
                case 5:  return loadImage(powerImg, "image/Items/Power.png");
                case 6:  return loadImage(level1Img, "image/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png");
                case 7:  return loadImage(level2Img, "image/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png");
                case 8:  return loadImage(level3Img, "image/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png");
                case 9:  return loadImage(explodingImg, "image/Effects/Explosion.png");
                case 10: return loadImage(BackgroundImg, "image/Background.png");

                case 11: return loadLevel(level1Json = loadMap("image/Maps/Level1.json"));
                case 12: return loadLevel(level2Json = loadMap("image/Maps/Level1.json"));
                case 13: return loadLevel(level3Json = loadMap("image/Maps/Level1.json"));

                case 14: return loadMusic(MenuMusic, "sound/MainMenu Music.wav");
                case 15: return loadMusic(Level1Music, "sound/Level1 Music.wav");
                case 16: return loadMusic(Level2Music, "sound/Level2 Music.wav");
                case 17: return loadMusic(Level3Music, "sound/Level3 Music.wav");

                case 18: return loadSound(MenuButtons, "sound/Menu buttons.wav");
                case 19: return loadSound(MenuClick, "sound/Menu click.wav");
                case 20: return loadSound(GameOver, "sound/game_over.wav");
                case 21: return loadSound(Shooting, "sound/shooting.wav");
                case 22: return loadSound(ExplodEnemy, "sound/Death.wav");
                case 23: return loadSound(ExplodPlayer, "sound/explod_1.wav");
                case 24: return loadSound(ShootingEnemy, "sound/lasergun.wav");
                case 25: return loadSound(Jump, "sound/Jump.wav");
                case 26: return loadSound(Coin, "sound/Coin.wav");
                case 27: return loadSound(Power, "sound/Power.wav");
                default: return "";
            }
        }

        public string loadImage(Image img, string src) { 
            img.LoadImage(src);
            return src;
        }
        public string loadLevel(Dictionary<string, object> map) {
            return src;
        }

        public Dictionary<string, object> loadMap(string src) {
            this.src = src;
            json = File.ReadAllText(src);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
        public string loadMusic(AudioComponent music, string src)
        {
            music.LoadMusic(src);
            return src;
        }
        public string loadSound(AudioComponent sound, string src)
        {
            sound.LoadSound(src);
            return src;
        }
        public void printTxt(string txt, int y, int size) {
            IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(Txt.Font, txt, Txt.White);
            Txt.AddText(Program.Window.Renderer, surfaceMessage, Program.Window.Width / 2 - txt.Length * 10, y , txt.Length * 20, size);
        }
        public void load()
        {
            Txt.SetUp();
            Txt.LoadText(1);

            // loop through the assets and load each one
            for (int i = 0; i < totalAssets; i++)
            {
                SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 0, 0, 0, 0);
                SDL.SDL_RenderPresent(Program.Window.Renderer);
                SDL.SDL_RenderClear(Program.Window.Renderer);
                printTxt("Loading", 500, 30);

                // load the asset
                string loadState = loadAssets(i);
                printTxt(loadState, 550, 25);
                assetsLoaded++;

                // update the loading bar with the current progress
                int percentLoaded = (int)((float)assetsLoaded / totalAssets * 100);

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
                    x = Program.Window.Width * 10 / 100,
                    y = Program.Window.Height * 80 / 100,
                    w = (int)(percentLoaded / 100.0f * (Program.Window.Width - Program.Window.Width * 20 / 100)),
                    h = 50
                };
                SDL.SDL_SetRenderDrawColor(Program.Window.Renderer, 0, 255, 0, 255);
                SDL.SDL_RenderFillRect(Program.Window.Renderer, ref loadingBarRect);

                SDL.SDL_Delay(200);
            }
            setAudio();
        }
        public void setAudio() {
            Program.Game._audio.AddAudio(MenuButtons);
            Program.Game._audio.AddAudio(MenuClick);
            Program.Game._audio.AddAudio(GameOver);
            Program.Game._audio.AddAudio(Shooting);
            Program.Game._audio.AddAudio(ExplodEnemy);
            Program.Game._audio.AddAudio(ExplodPlayer);
            Program.Game._audio.AddAudio(ShootingEnemy);
            Program.Game._audio.AddAudio(MenuMusic);
            Program.Game._audio.AddAudio(Level1Music);
            Program.Game._audio.AddAudio(Level2Music);
            Program.Game._audio.AddAudio(Level3Music);
            Program.Game._audio.AddAudio(Jump);
            Program.Game._audio.AddAudio(Coin);
            Program.Game._audio.AddAudio(Power);
        }
    }
}
