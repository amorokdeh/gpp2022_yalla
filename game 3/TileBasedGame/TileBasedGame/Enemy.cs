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
        }

        public int ExplosionStep = 0;

        public void Explode()
        {
            
            if (ExplosionStep < 5)
            {
                ImgChange = 16 * ExplosionStep;
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
