using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Player:GameObject
    {
        public int Lives = Globals.Lives;
        public int Score = 0;
        public int fixImageX = 8;
        public int fixImageY = 12;

        public Player(string name, int w, int h): base(name, w, h)
        {
            ImgChange = 0;
            Img = Program.Game.loader.playerImg;
            Program.Game._maps.currentMap.tiles.Add(this);
        }

        public void FlyLeft()
        {
            ImgChange = 50;
            ImgChangeY = 32;
        }
        public void FlyStraight()
        {
            ImgChange = 0 + fixImageX;
            ImgChangeY = 0 + fixImageY;
        }
        public void FlyRight()
        {
            ImgChange = Globals.NormalImageSize*2 + fixImageX;
        }

        public void Reset()
        {
            Lives = Globals.Lives;
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
