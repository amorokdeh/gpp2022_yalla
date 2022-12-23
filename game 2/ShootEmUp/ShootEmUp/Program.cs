using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Program
    {
        public static Window window = new Window(768, 1024); //window size
        public static Game game = new Game(); //create the game
        static void Main(string[] args)
        {

            window.setup(); //setup the window
            game.run(); //run the game loop


            //Observable und Observer aus der Vorlesung
            //game.tryHeroStuff();
        }
    }
}
