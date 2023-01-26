using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Loader
    {
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

        //Json
        string json;
        public Dictionary<string, object> level1Json;
        public Dictionary<string, object> level2Json;
        public Dictionary<string, object> level3Json;

        public Loader() { } 

        public void loadImages() {

            playerImg.LoadImage("image/MiniPixelPack3/PlayerShip/player.png");
            shipImg.LoadImage("image/MiniPixelPack3/Enemies/Lips.png");
            ufoImg.LoadImage("image/MiniPixelPack3/Enemies/Alan.png");
            bulletImg.LoadImage("image/MiniPixelPack3/Projectiles/PlayerBeam.png");
            level1Img.LoadImage("image/MiniPixelPack3/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png");
            level2Img.LoadImage("image/MiniPixelPack3/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png");
            level3Img.LoadImage("image/MiniPixelPack3/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png");
            enemyBulletImg.LoadImage("image/MiniPixelPack3/Projectiles/enemyBullet.png");
            explodingImg.LoadImage("image/MiniPixelPack3/Effects/Explosion.png");
            
        }

        public void loadJson(){

            json = File.ReadAllText("image/MiniPixelPack3/Maps/Level1.json");
            level1Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            json = File.ReadAllText("image/MiniPixelPack3/Maps/Level1.json");
            level2Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            json = File.ReadAllText("image/MiniPixelPack3/Maps/Level1.json");
            level3Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
    }
}
