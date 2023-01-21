using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Player:GameObject
    {
        public int Lives = 100;
        public int Score = 0;
        public Player(string name, int w, int h): base(name, w, h)
        {
            ImgChange = 16;
            Img.LoadImage("image/MiniPixelPack3/PlayerShip/PlayerShip.png");
            PosX = Program.Window.Width / 2;
            PosY = Program.Window.Height / 2;
        }

        public void FlyLeft()
        {
            ImgChange = 0;
        }
        public void FlyStraight()
        {
            ImgChange = Globals.NormalImageSize;
        }
        public void FlyRight()
        {
            ImgChange = Globals.NormalImageSize*2;
        }

        public void Reset()
        {
            Lives = 100;
            Score = 0;
            PosX = Program.Window.Width/2;
            PosY = Program.Window.Height/2;
            VelX = Globals.Velocity;
            VelY = Globals.Velocity;
            CurrentVelX = Globals.Reset;
            CurrentVelY = Globals.Reset;
        }
    }
}
