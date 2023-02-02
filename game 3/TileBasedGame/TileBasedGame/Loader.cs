using Newtonsoft.Json;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TileBasedGame
{
    class Loader
    {
        public Text Txt = new Text();
        //Images
        public Image playerImg = new Image();
        public Image shipImg = new Image();
        public Image ufoImg = new Image();
        public Image bulletImg = new Image();
        public Image level1Img = new Image();
        public Image level2Img = new Image();
        public Image level3Img = new Image();
        public Image enemyBulletImg = new Image();
        public Image explodingImg = new Image();
        public Image BackgroundImg = new Image();
        //Json
        string json;
        public Dictionary<string, object> level1Json;
        public Dictionary<string, object> level2Json;
        public Dictionary<string, object> level3Json;
        //number of assets to load
        int totalAssets = 13;
        int assetsLoaded = 0;

        public Loader() { } 

        public void loadAssets(int i) {
            switch (i)
            {
                case 0: playerImg.LoadImage("image/PlayerShip/player.png"); break;
                case 1: shipImg.LoadImage("image/Enemies/Owlet_Monster_Walk_6.png"); break;
                case 2: ufoImg.LoadImage("image/Enemies/Dude_Monster_Walk_6.png"); break;
                case 3: bulletImg.LoadImage("image/Projectiles/Player_charged_beam (16 x 16).png"); break;
                case 4: level1Img.LoadImage("image/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png"); break;
                case 5: level2Img.LoadImage("image/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png"); break;
                case 6: level3Img.LoadImage("image/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png"); break;
                case 7: enemyBulletImg.LoadImage("image/Projectiles/Bullet.png"); break;
                case 8: explodingImg.LoadImage("image/Effects/Explosion.png"); break;
                case 9: BackgroundImg.LoadImage("image/Background.png"); break;
                case 10: json = File.ReadAllText("image/Maps/Level1.json"); level1Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(json); break;
                case 11: json = File.ReadAllText("image/Maps/Level1.json"); level2Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(json); break;
                case 12: json = File.ReadAllText("image/Maps/Level1.json"); level3Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(json); break;
            }
        }


        public void load()
        {
            Txt.SetUp();
            Txt.LoadText(1);
            string text = "Loading";
            IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(Txt.Font, text, Txt.White);
            Txt.AddText(Program.Window.Renderer, surfaceMessage, Program.Window.Width / 2 - text.Length * 10, 500, text.Length * 20, 30);

            // loop through the assets and load each one
            for (int i = 0; i < totalAssets; i++)
            {
                // load the asset
                loadAssets(i);
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

                // render the changes
                SDL.SDL_RenderPresent(Program.Window.Renderer);
                SDL.SDL_Delay(200);
            }
        }
    }
}
