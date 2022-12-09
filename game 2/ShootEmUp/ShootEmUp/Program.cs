using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Program
    {
        public static Window window = new Window(64 * 6,128 * 6); //window size
        public static Game game = new Game(); //create the game
        static void Main(string[] args)
        {

            window.setup(); //setup the window
            game.run(); //run the game loop
        }
    }
}
