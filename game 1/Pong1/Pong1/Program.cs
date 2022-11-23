using SDL2;
using System;
using System.IO;
using System.Text;


namespace Pong
{
    class Program
    {
        public static Window window = new Window(600, 800); //window size
        public static Game game = new Game(); //create the game
        static void Main(string[] args)
        {
            
            window.setup(); //setup the window
            game.run(); //run the game loop
        }
    }
}
