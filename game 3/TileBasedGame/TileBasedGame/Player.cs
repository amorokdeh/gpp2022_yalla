
namespace TileBasedGame
{
    class Player:GameObject
    {
        public int Lives = Globals.Lives;
        public int Score = 0;

        public Player(string name, int w, int h): base(name, w, h) {}
        
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
