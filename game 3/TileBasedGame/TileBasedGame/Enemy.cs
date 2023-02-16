
namespace TileBasedGame
{
    class Enemy:GameObject
    {
        public Image ExplodingImg = new Image();
        public Image FlyingImg = new Image();

        public Enemy(string name, int w, int h) : base(name, w, h)
        {           
            ExplodingImg = Program.Game.Loader.ExplodingImg;
            VelY = Globals.Velocity;
            VelX= Globals.Velocity;
            
        }

        public int ExplosionStep = 0;

        public void Explode()
        {
            
            if (ExplosionStep < 5)
            {
                //ImgChange = Globals.NormalImageSize * ExplosionStep;
            } else
            {
                ExplosionStep = 0;
                //Img = FlyingImg;
                Died = false;
                Program.Game.DespawnEnemy(this);
            }

            ExplosionStep++;            
        }
    }
}
