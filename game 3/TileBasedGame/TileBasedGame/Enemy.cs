using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Enemy:GameObject
    {
        public Image ExplodingImg = new Image();
        public Image FlyingImg = new Image();

        public Enemy(string name, int w, int h) : base(name, w, h)
        {
            ExplodingImg.LoadImage("image/MiniPixelPack3/Effects/Explosion.png");
            VelY = 100;
            Program.Game._maps.currentMap.tiles.Add(this);
        }

        public int ExplosionStep = 0;

        public void Explode()
        {
            
            if (ExplosionStep < 5)
            {
                ImgChange = Globals.NormalImageSize * ExplosionStep;
            } else
            {
                ExplosionStep = 0;
                Img = FlyingImg;
                Died = false;
                Program.Game.DespawnEnemy(this);
            }

            ExplosionStep++;
            
        }
    }
}
